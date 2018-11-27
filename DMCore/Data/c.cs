using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data
{
    public static class c
    {
        public enum DealStatus
        {
            Pending = 0,
            Active = 1,
            Expired = 9
        }

        public enum Views
        {
            Popular = 0,
            Active = 1,
            Expired = 9
        }
    }
}
