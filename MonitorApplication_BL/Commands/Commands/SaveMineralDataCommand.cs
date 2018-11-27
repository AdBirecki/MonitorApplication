using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_Models;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Commands.Commands
{
    public class SaveMineralDataCommand: ICommand
    {
        public ChangeDto PriceData;
        public long PriceDataTimestamp;

        public SaveMineralDataCommand() {
        }

        public SaveMineralDataCommand(ChangeDto mineralChange, long timestamp) {
            PriceData = mineralChange;
            PriceDataTimestamp = timestamp;
        }
    }
}
