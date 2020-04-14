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


using ASC.Core.Common.Settings;
using ASC.Core.Tenants;
using ASC.CRM.Core.Dao;
using ASC.CRM.Core.Enums;
using ASC.CRM.Resources;
using ASC.Files.Core;
using ASC.Web.CRM.Core;
using ASC.Web.Files.Services.DocumentService;
using Autofac;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.DependencyInjection;


namespace ASC.Web.CRM.Classes
{
    public class ReportHelper
    {
        public ReportHelper(TenantUtil tenantUtil,
                            Global global,
                            DocbuilderReportsUtility docbuilderReportsUtility,
                            SettingsManager settingsManager,
                            IServiceProvider serviceProvider
                          )
        {
            TenantUtil = tenantUtil;
            Global = global;
            DocbuilderReportsUtility = docbuilderReportsUtility;
            SettingsManager = settingsManager;
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public DocbuilderReportsUtility DocbuilderReportsUtility { get; }
        public Global Global { get; }
        public SettingsManager SettingsManager { get; }
        public TenantUtil TenantUtil { get; }

        private string GetFileName(ReportType reportType)
        {
            string reportName;

            switch (reportType)
            {
                case ReportType.SalesByManagers:
                    reportName = CRMReportResource.SalesByManagersReport;
                    break;
                case ReportType.SalesForecast:
                    reportName = CRMReportResource.SalesForecastReport;
                    break;
                case ReportType.SalesFunnel:
                    reportName = CRMReportResource.SalesFunnelReport;
                    break;
                case ReportType.WorkloadByContacts:
                    reportName = CRMReportResource.WorkloadByContactsReport;
                    break;
                case ReportType.WorkloadByTasks:
                    reportName = CRMReportResource.WorkloadByTasksReport;
                    break;
                case ReportType.WorkloadByDeals:
                    reportName = CRMReportResource.WorkloadByDealsReport;
                    break;
                case ReportType.WorkloadByInvoices:
                    reportName = CRMReportResource.WorkloadByInvoicesReport;
                    break;
                case ReportType.WorkloadByVoip:
                    reportName = CRMReportResource.WorkloadByVoipReport;
                    break;
                case ReportType.SummaryForThePeriod:
                    reportName = CRMReportResource.SummaryForThePeriodReport;
                    break;
                case ReportType.SummaryAtThisMoment:
                    reportName = CRMReportResource.SummaryAtThisMomentReport;
                    break;
                default:
                    reportName = string.Empty;
                    break;
            }

            return string.Format("{0} ({1} {2}).xlsx",
                                 reportName,
                                 TenantUtil.DateTimeNow().ToShortDateString(),
                                 TenantUtil.DateTimeNow().ToShortTimeString());
        }

        public bool CheckReportData(ReportType reportType, ReportTimePeriod timePeriod, Guid[] managers)
        {
            throw new NotImplementedException();
            using (var scope = DIHelper.Resolve())
            {
                var reportDao = scope.Resolve<DaoFactory>().GetReportDao();

                //switch (reportType)
                //{
                //    case ReportType.SalesByManagers:
                //        return reportDao.CheckSalesByManagersReportData(timePeriod, managers);
                //    case ReportType.SalesForecast:
                //        return reportDao.CheckSalesForecastReportData(timePeriod, managers);
                //    case ReportType.SalesFunnel:
                //        return reportDao.CheckSalesFunnelReportData(timePeriod, managers);
                //    case ReportType.WorkloadByContacts:
                //        return reportDao.CheckWorkloadByContactsReportData(timePeriod, managers);
                //    case ReportType.WorkloadByTasks:
                //        return reportDao.CheckWorkloadByTasksReportData(timePeriod, managers);
                //    case ReportType.WorkloadByDeals:
                //        return reportDao.CheckWorkloadByDealsReportData(timePeriod, managers);
                //    case ReportType.WorkloadByInvoices:
                //        return reportDao.CheckWorkloadByInvoicesReportData(timePeriod, managers);
                //    case ReportType.WorkloadByVoip:
                //        return reportDao.CheckWorkloadByViopReportData(timePeriod, managers);
                //    case ReportType.SummaryForThePeriod:
                //        return reportDao.CheckSummaryForThePeriodReportData(timePeriod, managers);
                //    case ReportType.SummaryAtThisMoment:
                //        return reportDao.CheckSummaryAtThisMomentReportData(timePeriod, managers);
                //    default:
                //        return false;
                //}
            }
        }

        public List<string> GetMissingRates(ReportType reportType)
        {
            using (var scope = DIHelper.Resolve())
            {
                var reportDao = scope.Resolve<DaoFactory>().GetReportDao();
                if (reportType == ReportType.WorkloadByTasks || reportType == ReportType.WorkloadByInvoices ||
                    reportType == ReportType.WorkloadByContacts || reportType == ReportType.WorkloadByVoip) return null;

                return reportDao.GetMissingRates(SettingsManager.Load<CRMSettings>().DefaultCurrency.Abbreviation);
            }
        }

        private object GetReportData(ReportType reportType, ReportTimePeriod timePeriod, Guid[] managers)
        {
            throw new NotImplementedException();
            using (var scope = DIHelper.Resolve())
            {
                var reportDao = scope.Resolve<DaoFactory>().GetReportDao();

                var defaultCurrency = SettingsManager.Load<CRMSettings>().DefaultCurrency.Abbreviation;

                //switch (reportType)
                //{
                //    case ReportType.SalesByManagers:
                //        return reportDao.GetSalesByManagersReportData(timePeriod, managers, defaultCurrency);
                //    case ReportType.SalesForecast:
                //        return reportDao.GetSalesForecastReportData(timePeriod, managers, defaultCurrency);
                //    case ReportType.SalesFunnel:
                //        return reportDao.GetSalesFunnelReportData(timePeriod, managers, defaultCurrency);
                //    case ReportType.WorkloadByContacts:
                //        return reportDao.GetWorkloadByContactsReportData(timePeriod, managers);
                //    case ReportType.WorkloadByTasks:
                //        return reportDao.GetWorkloadByTasksReportData(timePeriod, managers);
                //    case ReportType.WorkloadByDeals:
                //        return reportDao.GetWorkloadByDealsReportData(timePeriod, managers, defaultCurrency);
                //    case ReportType.WorkloadByInvoices:
                //        return reportDao.GetWorkloadByInvoicesReportData(timePeriod, managers);
                //    case ReportType.WorkloadByVoip:
                //        return reportDao.GetWorkloadByViopReportData(timePeriod, managers);
                //    case ReportType.SummaryForThePeriod:
                //        return reportDao.GetSummaryForThePeriodReportData(timePeriod, managers, defaultCurrency);
                //    case ReportType.SummaryAtThisMoment:
                //        return reportDao.GetSummaryAtThisMomentReportData(timePeriod, managers, defaultCurrency);
                //    default:
                //        return null;
                //}
            }
        }

        private string GetReportScript(object data, ReportType type, string fileName)
        {
            var script =
                FileHelper.ReadTextFromEmbeddedResource(string.Format("ASC.Web.CRM.ReportTemplates.{0}.docbuilder", type));

            if (string.IsNullOrEmpty(script))
                throw new Exception(CRMReportResource.BuildErrorEmptyDocbuilderTemplate);

            return script.Replace("${outputFilePath}", fileName)
                         .Replace("${reportData}", JsonConvert.SerializeObject(data));
        }

        private void SaveReportFile(ReportState state, string url)
        {
            using (var scope = DIHelper.Resolve())
            {
                var daoFactory = scope.Resolve<DaoFactory>();
                var data = new WebClient().DownloadData(url);

                using (var stream = new System.IO.MemoryStream(data))
                {

                    var document = ServiceProvider.GetService<File<int>>();

                    document.Title = state.FileName;
                    document.FolderID = daoFactory.GetFileDao().GetRoot();
                    document.ContentLength = stream.Length;
                                     
                    var file = daoFactory.GetFileDao().SaveFile(document, stream);

                    daoFactory.GetReportDao().SaveFile((int)file.ID, (ReportType)state.ReportType);

                    state.FileId = (int)file.ID;
                }
            }
        }

        public ReportState RunGenareteReport(ReportType reportType, ReportTimePeriod timePeriod, Guid[] managers)
        {
            var reportData = GetReportData(reportType, timePeriod, managers);
            if (reportData == null)
                throw new Exception(CRMReportResource.ErrorNullReportData);

            var tmpFileName = DocbuilderReportsUtility.TmpFileName;

            var script = GetReportScript(reportData, reportType, tmpFileName);
            if (string.IsNullOrEmpty(script))
                throw new Exception(CRMReportResource.ErrorNullReportScript);

         //   ServiceProvider.GetService<ReportState>()

            var state = new ReportState(GetFileName(reportType), tmpFileName, script, (int)reportType, ReportOrigin.CRM, SaveReportFile, null);

            DocbuilderReportsUtility.Enqueue(state);

            return state;
        }
    }
}