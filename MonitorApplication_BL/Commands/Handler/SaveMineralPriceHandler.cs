﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Commands.Commands;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.Units;
using MonitorApplication_USERS_DAL.Contexts;
using MonitorApplication_Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Commands.Handler
{
    public class SaveMineralPriceHandler : ICommandHandler<SaveMineralDataCommand>
    {
        private readonly OrdersDbContext _orderDbContext;
        private readonly ILogger _logger;
        private const string handlerName = nameof(SaveMineralPriceHandler);

        public SaveMineralPriceHandler(
            ILoggerFactory loggerFactory,
            OrdersDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
            _logger = loggerFactory.CreateLogger<SaveMineralPriceHandler>();
        }

        public void Execute(SaveMineralDataCommand command)
        {
            /* In ordinary case I would use AutoMapper to handle type conversion.
             * Due to time limitations we need to live with this solution. */
            MineralPriceData mineralPriceData = new MineralPriceData()
            {
                GoldPrice = command.PriceData.XagPrice,
                GoldChange = command.PriceData.ChgXau,
                SilverPrice = command.PriceData.XauPrice,
                SilverChange = command.PriceData.ChgXag,
                GoldClose = command.PriceData.XauClose,
                SilverClose = command.PriceData.XagClose,
                GoldPcExchange = command.PriceData.PcXau,
                SilverPcExchange = command.PriceData.PcXag,
                Currency = ConvertToEnumCurrency(command.PriceData.Currency),
                TimeOfRegistration = command.PriceDataTimestamp.GetDateTime(),
                Timestamp =command.PriceDataTimestamp
            };

            try
            {
                _orderDbContext.MineralPriceData.Add(mineralPriceData);
                _orderDbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                _logger.Log(LogLevel.Error, $" {handlerName} caused an DbUpdateException { exception.Message} ");
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $" {handlerName} caused an Exception { exception.Message} ");
                throw;
            }
        }


        // In ordinary case AutoMapper would handle this.
        private Currency ConvertToEnumCurrency(string curr)
        {
            Currency resultingCurrency = Currency.USD;
            switch (curr)
            {
                case CurrencyNames.USD:
                    resultingCurrency = Currency.USD;
                    break;
                case CurrencyNames.PLN:
                    resultingCurrency = Currency.PLN;
                    break;
                case CurrencyNames.EUR:
                    resultingCurrency = Currency.EUR;
                    break;
                default:
                    break;
            }
            return resultingCurrency;
        }
    }
}
