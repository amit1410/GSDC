using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace GSDC.App_Code
{
    public class CommonMethods
    {
        public const int SL_AppliedBefore = 0;
        public const int CL_AppliedBefore = 0;
        public const int EL_AppliedBefore = 7;
        public const int Comp_AppliedBefore = 7;
        public CommonMethods()
        {

        }
        public void SendEmailBackgroundThread(Object mailMsg)
        {
            MailMessage mailMessage = (MailMessage)mailMsg;
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.domain.global", 25);
                smtpClient.EnableSsl = true;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);


            }
            catch (Exception ex)
            {
            }
        }
        public static string LeaveDetails(string leaveId, string leaveType, DateTime startDate, DateTime endDate)
        {
            string strLeaveDetails = "</br></br><b>Below is your leave request details.</b></br></br>";
            strLeaveDetails += "<div style='line-height: 2px'> Leave Request ID - " + leaveId + "</br>";
            strLeaveDetails += "Type of Leave - " + leaveType + "</br>";
            strLeaveDetails += "Start Date - " + String.Format("{0:dddd, MMMM d, yyyy}", startDate) + "</br>";
            strLeaveDetails += "End Date - " + String.Format("{0:dddd, MMMM d, yyyy}", endDate) + "</div></br>";
            return strLeaveDetails;
        }
        public void SendEmail(string strFrom, string strTo, string strSubject, string strBody)
        {
            strFrom = strFrom + ConfigurationManager.AppSettings["domain"].ToString();
            strTo = strTo + ConfigurationManager.AppSettings["domain"].ToString();
            strBody = "<div style='font-family:Tahoma;font-size:10pt'>" + strBody + "</div>";
            MailMessage message = new MailMessage("ServiceManagement@CHPVS00298.cloudapp.net", strTo, strSubject, strBody);
            message.IsBodyHtml = true;

            Thread bgThread = new Thread(new ParameterizedThreadStart(SendEmailBackgroundThread));
            bgThread.IsBackground = true;

            bgThread.Start(message);

        }
        public static int LeaveDaysCount(DateTime startDate, DateTime endDate, Boolean excludeWeekends, List<DateTime> excludeDates)
        {
            int count = 0;
            for (DateTime index = startDate; index <= endDate; index = index.AddDays(1))
            {
                if (excludeWeekends && index.DayOfWeek != DayOfWeek.Sunday && index.DayOfWeek != DayOfWeek.Saturday)
                {
                    bool excluded = false; ;
                    if (excludeDates.Count() > 0)
                    {
                        for (int i = 0; i < excludeDates.Count; i++)
                        {
                            if (index.Date.CompareTo(excludeDates[i].Date) == 0)
                            {
                                excluded = true;
                                break;
                            }
                        }
                    }
                    if (!excluded)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        // public Status RequestStatus { get; set; }


        public static DataTable Import_To_Grid(string FilePath, string Extension, string isHDR)
        {

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            return dt;

        }

        public enum Status
        {
            Cancelled,
            Pending,
            Approved,
            Rejected
        }
    }
}