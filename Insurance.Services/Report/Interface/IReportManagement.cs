using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Models.ViewModel;
using Insurance.Services.Report.DTO;

namespace Insurance.Services.Report.Interface
{
    public interface IReportManagement
    {
        Task<List<loadSummaryDashboardViewModel>> LoadSummaryofDashboard(string UserId);
        Task<List<TransactionView>> TransactionHistoryView(string UserId);
        Task<List<InFlowOutFlowReportView>> InFlowOutFlowView(string UserId);
        Task<List<loadSummaryDashboardViewModel>> Top10SuccessfulTransaction(string UserId, int Today);

        Task<List<loadSummaryDashboardViewModel>> Top10WalletTransaction(string UserId);

        Task<List<loadSummaryDashboardViewModel>> AdminLoadSummaryofDashboard();

        
    }
}
