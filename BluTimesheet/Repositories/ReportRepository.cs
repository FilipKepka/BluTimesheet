using BluTimesheet.Context;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace BluTimesheet.Repositories
{
    public class ReportRepository : GenericRepository<Activity>
    {
        public ReportRepository(TimesheetDbContext context): base(context)
        {

        }
    }
}