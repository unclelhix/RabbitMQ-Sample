using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator.Shared.ResponseWrapper
{
    public class ServiceResponse<T>
    {
        public T Hashes { get; set; }
    }
}
