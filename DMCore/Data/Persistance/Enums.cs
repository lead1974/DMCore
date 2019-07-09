using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data
{
    public static class c
    {
        public enum Status
        {
            Pending,
            Active,
            Deleted,
            Expired
        }

        public enum Views
        {
            Popular,
            Active,
            Expired
        }
    }
}
