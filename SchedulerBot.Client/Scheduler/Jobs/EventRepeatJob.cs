﻿using System.Threading.Tasks;
using MediatR;
using Quartz;
using SchedulerBot.Application.Events.Commands.ApplyEventRepeat;
using SchedulerBot.Client.Scheduler.Data;

namespace SchedulerBot.Client.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class EventRepeatJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IEventScheduler _eventScheduler;

        public EventRepeatJob(IMediator mediator, IEventScheduler eventScheduler)
        {
            _mediator = mediator;
            _eventScheduler = eventScheduler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var data = new EventJobDataMap(context.MergedJobDataMap);

            var result = await _mediator.Send(new ApplyEventRepeatCommand
            {
                EventId = data.EventId
            });

            await _eventScheduler.ScheduleEvent(result, data.ShardClientId, data.ChannelId);
        }
    }
}
