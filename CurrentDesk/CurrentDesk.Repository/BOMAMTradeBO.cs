using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.DAL;
using CurrentDesk.Models;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 6/11/2013 5:05:56 PM
//	   and this file should not get overridden 
//
//     Add your own data access methods.
// </auto-generated>
//------------------------------------------------------------------------------ 

namespace CurrentDesk.Repository.CurrentDesk
{
    /// <summary>
    /// Business Object For BOMAMTrade
    /// </summary>
    public class BOMAMTradeBO
    {
        /// <summary>
        /// This Function Will Get the last process Id of OpenTradesBO
        /// </summary>
        /// <returns></returns>
        public int GetLastOpenTradeProcessedID()
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var boMAMTradeRepo =
                        new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                    return ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context).
                        BOMAMTrades.Where(trade => trade.IsopenTrades == true).Max(trade => trade.LastIDProcessed);

                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }

        /// <summary>
        /// This Function will get last closed trade ProcessedID
        /// </summary>
        /// <returns></returns>
        public int GetLastClosedTradeProcessedID()
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var boMAMTradeRepo =
                        new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                    return ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context).
                        BOMAMTrades.Where(trade => trade.IsopenTrades == false).Max(trade => trade.LastIDProcessed);

                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }

        /// <summary>
        /// This Function Will Add Bulk Trade
        /// </summary>
        /// <param name="boMAMTradeList">boMAMTradeList</param>
        public void AddBOMAMOpenTrades(List<BOMAMTrade> boMAMTradeList)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var boMAMTradeRepo =
                        new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                    //Get The Context
                    var context = ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context);

                    //Loop Through all the Back Office Open Trades
                    foreach (var item in boMAMTradeList)
                    {
                        //Add all object to context                        
                        context.BOMAMTrades.AddObject(item);

                        //Get The Selected Client and measure the equity
                        var selectedClientAccount = context.Client_Account.Where(x => x.PK_ClientAccountID == item.FK_ClientAccountID).FirstOrDefault();
                        selectedClientAccount.Equity = selectedClientAccount.CurrentBalance +
                        (decimal)(GetProfitSummation(item.FK_ClientAccountID) + item.Pnl + item.Swap + item.Commission);
                    }

                    //Save Changes
                    context.SaveChanges();
                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Update BOMAMOpen trades to closed trades
        /// </summary>
        /// <param name="boMAMTradeList"></param>
        public void UpdateBOMAMTrade(List<BOMAMTrade> boMAMTradeList)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var boMAMTradeRepo =
                        new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                    //Get The Context
                    var context = ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context);

                    //Loop Through all the Back Office Open Trades
                    foreach (var item in boMAMTradeList)
                    {
                        //Add all object to context
                        var existingOpenTrades = context.BOMAMTrades.Where(x => x.OrderID == item.OrderID).FirstOrDefault();
                        if (existingOpenTrades != null)
                        {
                            existingOpenTrades.IsopenTrades = false;
                            existingOpenTrades.ClosePrice = item.ClosePrice;
                            existingOpenTrades.CloseTime = item.CloseTime;
                        }
                    }

                    //Save Changes
                    context.SaveChanges();
                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This function will update the closed trades
        /// </summary>
        /// <param name="assetManagerDict">assetManagerDict with login and slaves</param>
        /// <param name="lastProcessedID">lastProcessedID</param>
        /// <returns>int Last ProcessedID</returns>
        public int UpdateBOMAMOpenToCloseTrade(Dictionary<int, List<R_AssetManager_IntroducingBroker_ClientAccount>> assetManagerDict, int lastProcessedID)
        {
            //Get All The Closed trade List
            int lastProcessID = 0;
            var tradeHistoryBO = new TradesHistoryBO();

            try
            {
                //Get All the closed Tradde List
                var closedTradeList = tradeHistoryBO.GetAssetManagerClosedTrades(assetManagerDict.Keys.ToList(), lastProcessedID);

                //Check the count of the close tradelist
                if (closedTradeList.Count > 0)
                {
                    //Get the last processedID
                    lastProcessID = closedTradeList.Max(x => x.PK_TradeID);

                    using (var unitOfWork = new EFUnitOfWork())
                    {
                        var boMAMTradeRepo =
                            new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                        //Get The Context
                        var context = ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context);

                        //Loop Through all the Back Office Open Trades
                        foreach (var item in closedTradeList)
                        {
                            //Add all object to context
                            //If Trade Exists
                            var existingOpenTrades = context.BOMAMTrades.Where(x => x.OrderID == item.OrderID).ToList();

                            //Check if the count is graeter than 0 
                            if (existingOpenTrades.Count > 0)
                            {
                                foreach (var trd in existingOpenTrades)
                                {

                                    //Get the allocation Ratio of the slave and change the size
                                    var allocationRatio = assetManagerDict[(int)item.Login].
                                        Where(x => x.FK_ClientAccountID == trd.FK_ClientAccountID).
                                        Select(x => x.AllocationRatio).First();

                                    var existingPnl = trd.Pnl;
                                    var existingSwap = trd.Swap;
                                    var existingCommission = trd.Commission;

                                    trd.Size = (double)item.Volume * (double)allocationRatio;
                                    trd.Swap = item.Storage * (double)allocationRatio;
                                    trd.Commission = item.Commission * (double)allocationRatio;
                                    trd.Pnl = item.Profit * (double)allocationRatio;
                                    trd.IsopenTrades = false;
                                    trd.ClosePrice = item.ClosePrice;
                                    trd.CloseTime = item.CloseTime;
                                    trd.LastIDProcessed = item.PK_TradeID;

                                    /* When the trades get closed 
                                     * Update the current Balance And Than
                                     * Update the Equity
                                     */

                                    var selectedClientAccount = context.Client_Account.Where(x => x.PK_ClientAccountID == trd.FK_ClientAccountID).FirstOrDefault();
                                    selectedClientAccount.CurrentBalance = selectedClientAccount.CurrentBalance + (decimal)trd.Pnl;
                                    selectedClientAccount.Equity = selectedClientAccount.CurrentBalance +
                                        (decimal)(GetProfitSummation(trd.FK_ClientAccountID) - existingPnl - existingSwap - existingCommission);

                                }
                            }
                            else
                            {
                                var missingTrades = AddMissingOpenTrades(item, assetManagerDict[(int)item.Login]);
                                if (missingTrades != null)
                                {
                                    foreach (var missTrade in missingTrades)
                                    {
                                        context.BOMAMTrades.AddObject(missTrade);

                                        /* When the trades get closed 
                                        * Update the current Balance And Than
                                        * Update the Equity
                                        */
                                        var selectedClientAccount = context.Client_Account.Where(x => x.PK_ClientAccountID == missTrade.FK_ClientAccountID).FirstOrDefault();
                                        selectedClientAccount.CurrentBalance = selectedClientAccount.CurrentBalance + (decimal)missTrade.Pnl;
                                        selectedClientAccount.Equity = selectedClientAccount.CurrentBalance +
                                            (decimal)(GetProfitSummation(missTrade.FK_ClientAccountID) + missTrade.Swap + missTrade.Commission);
                                    }
                                }
                            }
                        }

                        //Save Changes
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return lastProcessID;
        }

        /// <summary>
        /// This Function Will rteturn the missing trades 
        /// </summary>
        /// <param name="trade">trade</param>
        /// <param name="slavelist">slavelist</param>
        /// <returns>List of BOMAM Trade</returns>
        public List<BOMAMTrade> AddMissingOpenTrades(TradesHistory trade, List<R_AssetManager_IntroducingBroker_ClientAccount> slavelist)
        {
            try
            {
                var boMAMTradeList = new List<BOMAMTrade>();

                //Checking the Count
                if (slavelist.Count > 0)
                {
                    //Loop through all the slavelist and create missing trade
                    foreach (var item in slavelist)
                    {
                        var boMAMTrade = new BOMAMTrade()
                        {
                            OrderID = (int)trade.OrderID,
                            FK_IBID = (int)item.FK_IBID,
                            FK_ClientAccountID = (int)item.FK_ClientAccountID,
                            OpenPrice = trade.OpenPrice,
                            ClosePrice = trade.ClosePrice,
                            OpenTime = trade.OpenTime,
                            CloseTime = trade.CloseTime,
                            Symbol = trade.Symbol,
                            Agent = "",
                            LastIDProcessed = trade.PK_TradeID,
                            Size = (double)trade.Volume * (double)item.AllocationRatio,
                            Swap = trade.Storage * item.AllocationRatio,
                            Commission = trade.Commission * item.AllocationRatio,
                            Pnl = trade.Profit * item.AllocationRatio,
                            IsopenTrades = false
                        };
                        boMAMTradeList.Add(boMAMTrade);
                    }
                }

                return boMAMTradeList;

            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }

        }

        /// <summary>
        /// This Function will calculate all the pnl 
        /// and return it Slave Account
        /// </summary>
        /// <param name="clientAccountID">clientAccountID</param>
        /// <returns>sum of pnl</returns>
        public double GetProfitSummation(int clientAccountID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var boMAMTradeRepo =
                        new BOMAMTradeRepository(new EFRepository<BOMAMTrade>(), unitOfWork);

                    return
                        ((CurrentDeskClientsEntities)boMAMTradeRepo.Repository.UnitOfWork.Context).BOMAMTrades.
                         Where(act => act.FK_ClientAccountID == clientAccountID && act.IsopenTrades == true).Sum(act => act.Pnl + act.Commission + act.Swap) ?? 0;
                }
            }
            catch (Exception exceptionMessage)
            {
                CommonErrorLogger.CommonErrorLog(exceptionMessage, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }

        }

    }
}