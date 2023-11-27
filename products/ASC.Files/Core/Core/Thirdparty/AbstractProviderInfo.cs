﻿// (c) Copyright Ascensio System SIA 2010-2023
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

using DriveFile = Google.Apis.Drive.v3.Data.File;

namespace ASC.Files.Core.Core.Thirdparty;

internal abstract class AbstractProviderInfo<TFile, TFolder, TItem, TProvider> : IProviderInfo<TFile, TFolder, TItem>
    where TFile : class, TItem
    where TFolder : class, TItem
    where TItem : class
    where TProvider : Consumer, IOAuthProvider, new()
{
    public abstract Selector Selector { get; }
    public abstract ProviderFilter ProviderFilter { get; }
    public virtual bool MutableEntityId => false;
    private readonly DisposableWrapper _wrapper;
    internal readonly ProviderInfoHelper ProviderInfoHelper;

    protected AbstractProviderInfo(DisposableWrapper wrapper, ProviderInfoHelper providerInfoHelper)
    {
        _wrapper = wrapper;
        ProviderInfoHelper = providerInfoHelper;
    }

    public DateTime CreateOn { get; set; }
    public string CustomerTitle { get; set; }
    public string FolderId { get; set; }
    public FolderType FolderType { get; set; }
    public bool HasLogo { get; set; }
    public int ProviderId { get; set; }
    public Guid Owner { get; set; }
    public bool Private { get; set; }
    public string ProviderKey { get; set; }
    public string RootFolderId => $"{Selector.Id}-" + ProviderId;
    public FolderType RootFolderType { get; set; }
    public OAuth20Token Token { get; set; }
    public bool StorageOpened => _wrapper.TryGetStorage(ProviderId, out var storage) && storage.IsOpened;

    public Task<IThirdPartyStorage<TFile, TFolder, TItem>> StorageAsync
    {
        get
        {
            if (!_wrapper.TryGetStorage<IThirdPartyStorage<TFile, TFolder, TItem>>(ProviderId, out var storage) || !storage.IsOpened)
            {
                return _wrapper.CreateStorageAsync<IThirdPartyStorage<TFile, TFolder, TItem>, TProvider>(Token, ProviderId);
            }

            return Task.FromResult(storage);
        }
    }

    public async Task<bool> CheckAccessAsync()
    {
        var storage = await StorageAsync;

        return await storage.CheckAccessAsync();
    }

    public void Dispose()
    {
        if (StorageOpened)
        {
            StorageAsync.Result.Close();
        }
    }

    public Task InvalidateStorageAsync()
    {
        if (_wrapper != null)
        {
            _wrapper.Dispose();
        }

        return CacheResetAsync();
    }

    public void UpdateTitle(string newtitle)
    {
        CustomerTitle = newtitle;
    }

    public Task CacheResetAsync(string id = null, bool? isFile = null)
    {
        return ProviderInfoHelper.CacheResetAsync(ProviderId, id, isFile);
    }

    public async Task<TFile> GetFileAsync(string fileId)
    {
        var storage = await StorageAsync;

        return await ProviderInfoHelper.GetFileAsync(storage, ProviderId, fileId, Selector.Id);
    }

    public async Task<TFolder> GetFolderAsync(string folderId)
    {
        var storage = await StorageAsync;

        return await ProviderInfoHelper.GetFolderAsync(storage, ProviderId, folderId, Selector.Id);
    }

    public async Task<List<TItem>> GetItemsAsync(string folderId)
    {
        var storage = await StorageAsync;

        return await ProviderInfoHelper.GetItemsAsync(storage, ProviderId, folderId, Selector.Id);
    }
}

[Singleton]
public class ProviderInfoHelper
{
    private readonly ICache _cache;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(1);
    private readonly ConcurrentDictionary<string, object> _cacheKeys;
    private readonly ICacheNotify<BoxCacheItem> _cacheNotify;
    private readonly IEnumerable<string> _selectors = Selectors.StoredCache.Select(s => s.Id);

    public ProviderInfoHelper(ICacheNotify<BoxCacheItem> cacheNotify, ICache cache)
    {
        _cache = cache;
        _cacheKeys = new ConcurrentDictionary<string, object>();
        _cacheNotify = cacheNotify;
        foreach (var selector in _selectors)
        {
            _cacheNotify.Subscribe((i) =>
            {
                if (i.ResetAll)
                {
                    _cache.Remove(_cacheKeys, new Regex($"^{selector}-" + i.Key + ".*"));
                    _cache.Remove(_cacheKeys, new Regex($"^{selector}f-" + i.Key + ".*"));
                    _cache.Remove(_cacheKeys, new Regex($"^{selector}d-" + i.Key + ".*"));
                }

                if (!i.IsFileExists)
                {
                    _cache.Remove($"{selector}-" + i.Key);

                    _cache.Remove($"{selector}d-" + i.Key);
                }
                else
                {
                    if (i.IsFile)
                    {
                        _cache.Remove($"{selector}f-" + i.Key);
                    }
                    else
                    {
                        _cache.Remove($"{selector}d-" + i.Key);
                    }
                }
            }, CacheNotifyAction.Remove);
        }
    }

    internal async Task CacheResetAsync(int ThitdId, string id = null, bool? isFile = null)
    {
        var key = ThitdId + "-";
        if (id == null)
        {
            await _cacheNotify.PublishAsync(new BoxCacheItem { ResetAll = true, Key = key }, CacheNotifyAction.Remove);
        }
        else
        {
            key += id;

            await _cacheNotify.PublishAsync(new BoxCacheItem { IsFile = isFile ?? false, IsFileExists = isFile.HasValue, Key = key }, CacheNotifyAction.Remove);
        }
    }

    internal async ValueTask<TFile> GetFileAsync<TFile>(IThirdPartyFileStorage<TFile> storage, int id, string fileId, string selector) where TFile : class
    {
        var key = $"{selector}f-" + id + "-" + fileId;
        var file = _cache.Get<TFile>(key);
        if (file == null)
        {
            file = await storage.GetFileAsync(fileId);
            if (file != null)
            {
                _cache.Insert(key, file, DateTime.UtcNow.Add(_cacheExpiration), EvictionCallback);
                _cacheKeys.TryAdd(key, null);
            }
        }

        return file;
    }

    internal async Task<TFolder> GetFolderAsync<TFolder>(IThirdPartyFolderStorage<TFolder> storage, int id, string folderId, string selector) where TFolder : class
    {
        var key = $"{selector}d-" + id + "-" + folderId;
        var folder = _cache.Get<TFolder>(key);
        if (folder == null)
        {
            folder = await storage.GetFolderAsync(folderId);
            if (folder != null)
            {
                _cache.Insert(key, folder, DateTime.UtcNow.Add(_cacheExpiration), EvictionCallback);
                _cacheKeys.TryAdd(key, null);
            }
        }

        return folder;
    }

    internal async Task<List<TItem>> GetItemsAsync<TItem>(IThirdPartyItemStorage<TItem> storage, int id, string folderId, string selector, bool? folder = null) where TItem : class
    {
        var items = _cache.Get<List<TItem>>($"{selector}-{folder}" + id + "-" + folderId);

        if (items == null)
        {
            if (folder != null && storage is IGoogleDriveItemStorage<TItem> googleStorage)
            {
                items = await googleStorage.GetItemsAsync(folderId, folder);
            }
            else
            {
                items = await storage.GetItemsAsync(folderId);
            }
            var key = $"{selector}-" + id + "-" + folderId;
            _cache.Insert(key, items, DateTime.UtcNow.Add(_cacheExpiration), EvictionCallback);
            _cacheKeys.TryAdd(key, null);
        }

        return items;
    }

    private void EvictionCallback(object key, object value, EvictionReason reason, object state)
    {
        _cacheKeys.TryRemove(key.ToString(), out _);
    }
}

[Transient(Additional = typeof(DisposableWrapperExtension))]
public class DisposableWrapper : IDisposable
{
    private readonly ConsumerFactory _consumerFactory;
    private readonly OAuth20TokenHelper _oAuth20TokenHelper;
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<int, IThirdPartyStorage> _storages = new();

    public DisposableWrapper(ConsumerFactory consumerFactory, IServiceProvider serviceProvider, OAuth20TokenHelper oAuth20TokenHelper)
    {
        _consumerFactory = consumerFactory;
        _serviceProvider = serviceProvider;
        _oAuth20TokenHelper = oAuth20TokenHelper;
    }

    public void Dispose()
    {
        foreach (var (key, storage) in _storages)
        {
            storage.Close();
            _storages.Remove(key, out _);
        }
    }

    internal Task<T> CreateStorageAsync<T, T1>(OAuth20Token token, int id)
        where T : IThirdPartyStorage
        where T1 : Consumer, IOAuthProvider, new()
    {
        if (TryGetStorage<T>(id, out var storage) && storage.IsOpened)
        {
            return Task.FromResult(storage);
        }

        return InternalCreateStorageAsync<T, T1>(token, id);
    }

    internal bool TryGetStorage<T>(int providerId, out T storage)
    {
        var result = _storages.TryGetValue(providerId, out var s);
        storage = (T)s;
        return result;
    }

    internal bool TryGetStorage(int providerId, out IThirdPartyStorage storage)
    {
        return _storages.TryGetValue(providerId, out storage);
    }

    private async ValueTask CheckTokenAsync<T>(OAuth20Token token, int id) where T : Consumer, IOAuthProvider, new()
    {
        if (token == null)
        {
            throw new UnauthorizedAccessException("Cannot create third party session with given token");
        }

        if (token.IsExpired)
        {
            token = _oAuth20TokenHelper.RefreshToken<T>(_consumerFactory, token);

            var dbDao = _serviceProvider.GetService<ProviderAccountDao>();
            await dbDao.UpdateProviderInfoAsync(id, new AuthData(token: token.ToJson()));
        }
    }

    private async Task<T> InternalCreateStorageAsync<T, T1>(OAuth20Token token, int id)
        where T : IThirdPartyStorage
        where T1 : Consumer, IOAuthProvider, new()
    {
        var storage = _serviceProvider.GetService<T>();
        await CheckTokenAsync<T1>(token, id);

        storage.Open(token);

        _storages.TryAdd(id, storage);

        return storage;
    }
}

public static class DisposableWrapperExtension
{
    public static void Register(DIHelper services)
    {
        services.TryAdd<IThirdPartyStorage<BoxFile, BoxFolder, BoxItem>, BoxStorage>();
        services.TryAdd<IThirdPartyStorage<FileMetadata, FolderMetadata, Metadata>, DropboxStorage>();
        services.TryAdd<IThirdPartyStorage<DriveFile, DriveFile, DriveFile>, GoogleDriveStorage>();
        services.TryAdd<IThirdPartyStorage<Item, Item, Item>, OneDriveStorage>();
    }
}