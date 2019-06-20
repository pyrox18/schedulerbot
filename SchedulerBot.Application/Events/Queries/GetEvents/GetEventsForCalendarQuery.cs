﻿using System.Collections.Generic;
using MediatR;
using SchedulerBot.Application.Events.Models;

namespace SchedulerBot.Application.Events.Queries.GetEvents
{
    public class GetEventsForCalendarQuery : IRequest<List<SimplifiedEventViewModel>>
    {
        public ulong CalendarId { get; set; }
    }
}