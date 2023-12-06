// (c) Copyright Ascensio System SIA 2010-2023
// 
// This program is a free software product.
// You can redistribute it and/or modify it under the terms
// of the GNU Affero General Public License (AGPL) version 3 as published by the Free Software
// Foundation. In accordance with Section 7(a) of the GNU AGPL its Section 15 shall be amended
// to the effect that Ascensio System SIA expressly excludes the warranty of non-infringement of
// any third-party rights.
// 
// This program is distributed WITHOUT ANY WARRANTY, without even the implied warranty
// of MERCHANTABILITY or FITNESS FOR A PARTICULAR  PURPOSE. For details, see
// the GNU AGPL at: http://www.gnu.org/licenses/agpl-3.0.html
// 
// You can contact Ascensio System SIA at Lubanas st. 125a-25, Riga, Latvia, EU, LV-1021.
// 
// The  interactive user interfaces in modified source and object code versions of the Program must
// display Appropriate Legal Notices, as required under Section 5 of the GNU AGPL version 3.
// 
// Pursuant to Section 7(b) of the License you must retain the original Product logo when
// distributing the program. Pursuant to Section 7(e) we decline to grant you any rights under
// trademark law for use of our trademarks.
// 
// All the Product's GUI elements, including illustrations and icon sets, as well as technical writing
// content are licensed under the terms of the Creative Commons Attribution-ShareAlike 4.0
// International. See the License terms at http://creativecommons.org/licenses/by-sa/4.0/legalcode

using ThumbnailSize = Dropbox.Api.Files.ThumbnailSize;

namespace ASC.Files.Thirdparty.Dropbox;

[Transient]
internal class DropboxStorage(TempStream tempStream) : IThirdPartyStorage<FileMetadata, FolderMetadata, Metadata>,
    IDisposable
{
    public bool IsOpened { get; private set; }
    private readonly long _maxChunkedUploadFileSize = 20L * 1024L * 1024L * 1024L;

    private DropboxClient _dropboxClient;

    public void Open(OAuth20Token token)
    {
        if (IsOpened)
        {
            return;
        }

        _dropboxClient = new DropboxClient(token.AccessToken);

        IsOpened = true;
    }

    public void Close()
    {
        _dropboxClient.Dispose();

        IsOpened = false;
    }
    
    private string MakeDropboxPath(string parentPath, string name)
    {
        return (parentPath ?? "") + "/" + (name ?? "");
    }

    public async Task<bool> CheckAccessAsync()
    {
        try
        {
            await _dropboxClient.Users.GetSpaceUsageAsync();
            return true;
        }
        catch (AggregateException)
        {
            return false;
        }
    }


    public async Task<FolderMetadata> GetFolderAsync(string folderId)
    {
        if (string.IsNullOrEmpty(folderId) || folderId == "/")
        {
            return new FolderMetadata(string.Empty, "/");
        }

        try
        {
            var metadata = await _dropboxClient.Files.GetMetadataAsync(folderId);

            return metadata.AsFolder;
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is ApiException<GetMetadataError>
                && ex.InnerException.Message.StartsWith("path/not_found/"))
            {
                return null;
            }
            throw;
        }
    }

    public async Task<FileMetadata> GetFileAsync(string fileId)
    {
        if (string.IsNullOrEmpty(fileId) || fileId == "/")
        {
            return null;
        }

        try
        {
            var data = await _dropboxClient.Files.GetMetadataAsync(fileId);

            return data.AsFile;
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is ApiException<GetMetadataError>
                && ex.InnerException.Message.StartsWith("path/not_found/"))
            {
                return null;
            }
            throw;
        }
    }


    public async Task<List<Metadata>> GetItemsAsync(string folderId)
    {
        var data = await _dropboxClient.Files.ListFolderAsync(folderId);
        return new List<Metadata>(data.Entries);
    }

    public async Task<Stream> GetThumbnailAsync(string fileId, int width, int height)
    {
        try
        {
            var path = new PathOrLink.Path(fileId);
            var size = Convert(width, height);
            var arg = new ThumbnailV2Arg(path, size: size);

            var response = await _dropboxClient.Files.GetThumbnailV2Async(arg);
            return await response.GetContentAsStreamAsync();
        }
        catch
        {
            return null;
        }
    }

    private ThumbnailSize Convert(int width, int height)
    {
        if (width > 368)
        {
            return ThumbnailSize.W480h320.Instance;
        }

        return ThumbnailSize.W256h256.Instance;
    }

    public async Task<Stream> DownloadStreamAsync(FileMetadata file, int offset = 0)
    {
        var filePath = MakeId(file);
        ArgumentException.ThrowIfNullOrEmpty(filePath);

        using var response = await _dropboxClient.Files.DownloadAsync(filePath);
        var tempBuffer = tempStream.Create();
        await using var str = await response.GetContentAsStreamAsync();
        if (str != null)
        {
            await str.CopyToAsync(tempBuffer);
            await tempBuffer.FlushAsync();
            tempBuffer.Seek(offset, SeekOrigin.Begin);
        }

        return tempBuffer;
    }

    public async Task<FolderMetadata> CreateFolderAsync(string title, string parentId)
    {
        var path = MakeDropboxPath(parentId, title);
        var result = await _dropboxClient.Files.CreateFolderV2Async(path, true);

        return result.Metadata;
    }

    public async Task<FileMetadata> CreateFileAsync(Stream fileStream, string title, string parentId)
    {
        var path = MakeDropboxPath(parentId, title);

        return await _dropboxClient.Files.UploadAsync(path, WriteMode.Add.Instance, true, body: fileStream);
    }

    public async Task DeleteItemAsync(Metadata dropboxItem)
    {
        await _dropboxClient.Files.DeleteV2Async(dropboxItem.PathDisplay);
    }

    public async Task<FolderMetadata> MoveFolderAsync(string folderId, string newFolderName, string toFolderId)
    {
        var pathTo = MakeDropboxPath(newFolderName, toFolderId);
        var result = await _dropboxClient.Files.MoveV2Async(folderId, pathTo, autorename: true);

        return (FolderMetadata)result.Metadata;
    }

    public async Task<FolderMetadata> RenameFolderAsync(string folderId, string newName)
    {
        var folder = await GetFolderAsync(folderId);
        var pathTo = GetParentFolderId(folder);
        var result = await _dropboxClient.Files.MoveV2Async(folderId, MakeDropboxPath(pathTo, newName), autorename: true);

        return (FolderMetadata)result.Metadata;
    }

    public async Task<FileMetadata> MoveFileAsync(string fileId, string newFileName, string toFolderId)
    {
        var pathTo = MakeDropboxPath(newFileName, toFolderId);
        var result = await _dropboxClient.Files.MoveV2Async(fileId, pathTo, autorename: true);

        return (FileMetadata)result.Metadata;
    }

    public async Task<FileMetadata> RenameFileAsync(string fileId, string newName)
    {
        var file = await GetFileAsync(fileId);
        var pathTo = GetParentFolderId(file);
        var result = await _dropboxClient.Files.MoveV2Async(fileId, MakeDropboxPath(pathTo, newName), autorename: true);

        return (FileMetadata)result.Metadata;
    }

    public async Task<FolderMetadata> CopyFolderAsync(string folderId, string newFolderName, string toFolderId)
    {
        var pathTo = MakeDropboxPath(newFolderName, toFolderId);
        var result = await _dropboxClient.Files.CopyV2Async(folderId, pathTo, autorename: true);

        return (FolderMetadata)result.Metadata;
    }

    public async Task<FileMetadata> CopyFileAsync(string fileId, string newFileName, string toFolderId)
    {
        var pathTo = MakeDropboxPath(newFileName, toFolderId);
        var result = await _dropboxClient.Files.CopyV2Async(fileId, pathTo, autorename: true);

        return (FileMetadata)result.Metadata;
    }

    public async Task<FileMetadata> SaveStreamAsync(string fileId, Stream fileStream)
    {
        var metadata = await _dropboxClient.Files.UploadAsync(fileId, WriteMode.Overwrite.Instance, body: fileStream);

        return metadata.AsFile;
    }

    public async Task<string> CreateRenewableSessionAsync()
    {
        var session = await _dropboxClient.Files.UploadSessionStartAsync(body: new MemoryStream());

        return session.SessionId;
    }

    public async Task TransferAsync(string dropboxSession, long offset, Stream stream)
    {
        await _dropboxClient.Files.UploadSessionAppendV2Async(new UploadSessionCursor(dropboxSession, (ulong)offset), body: stream);
    }

    public async Task<Metadata> FinishRenewableSessionAsync(string dropboxSession, string dropboxFolderPath, string fileName, long offset)
    {
        var dropboxFilePath = MakeDropboxPath(dropboxFolderPath, fileName);
        return await FinishRenewableSessionAsync(dropboxSession, dropboxFilePath, offset);
    }

    public async Task<Metadata> FinishRenewableSessionAsync(string dropboxSession, string dropboxFilePath, long offset)
    {
        using var tempBody = new MemoryStream();
        return await _dropboxClient.Files.UploadSessionFinishAsync(
            new UploadSessionCursor(dropboxSession, (ulong)offset),
            new CommitInfo(dropboxFilePath, WriteMode.Overwrite.Instance),
            body: tempBody);
    }

    private string MakeId(Metadata dropboxItem)
    {
        string path = null;
        if (dropboxItem != null)
        {
            path = dropboxItem.PathDisplay;
        }

        return path;
    }

    private string GetParentFolderId(Metadata dropboxItem)
    {
        if (dropboxItem == null || IsRoot(dropboxItem.AsFolder))
        {
            return null;
        }

        var pathLength = dropboxItem.PathDisplay.Length - dropboxItem.Name.Length;

        return dropboxItem.PathDisplay[..(pathLength > 1 ? pathLength - 1 : 0)];
    }

    private bool IsRoot(FolderMetadata dropboxFolder)
    {
        return dropboxFolder is { Id: "/" };
    }


    public Task<long> GetMaxUploadSizeAsync()
    {
        return Task.FromResult(_maxChunkedUploadFileSize);
    }

    public void Dispose()
    {
        _dropboxClient?.Dispose();
    }
}
