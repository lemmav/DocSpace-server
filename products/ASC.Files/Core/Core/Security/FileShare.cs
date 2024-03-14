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

namespace ASC.Files.Core.Security;

/// <summary>
/// </summary>
[EnumExtensions]
[JsonConverter(typeof(FileShareConverter))]
public enum FileShare
{
    None,
    ReadWrite,
    Read,
    Restrict,
    Varies,
    Review,
    Comment,
    FillForms,
    CustomFilter,
    RoomAdmin,
    Editing,
    Collaborator
}

public class FileShareConverter : System.Text.Json.Serialization.JsonConverter<FileShare>
{
    public override FileShare Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var result))
        {
            return (FileShare)result;
        }

        if (reader.TokenType == JsonTokenType.String && FileShareExtensions.TryParse(reader.GetString(), out var share))
        {
            return share;
        }

        return FileShare.None;
    }

    public override void Write(Utf8JsonWriter writer, FileShare value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((int)value);
    }
}

public static partial class FileShareExtensions
{
    public static string GetAccessString(FileShare fileShare, bool useRoomFormat = false, CultureInfo cultureInfo = null)
    {
        var prefix = useRoomFormat && fileShare != FileShare.ReadWrite ? "RoleEnum_" : "AceStatusEnum_";

        switch (fileShare)
        {
            case FileShare.Read:
            case FileShare.ReadWrite:
            case FileShare.CustomFilter:
            case FileShare.Review:
            case FileShare.FillForms:
            case FileShare.Comment:
            case FileShare.Restrict:
            case FileShare.RoomAdmin:
            case FileShare.Editing:
            case FileShare.Collaborator:
            case FileShare.Varies:
            case FileShare.None:
                return FilesCommonResource.ResourceManager.GetString(prefix + fileShare.ToStringFast(), cultureInfo);
            default:
                return string.Empty;
        }
    }
}