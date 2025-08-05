namespace VesteTemplateWorker.Shared.Helpers;

public static class DateTimeExtensions
{
    public static DateTime GetGmtDateTime()
    {
        DateTime dataAtual = DateTime.UtcNow;
        TimeZoneInfo timeZoneBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(dataAtual, timeZoneBrasilia);
    }
}