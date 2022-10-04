using HashGenerator.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator.Shared.Services
{
    public sealed class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
