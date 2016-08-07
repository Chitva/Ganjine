using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
namespace WebUI.Infrastructure.Utility
{
    public class DateTimeUtility
    {
        //تبدیل تاریخ میلادی به شمسی
        public string MiladiToShamsi(DateTime? Date)
        {
            DateTime Date1 = Convert.ToDateTime(Date);
            DateTime d = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            StringBuilder ShamsiDate = new StringBuilder();
            if (Date != null)
            {
                ShamsiDate.Append(pc.GetYear(Date1).ToString("0000"));
                ShamsiDate.Append("/");
                ShamsiDate.Append(pc.GetMonth(Date1).ToString("00"));
                ShamsiDate.Append("/");
                ShamsiDate.Append(pc.GetDayOfMonth(Date1).ToString("00"));
            }
            return ShamsiDate.ToString();
        }
        //ایجاد آرایه ای از تارخ برای مشخص کردن تاریخ های تقویم
        public string CreateArrayDate(IQueryable<DateTime> Date, DateTime MinDate, DateTime MaxDate)
        {
            TimeSpan Compare = (MaxDate).Subtract(MinDate);
            Double DiffrentDays = Compare.TotalDays;
            string ArrayDate = "";
            int Days = Convert.ToInt32(DiffrentDays);
            if ((DiffrentDays / Days != 0) && (Days < DiffrentDays))
                Days++;
            bool Check = false;
            for (int i = 1; i <= Days; i++)
            {
                Check = false;
                foreach (var item in Date)
                {
                    if (MinDate.Date == item)
                    {
                        Check = true;
                        break;
                    }
                }
                if (Check == false)
                {
                    PersianCalendar pc = new PersianCalendar();
                    if (ArrayDate != "")
                    {
                        ArrayDate = ArrayDate + "-";
                    }
                    ArrayDate = ArrayDate + pc.GetDayOfMonth(MinDate).ToString("00") + ", " + pc.GetMonth(MinDate).ToString("00") + ", " + pc.GetYear(MinDate).ToString("0000");
                }
                MinDate = MinDate.AddDays(1);
            }
            return ArrayDate;
        }
        private int GetDayOfWeek(string Day)
        {
            switch (Day)
            {
                case "Saterday":
                    return 1;
                case "Sunday":
                    return 2;
                case "Monday":
                    return 3;
                case "Tuesday":
                    return 4;
                case "Wednesday":
                    return 5;
                case "Thursday":
                    return 6;
                case "Friday":
                    return 7;
                case "EveryDay":
                    return 8;
                default:
                    return 0;
            }
        }
        public List<DateTime> GetDate(string Day, DateTime MinDate, DateTime MaxDate)
        {
            List<DateTime> Result = new List<DateTime>();
            int DayOfWeek = GetDayOfWeek(Day);
            int NumberDay = GetDayOfWeek(MinDate.DayOfWeek.ToString());
            if (DayOfWeek == 8)
            {
                int SubDate = MaxDate.Subtract(MinDate).Days;
                for (int i = 0; i <= SubDate; i++)
                {
                    if (MaxDate >= MinDate)
                    {
                        Result.Add(MinDate);
                        MinDate = MinDate.AddDays(1);
                    }
                }
            }
            else if (DayOfWeek != 0)
            {
                if (DayOfWeek > NumberDay)
                {
                    MinDate = MinDate.AddDays(7 - (NumberDay - DayOfWeek));
                }
                else
                    MinDate = MinDate.AddDays(NumberDay - DayOfWeek);
                if (MaxDate >= MinDate)
                {
                    int SubDate = MaxDate.Subtract(MinDate).Days;
                    int CountDay = (SubDate / 7) + 1;
                    if (SubDate % 7 == 0)
                    {
                        CountDay = SubDate / 7;
                    }
                    for (int i = 0; i < CountDay; i++)
                    {
                        if (MaxDate >= MinDate)
                        {
                            Result.Add(MinDate);
                            MinDate = MinDate.AddDays(7);
                        }
                    }
                }
            }
            return Result;
        }
    }
}