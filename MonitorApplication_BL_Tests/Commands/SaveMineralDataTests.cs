using MonitorApplication_BL.Commands.Commands;
using MonitorApplication_Models;
using NUnit.Framework;
using System;
using MonitorApplication_Utilities.Helpers;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL_Tests
{
    [TestFixture]
    class SaveMineralDataTests
    {
        ChangeDto validChangeDto;
        ChangeDto invalidChangeDto;

        [SetUp]
        public void SetUp()
        {  
            /*I have decided to instantiate changeDto's here. 
            In perfect world these would be read only bu since SetUp is not a constructor that is the way it is. */
            #region setting up changeDto's
            validChangeDto = new ChangeDto
            {
                Currency = "USD",
                XauPrice = 100,
                XagPrice = 10,
                ChgXau = 2,
                ChgXag = 1,
                XauClose = 90,
                XagClose = 9
            };
            invalidChangeDto = new ChangeDto
            {
                XauPrice = 100,
                XagPrice = 10,
                ChgXau = 2,
                ChgXag = 1,
                XauClose = 90,
                XagClose = 9
            };
            #endregion
        }

        [Test]
        public void CheckValidTimeStamp() {

            double validTimeStamp = DateTime.Now.AddSeconds(-30).GetDateTimeNoEpoch();
            SaveMineralDataCommand command = new SaveMineralDataCommand(validChangeDto, validTimeStamp);
            bool isValid = command.IsValid();

            Assert.IsTrue(isValid);
            Assert.IsNotNull(validChangeDto);

        }

        [Test]
        public void CheckInvalidTimeStamp() {

            double invalidTimeStamp = DateTime.Now
                .AddMinutes(-2)
                .GetDateTimeNoEpoch();

            SaveMineralDataCommand command = new SaveMineralDataCommand(validChangeDto, invalidTimeStamp);
            bool isValid = command.IsValid();

            Assert.IsFalse(isValid);
            Assert.IsNotNull(validChangeDto);
        }

        [Test]
        public void CheckInvalidChangeDto() {
            double validTimeStamp = DateTime.Now
                .GetDateTimeNoEpoch();

            SaveMineralDataCommand command = new SaveMineralDataCommand(invalidChangeDto, validTimeStamp);
            bool isValid = command.IsValid();

            Assert.IsFalse(isValid);
            Assert.IsNotNull(invalidChangeDto);
            Assert.IsNull(invalidChangeDto.Currency);
            Assert.IsNull(command.PriceData.Currency);
        }
    }
}
