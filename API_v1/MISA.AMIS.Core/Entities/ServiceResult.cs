using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ServiceResult
    {
        public StatusCode Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
