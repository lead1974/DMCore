using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Services
{
    public class GlobalService
    {

        private readonly ILogger _logger;

        public GlobalService(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GlobalService>();
        }
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("D");
        }

        public bool IsNullOrDefault<T>(T value)
        {
            return object.Equals(value, default(T));
        }
    }
}
