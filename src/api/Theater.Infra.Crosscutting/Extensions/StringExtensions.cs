using System;
using System.IO;
using System.Text;

namespace Theater.Infra.Crosscutting.Extensions
{
    public static class StringExtensions
    {
        public static string FormatDurationFromHourMinute(this string duration)
        {
            var spplitedDuration = duration.Split(":");
            var sb = new StringBuilder();
            sb.Append(spplitedDuration[0]);
            sb.Append("h");
            sb.Append(" ");
            sb.Append(spplitedDuration[1]);
            sb.Append("m");

            return sb.ToString();
        }

        public static string ConvertFilePathToBase64(this string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);

            return Convert.ToBase64String(bytes);
        }
    }
}
