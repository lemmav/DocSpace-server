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

using ASC.Web.Core.WhiteLabel;
using ASC.Web.Files.Configuration;

namespace ASC.Files.Service;

public class Startup : BaseWorkerStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        : base(configuration, hostEnvironment)
    {
        if (String.IsNullOrEmpty(configuration["RabbitMQ:ClientProvidedName"]))
        {
            configuration["RabbitMQ:ClientProvidedName"] = Program.AppName;
        }
    }

    public override async Task ConfigureServices(IServiceCollection services)
    {
        await base.ConfigureServices(services);
        services.AddHttpClient();

        if (!Enum.TryParse<ElasticLaunchType>(Configuration["elastic:mode"], true, out var elasticLaunchType))
        {
            elasticLaunchType = ElasticLaunchType.Inclusive;
        }

        if (elasticLaunchType != ElasticLaunchType.Disabled)
        {
            services.AddHostedService<ElasticSearchIndexService>();
            DIHelper.TryAdd<FactoryIndexer>();
            DIHelper.TryAdd<ElasticSearchService>();
            //DIHelper.TryAdd<FileConverter>();
            DIHelper.TryAdd<FactoryIndexerFile>();
            DIHelper.TryAdd<FactoryIndexerFolder>();
        }

        if (elasticLaunchType != ElasticLaunchType.Exclusive)
        {
            services.AddHostedService<FeedAggregatorService>();
            DIHelper.TryAdd<FeedAggregatorService>();

            //services.AddHostedService<FeedCleanerService>();
            //DIHelper.TryAdd<FeedCleanerService>();

            services.AddActivePassiveHostedService<FileConverterService<int>>(DIHelper, Configuration);
            DIHelper.TryAdd<FileConverterService<int>>();

            services.AddActivePassiveHostedService<FileConverterService<string>>(DIHelper, Configuration);
            DIHelper.TryAdd<FileConverterService<string>>();

            services.AddHostedService<ThumbnailBuilderService>();
            DIHelper.TryAdd<ThumbnailBuilderService>();

            DIHelper.TryAdd<ThumbnailRequestedIntegrationEventHandler>();
            DIHelper.TryAdd<RoomIndexExportIntegrationEventHandler>();
            DIHelper.TryAdd<DeleteIntegrationEventHandler>();
            DIHelper.TryAdd<MoveOrCopyIntegrationEventHandler>();
            DIHelper.TryAdd<BulkDownloadIntegrationEventHandler>();
            DIHelper.TryAdd<MarkAsReadIntegrationEventHandler>();
            DIHelper.TryAdd<EmptyTrashIntegrationEventHandler>();

            services.AddHostedService<Launcher>();
            DIHelper.TryAdd<Launcher>();

            services.AddHostedService<DeleteExpiredService>();
            DIHelper.TryAdd<DeleteExpiredService>();
        }

        DIHelper.TryAdd<AuthManager>();
        DIHelper.TryAdd<BaseCommonLinkUtility>();
        DIHelper.TryAdd<FeedAggregateDataProvider>();
        DIHelper.TryAdd<SecurityContext>();
        DIHelper.TryAdd<TenantManager>();
        DIHelper.TryAdd<UserManager>();
        DIHelper.TryAdd<SocketServiceClient>();
        DIHelper.TryAdd<FileStorageService>();
        DIHelper.TryAdd<Builder<int>>();
        DIHelper.TryAdd<DistributedTaskProgress>();
        DIHelper.TryAdd<DocumentBuilderTask<int>>();
        DIHelper.TryAdd<AdditionalWhiteLabelSettingsHelperInit>();
        DIHelper.TryAdd<NotifyConfiguration>();

        services.AddScoped<ITenantQuotaFeatureChecker, CountRoomChecker>();
        services.AddScoped<CountRoomChecker>();

        services.AddScoped<ITenantQuotaFeatureStat<CountRoomFeature, int>, CountRoomCheckerStatistic>();
        services.AddScoped<CountRoomCheckerStatistic>();

        services.AddScoped<UsersInRoomChecker>();

        services.AddScoped<ITenantQuotaFeatureStat<UsersInRoomFeature, int>, UsersInRoomStatistic>();

        services.AddScoped<UsersInRoomStatistic>();

        services.AddBaseDbContextPool<FilesDbContext>();
        services.AddScoped<IWebItem, ProductEntryPoint>();

        services.AddSingleton(Channel.CreateUnbounded<FileData<int>>());
        services.AddSingleton(svc => svc.GetRequiredService<Channel<FileData<int>>>().Reader);
        services.AddSingleton(svc => svc.GetRequiredService<Channel<FileData<int>>>().Writer);
        services.AddDocumentServiceHttpClient();
    }
}