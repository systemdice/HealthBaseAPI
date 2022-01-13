using System;

namespace MongoDB.GenericRepository.Utility
{
    public class CommonMethods
    {

        //string fullPath = HttpContext.Current.Server.MapPath(@"~/Resources/Log/LogActivity.txt");

        public static void LogEntry()
        {
            //File.AppendAllText(fullPath, DateTime.Now.ToString() + Environment.NewLine);
        }

        public static string GetUniqueID()
        {
            var UniqueID = DateTime.Now.Month.ToString()+ DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2)+ DateTime.Now.Second + DateTime.Now.Millisecond + (new Random().Next(10, 99) + DateTime.Now.Minute).ToString();
            return UniqueID;
        }
        public static string GetUniqueIDShorter()
        {
            var UniqueID = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + DateTime.Now.Millisecond + (new Random().Next(10, 99) + DateTime.Now.Minute).ToString();
            return UniqueID;
        }

        public static string CreateISTTimezone()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            return custDateTime.ToString("dd-MM-yyyy");
        }
    }
}
