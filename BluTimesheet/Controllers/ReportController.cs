using BluTimesheet.Services.interfaces;
using System.Web.Http;

namespace BluTimesheet.Controllers
{
    public class ReportController : ApiController
    {
        private IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
    }
}
