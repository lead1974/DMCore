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
            Popular0,
            Active,
            Expired
        }
    }
    public static class RoleName
    {
        public const string CanManageSite = "CanManageSite";
        public const string CanManageInvoices = "CanManageInvoices";
        public const string CanManageCustomers = "CanManageCustomers";
    }
}
