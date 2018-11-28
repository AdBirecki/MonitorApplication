using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_Models;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.Units;
using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_Utilities.Helpers;

namespace MonitorApplication_BL.Commands.Commands
{
    public class SaveMineralDataCommand: ICommand
    {
        public ChangeDto PriceData;
        public double PriceDataTimestamp;

        public SaveMineralDataCommand(ChangeDto mineralChange, double timestamp) {
            PriceData = mineralChange;
            PriceDataTimestamp = timestamp;
        }

        /* I assume that time difference between request timestamp and current time should be less than a minute. 
         * In reality it should be less than that.  There is always a possibility that remote 
         * server starts to supply us with invalid (old) data.*/
        public bool IsValid()
        {
            bool validationResult = false;
            DateTime timestampDateTime = PriceDataTimestamp.GetDateTime();
            DateTime timeNow = DateTime.Now;

            double minutesDiff = (timeNow - timestampDateTime).TotalMinutes;

            if ( PriceData != null 
                && PriceData.Currency != null 
                && minutesDiff < 1 
                && minutesDiff >= 0)
            {
                validationResult = true;
            }

            return validationResult;
        }
    }
}
