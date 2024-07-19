// (c) Copyright Ascensio System SIA 2010-2022
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

using ASC.Core.Common.EF.Context;
using ASC.Core.Common.EF.Model;
using ASC.Core.Common.EF;
using ASC.Core.Tenants;

using Microsoft.EntityFrameworkCore.Internal;

namespace Migration.Runner;

public class MigrationRunner
{
    private readonly DbContextActivator _dbContextActivator;

    public MigrationRunner(IServiceProvider serviceProvider)
    {
        _dbContextActivator = new DbContextActivator(serviceProvider);
    }

    public void RunApplyMigrations(string path, ProviderInfo dbProvider, ProviderInfo teamlabsiteProvider, ConfigurationInfo configurationInfo, string targetMigration)
    {
        var migrationContext = _dbContextActivator.CreateInstance(typeof(MigrationContext), dbProvider) as MigrationContext;
        var query = from u in migrationContext.Users
                join t in migrationContext.Tenants on u.TenantId equals t.Id into t
                from mapping in t.DefaultIfEmpty()
                select new
                {
                    u = u,
                    t = mapping
                };
        var tenants = query.Where(q=> q.t == null).Select(q=> q.u.TenantId).Distinct().ToList();

        foreach (var tenant in tenants)
        {
            var dbTenant = new DbTenant();
            dbTenant.Id = tenant;
            dbTenant.Alias = $"temp-{tenant}";
            dbTenant.Version = 2;
            dbTenant.Name = "";
            dbTenant.Status = TenantStatus.Suspended;
            dbTenant.LastModified = DateTime.Now;
            migrationContext.Tenants.Add(dbTenant);
        }

        migrationContext.SaveChanges();

        var queryRows = from q in migrationContext.QuotaRows
                    join t in migrationContext.Tenants on q.TenantId equals t.Id into t
                    from mapping in t.DefaultIfEmpty()
                    select new
                    {
                        q = q,
                        t = mapping
                    };
        tenants = queryRows.Where(q => q.t == null).Select(q => q.q.TenantId).Distinct().ToList();

        foreach (var tenant in tenants)
        {
            var dbTenant = new DbTenant();
            dbTenant.Id = tenant;
            dbTenant.Alias = $"temp-{tenant}";
            dbTenant.Version = 2;
            dbTenant.Name = "";
            dbTenant.Status = TenantStatus.Suspended;
            dbTenant.LastModified = DateTime.Now;
            migrationContext.Tenants.Add(dbTenant);
        }

        migrationContext.SaveChanges();

        var queryTree = from t in migrationContext.Tree
                        join f in migrationContext.Folders on t.FolderId equals f.Id into f
                        from mapping in f.DefaultIfEmpty()
                        select new
                        {
                            t = t,
                            f = mapping
                        };

        queryTree.Where(q => q.f == null).Select(q => q.t).ExecuteDelete();

        Migrate(migrationContext, targetMigration);

       // var teamlabContext = _dbContextActivator.CreateInstance(typeof(TeamlabSiteContext), teamlabsiteProvider);
       // Migrate(teamlabContext, targetMigration);

        if (configurationInfo == ConfigurationInfo.Standalone)
        {
            migrationContext = _dbContextActivator.CreateInstance(typeof(MigrationContext), dbProvider, ConfigurationInfo.Standalone) as MigrationContext;
            Migrate(migrationContext, targetMigration);
        }

        migrationContext.Tenants.Where(t=> tenants.Contains(t.Id)).ExecuteDelete();
        Console.WriteLine("Migrations applied");
    }

    private void Migrate(DbContext migrationContext, string targetMigration)
    {
        if (string.IsNullOrEmpty(targetMigration))
        {
            migrationContext.Database.Migrate();
        }
        else
        {
            var migrations = migrationContext.Database.GetMigrations();
            if (migrations.Contains(targetMigration))
            {
                Console.WriteLine("Migration to " + targetMigration);

                var migrator = migrationContext.Database.GetService<IMigrator>();
                migrator?.Migrate(targetMigration);
            }
        }
    }
}
