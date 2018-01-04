using BluTimesheet.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BluTimesheet.Models;
using BluTimesheet.Models.DbModels;
using BluTimesheet.Repositories;

namespace BluTimesheet.Services.implementations
{
    public class ReportService //: IReportService
    {
        private readonly ReportRepository reportRepository;

        public ReportService(ReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }







        public IEnumerable<Activity> GetActivitesByUserPerMonth(int ReportFrom, int ReportTo, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetActivitesByUserr(string userId)
        {
            throw new NotImplementedException();
        }
    }
}