﻿using System.Threading.Tasks;
using SchedulerBot.Data.Models;

namespace SchedulerBot.Data.Services
{
    public interface ICalendarService
    {
        Task<Calendar> CreateCalendarAsync(Calendar calendar);
        Task<bool> DeleteCalendarAsync(ulong calendarId);
        Task<string> GetCalendarPrefixAsync(ulong calendarId);
        Task<string> UpdateCalendarPrefixAsync(ulong calendarId, string newPrefix);
        Task<ulong> GetCalendarDefaultChannelAsync(ulong calendarId);
        Task<ulong> UpdateCalendarDefaultChannelAsync(ulong calendarId, ulong newDefaultChannel);
        Task<string> GetCalendarTimezoneAsync(ulong calendarId);
        Task<string> UpdateCalendarTimezoneAsync(ulong calendarId, string newTimezone);
        Task<bool?> InitialiseCalendar(ulong calendarId, string timezone, ulong defaultChannelId);
        Task<Calendar> TryGetCalendarAsync(ulong calendarId);
    }
}
