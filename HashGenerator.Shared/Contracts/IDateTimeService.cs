using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator.Shared.Contracts
{
    public interface IDateTimeService
    {
        /// <summary>
        /// Returns the Current Date
        /// </summary>
        DateTime Now { get; }
    }
}
