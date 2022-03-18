/*
 *
 * (c) Copyright Ascensio System Limited 2010-2018
 *
 * This program is freeware. You can redistribute it and/or modify it under the terms of the GNU 
 * General Public License (GPL) version 3 as published by the Free Software Foundation (https://www.gnu.org/copyleft/gpl.html). 
 * In accordance with Section 7(a) of the GNU GPL its Section 15 shall be amended to the effect that 
 * Ascensio System SIA expressly excludes the warranty of non-infringement of any third-party rights.
 *
 * THIS PROGRAM IS DISTRIBUTED WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE. For more details, see GNU GPL at https://www.gnu.org/copyleft/gpl.html
 *
 * You can contact Ascensio System SIA by email at sales@onlyoffice.com
 *
 * The interactive user interfaces in modified source and object code versions of ONLYOFFICE must display 
 * Appropriate Legal Notices, as required under Section 5 of the GNU GPL version 3.
 *
 * Pursuant to Section 7 § 3(b) of the GNU GPL you must retain the original ONLYOFFICE logo which contains 
 * relevant author attributions when distributing the software. If the display of the logo in its graphic 
 * form is not reasonably feasible for technical reasons, you must include the words "Powered by ONLYOFFICE" 
 * in every copy of the program you distribute. 
 * Pursuant to Section 7 § 3(e) we decline to grant you any rights under trademark law for use of our trademarks.
 *
*/

using Profile = AutoMapper.Profile;

namespace ASC.Files.Core;

public enum FolderType
{
    DEFAULT = 0,
    COMMON = 1,
    BUNCH = 2,
    TRASH = 3,
    USER = 5,
    SHARE = 6,
    Projects = 8,
    Favorites = 10,
    Recent = 11,
    Templates = 12,
    Privacy = 13,
}

public interface IFolder
{
    public FolderType FolderType { get; set; }
    public int FilesCount { get; set; }
    public int FoldersCount { get; set; }
    public bool Shareable { get; set; }
    public int NewForMe { get; set; }
    public string FolderUrl { get; set; }
}

[DebuggerDisplay("{Title} ({Id})")]
[Transient]
public class Folder<T> : FileEntry<T>, IFolder, IMapFrom<DbFolder>
{
    public FolderType FolderType { get; set; }
    public int FilesCount { get; set; }
    public int FoldersCount { get; set; }
    public bool Shareable { get; set; }
    public int NewForMe { get; set; }
    public string FolderUrl { get; set; }
    public override bool IsNew
    {
        get => Convert.ToBoolean(NewForMe);
        set => NewForMe = Convert.ToInt32(value);
    }

    public Folder()
    {
        Title = string.Empty;
        FileEntryType = FileEntryType.Folder;
    }

    public Folder(FileHelper fileHelper, Global global) : this()
    {
        FileHelper = fileHelper;
        Global = global;
    }

    public override string UniqID => $"folder_{Id}";

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DbFolder, Folder<int>>();

        profile.CreateMap<DbFolderQuery, Folder<int>>()
            .IncludeMembers(r => r.Folder)
            .ForMember(r => r.CreateOn, r => r.ConvertUsing<TenantDateTimeConverter, DateTime>(s => s.Folder.CreateOn))
            .ForMember(r => r.ModifiedOn, r => r.ConvertUsing<TenantDateTimeConverter, DateTime>(s => s.Folder.ModifiedOn))
            .AfterMap((q, result) =>
            {
                switch (result.FolderType)
                {
                    case FolderType.COMMON:
                        result.Title = FilesUCResource.CorporateFiles;
                        break;
                    case FolderType.USER:
                        result.Title = FilesUCResource.MyFiles;
                        break;
                    case FolderType.SHARE:
                        result.Title = FilesUCResource.SharedForMe;
                        break;
                    case FolderType.Recent:
                        result.Title = FilesUCResource.Recent;
                        break;
                    case FolderType.Favorites:
                        result.Title = FilesUCResource.Favorites;
                        break;
                    case FolderType.TRASH:
                        result.Title = FilesUCResource.Trash;
                        break;
                    case FolderType.Privacy:
                        result.Title = FilesUCResource.PrivacyRoom;
                        break;
                    case FolderType.Projects:
                        result.Title = FilesUCResource.ProjectFiles;
                        break;
                    case FolderType.BUNCH:
                        try
                        {
                            result.Title = string.Empty;
                        }
                        catch (Exception)
                        {
                            //Global.Logger.Error(e);
                        }
                        break;
                }

                if (result.FolderType != FolderType.DEFAULT)
                {
                    if (0.Equals(result.ParentId))
                    {
                        result.RootFolderType = result.FolderType;
                    }

                    if (result.RootCreateBy == default)
                    {
                        result.RootCreateBy = result.CreateBy;
                    }

                    if (0.Equals(result.RootId))
                    {
                        result.RootId = result.Id;
                    }
                }
            })
            .ConstructUsingServiceLocator();
    }
}
