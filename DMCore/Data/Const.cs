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
    public static class RoleName
    {
        public const string CanManageSite = "CanManageSite";
        public const string CanManageInvoices = "CanManageInvoices";
        public const string CanManageCustomers = "CanManageCustomers";
    }
}
