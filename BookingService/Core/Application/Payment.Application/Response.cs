using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public abstract class Response
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public ErrorCodesEnum ErrorCode { get; set; }

    }
}
