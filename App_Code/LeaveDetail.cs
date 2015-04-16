using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDC.App_Code
{
    public class LeaveDetail
    {
        public string LeaveType;
        public int LeaveCount;

    }
    public class OutageDetails
    {
        public string LeaveDate;
        public decimal LeaveOutage;

    }
    public class ListLeaveDetails
    {
        public string ReportType;
        public List<LeaveDetail> ReportList;
    }
}