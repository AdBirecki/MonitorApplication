using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Controllers.BaseControllers
{
    public class ApiBaseController: ControllerBase
    {
        protected static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime myDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(unixTimeStamp/ticsDenomintor);
            DateTime localDate = myDate
                .ToLocalTime();
            return localDate;
        }

        private const int ticsDenomintor = 1000;
    }
}
