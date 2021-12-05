using Honoured.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Honoured.BackGroundJobs
{
    public class PlacementProcessor : PeriodicBackgroundWorkerBase
    {

        #region Ctors
        public PlacementProcessor(AbpTimer timer, IServiceScopeFactory serviceScopeFactory,
            IServiceProvider svcProvider) : base(timer, serviceScopeFactory)
        {
            _ = svcProvider.GetRequiredService<HonouredAppService>();
            timer.Period = HonouredAppService.SchedulGenerationInterval * 1000;
        }
        #endregion


        #region Implementations
        protected override void DoWork(PeriodicBackgroundWorkerContext workerContext)
        {
            using var exec = workerContext.ServiceProvider.GetRequiredService<IProcessScheduleExecutor>();
            exec.GenerateSchedule();
        }
        #endregion Implementations

    }
}
