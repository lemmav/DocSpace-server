// (c) Copyright Ascensio System SIA 2009-2024
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

namespace ASC.Web.Core;

[WebZone(WebZoneType.TopNavigationProductList | WebZoneType.StartProductList)]
public abstract class Product : IProduct
{
    public abstract Guid ProductID { get; }

    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract string StartURL { get; }

    public abstract string HelpURL { get; }

    public abstract string ProductClassName { get; }

    public abstract bool Visible { get; }

    public abstract void Init();

    public abstract ProductContext Context { get; }

    public virtual void Shutdown() { }

    public virtual Task<IEnumerable<ActivityInfo>> GetAuditEventsAsync(DateTime scheduleDate, Guid userId, Tenant tenant, WhatsNewType whatsNewType, CultureInfo cultureInfo)
    {
        return Task.FromResult(Enumerable.Empty<ActivityInfo>());
    }

    WebItemContext IWebItem.Context { get { return ((IProduct)this).Context; } }

    Guid IWebItem.ID { get { return ProductID; } }

    public virtual bool IsPrimary { get => false; }

    public abstract string ApiURL { get; }
}
