using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.Interfaces
{
    public interface IProcessScheduleExecutor : IDisposable
    {
        void GenerateSchedule();
        void GenerateSchedule(DateTime effectiveDate);
    }
}
