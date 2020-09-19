using System;
using System.Text;

namespace Theater.Infra.Crosscutting.Extensions
{
    public static class IntegerExtensions
    {
        public static string FormatDurationFromMinutes(this int duration)
        {
            var ts = TimeSpan.FromMinutes(duration);
            var hour = ts.Hours;
            var minute = ts.Minutes;
            var sb = new StringBuilder();
            sb.Append(hour);
            sb.Append(":");
            sb.Append(minute.ToString("D2"));

            return sb.ToString();
        }
    }
}
