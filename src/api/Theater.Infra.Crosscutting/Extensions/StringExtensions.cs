using System;
using System.Text;

namespace Theater.Infra.Crosscutting.Extensions
{
    public static class StringExtensions
    {
        public static string FormatDurationFromMinutes(this string duration)
        {
            var ts = TimeSpan.FromMinutes(double.Parse(duration));
            var hour = ts.Hours;
            var minute = ts.Minutes;
            var sb = new StringBuilder();
            sb.Append(hour);
            sb.Append(":");
            sb.Append(minute.ToString("D2"));

            return sb.ToString();
        }

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
    }
}
