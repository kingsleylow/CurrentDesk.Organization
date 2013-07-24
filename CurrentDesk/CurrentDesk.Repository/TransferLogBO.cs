using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.DAL;
using CurrentDesk.Models;
using System.Data.Objects;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 2/3/2013 1:36:06 PM
//	   and this file should not get overridden 
//
//     Add your own data access methods.
// </auto-generated>
//------------------------------------------------------------------------------ 
	
namespace CurrentDesk.Repository.CurrentDesk
{   
	public class TransferLogBO
	{
		// Add your own data access methods here.  If you wish to
		// expose your public method to a WCF service, marked them with
		// the attribute [NCPublish], and another T4 template will generate your service contract

        /// <summary>
        /// This method logs fund transfer details(Withdrawal/Deposit) in TransferLogs table
        /// </summary>
        /// <param name="fromAcc">fromAcc</param>
        /// <param name="toAcc">toAcc</param>
        /// <param name="fromCurrID">fromCurrID</param>
        /// <param name="toCurrID">toCurrID</param>
        /// <param name="amount">amount</param>
        /// <param name="exchangeRate">exchangeRate</param>
        public void AddTransferLogForTransaction(int pkTransactionID, string fromAcc, string toAcc, int fromCurrID, int toCurrID, double amount, double exchangeRate)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    if (fromCurrID != toCurrID)
                    {
                        //If from account is landing account
                        if (fromAcc.Split('-')[1] == "000")
                        {
                            TransferLog log1 = new TransferLog();
                            log1.CurrencyID = fromCurrID;
                            log1.Amount = Convert.ToDecimal(amount);
                            log1.TransactionType = "Withdrawal";
                            log1.AccountNumber = fromAcc;
                            log1.TransactionDateTime = DateTime.Now;
                            log1.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log1);
                        }
                        //If from account is trading account
                        else
                        {
                            //Withdrawal from trading acc
                            TransferLog log1 = new TransferLog();
                            log1.CurrencyID = fromCurrID;
                            log1.Amount = Convert.ToDecimal(amount);
                            log1.TransactionType = "Withdrawal";
                            log1.AccountNumber = fromAcc;
                            log1.TransactionDateTime = DateTime.Now;
                            log1.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log1);

                            //Deposit in landing account
                            TransferLog log6 = new TransferLog();
                            log6.CurrencyID = fromCurrID;
                            log6.Amount = Convert.ToDecimal(amount);
                            log6.TransactionType = "Deposit";
                            log6.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                            log6.TransactionDateTime = DateTime.Now;
                            log6.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log6);

                            //Withdrawal from landing account
                            TransferLog log7 = new TransferLog();
                            log7.CurrencyID = fromCurrID;
                            log7.Amount = Convert.ToDecimal(amount);
                            log7.TransactionType = "Withdrawal";
                            log7.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                            log7.TransactionDateTime = DateTime.Now;
                            log7.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log7);
                        }

                        //If to account is landing account
                        if (toAcc.Split('-')[1] == "000")
                        {
                            TransferLog log2 = new TransferLog();
                            log2.CurrencyID = toCurrID;
                            log2.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log2.TransactionType = "Deposit";
                            log2.AccountNumber = toAcc;
                            log2.TransactionDateTime = DateTime.Now;
                            log2.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log2);
                        }
                        //If to account is trading account
                        else
                        {
                            //Deposit in landing account
                            TransferLog log3 = new TransferLog();
                            log3.CurrencyID = toCurrID;
                            log3.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log3.TransactionType = "Deposit";
                            log3.AccountNumber = toAcc.Split('-')[0] + "-" + "000" + "-" + toAcc.Split('-')[2];
                            log3.TransactionDateTime = DateTime.Now;
                            log3.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log3);

                            //Withdrawal from landing account
                            TransferLog log4 = new TransferLog();
                            log4.CurrencyID = toCurrID;
                            log4.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log4.TransactionType = "Withdrawal";
                            log4.AccountNumber = toAcc.Split('-')[0] + "-" + "000" + "-" + toAcc.Split('-')[2];
                            log4.TransactionDateTime = DateTime.Now;
                            log4.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log4);

                            //Deposit in trading account
                            TransferLog log5 = new TransferLog();
                            log5.CurrencyID = toCurrID;
                            log5.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log5.TransactionType = "Deposit";
                            log5.AccountNumber = toAcc;
                            log5.TransactionDateTime = DateTime.Now;
                            log5.FK_TransactionID = pkTransactionID;
                            transferLogRepo.Add(log5);
                        }
                    }
                    else
                    {
                        //Withdrawal from acc
                        TransferLog log1 = new TransferLog();
                        log1.CurrencyID = fromCurrID;
                        log1.Amount = Convert.ToDecimal(amount);
                        log1.TransactionType = "Withdrawal";
                        log1.AccountNumber = fromAcc;
                        log1.TransactionDateTime = DateTime.Now;
                        log1.FK_TransactionID = pkTransactionID;
                        transferLogRepo.Add(log1);

                        //Deposit to account
                        TransferLog log2 = new TransferLog();
                        log2.CurrencyID = toCurrID;
                        log2.Amount = Convert.ToDecimal(amount);
                        log2.TransactionType = "Deposit";
                        log2.AccountNumber = toAcc;
                        log2.TransactionDateTime = DateTime.Now;
                        log2.FK_TransactionID = pkTransactionID;
                        transferLogRepo.Add(log2);
                    }

                    transferLogRepo.Save();
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method logs fund transfer details(Withdrawal/Deposit) in TransferLogs table
        /// </summary>
        /// <param name="fromAcc">fromAcc</param>
        /// <param name="toAcc">toAcc</param>
        /// <param name="fromCurrID">fromCurrID</param>
        /// <param name="toCurrID">toCurrID</param>
        /// <param name="amount">amount</param>
        /// <param name="exchangeRate">exchangeRate</param>
        /// <param name="organizationID">organizationID</param>
        public void AddTransferLogForTransaction(int pkTransactionID, string fromAcc, string toAcc, int fromCurrID, int toCurrID, double amount, double exchangeRate, int organizationID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    if (fromCurrID != toCurrID)
                    {
                        //If from account is landing account
                        if (fromAcc.Split('-')[1] == "000")
                        {
                            TransferLog log1 = new TransferLog();
                            log1.CurrencyID = fromCurrID;
                            log1.Amount = Convert.ToDecimal(amount);
                            log1.TransactionType = "Withdrawal";
                            log1.AccountNumber = fromAcc;
                            log1.TransactionDateTime = DateTime.Now;
                            log1.FK_TransactionID = pkTransactionID;
                            log1.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log1);
                        }
                        //If from account is trading account
                        else
                        {
                            //Withdrawal from trading acc
                            TransferLog log1 = new TransferLog();
                            log1.CurrencyID = fromCurrID;
                            log1.Amount = Convert.ToDecimal(amount);
                            log1.TransactionType = "Withdrawal";
                            log1.AccountNumber = fromAcc;
                            log1.TransactionDateTime = DateTime.Now;
                            log1.FK_TransactionID = pkTransactionID;
                            log1.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log1);

                            //Deposit in landing account
                            TransferLog log6 = new TransferLog();
                            log6.CurrencyID = fromCurrID;
                            log6.Amount = Convert.ToDecimal(amount);
                            log6.TransactionType = "Deposit";
                            log6.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                            log6.TransactionDateTime = DateTime.Now;
                            log6.FK_TransactionID = pkTransactionID;
                            log6.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log6);

                            //Withdrawal from landing account
                            TransferLog log7 = new TransferLog();
                            log7.CurrencyID = fromCurrID;
                            log7.Amount = Convert.ToDecimal(amount);
                            log7.TransactionType = "Withdrawal";
                            log7.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                            log7.TransactionDateTime = DateTime.Now;
                            log7.FK_TransactionID = pkTransactionID;
                            log7.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log7);
                        }

                        //If to account is landing account
                        if (toAcc.Split('-')[1] == "000")
                        {
                            TransferLog log2 = new TransferLog();
                            log2.CurrencyID = toCurrID;
                            log2.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log2.TransactionType = "Deposit";
                            log2.AccountNumber = toAcc;
                            log2.TransactionDateTime = DateTime.Now;
                            log2.FK_TransactionID = pkTransactionID;
                            log2.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log2);
                        }
                        //If to account is trading account
                        else
                        {
                            //Deposit in landing account
                            TransferLog log3 = new TransferLog();
                            log3.CurrencyID = toCurrID;
                            log3.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log3.TransactionType = "Deposit";
                            log3.AccountNumber = toAcc.Split('-')[0] + "-" + "000" + "-" + toAcc.Split('-')[2];
                            log3.TransactionDateTime = DateTime.Now;
                            log3.FK_TransactionID = pkTransactionID;
                            log3.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log3);

                            //Withdrawal from landing account
                            TransferLog log4 = new TransferLog();
                            log4.CurrencyID = toCurrID;
                            log4.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log4.TransactionType = "Withdrawal";
                            log4.AccountNumber = toAcc.Split('-')[0] + "-" + "000" + "-" + toAcc.Split('-')[2];
                            log4.TransactionDateTime = DateTime.Now;
                            log4.FK_TransactionID = pkTransactionID;
                            log4.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log4);

                            //Deposit in trading account
                            TransferLog log5 = new TransferLog();
                            log5.CurrencyID = toCurrID;
                            log5.Amount = Math.Round((Convert.ToDecimal(amount * exchangeRate)), 2);
                            log5.TransactionType = "Deposit";
                            log5.AccountNumber = toAcc;
                            log5.TransactionDateTime = DateTime.Now;
                            log5.FK_TransactionID = pkTransactionID;
                            log5.FK_OrganizationID = organizationID;
                            transferLogRepo.Add(log5);
                        }
                    }
                    else
                    {
                        //Withdrawal from acc
                        TransferLog log1 = new TransferLog();
                        log1.CurrencyID = fromCurrID;
                        log1.Amount = Convert.ToDecimal(amount);
                        log1.TransactionType = "Withdrawal";
                        log1.AccountNumber = fromAcc;
                        log1.TransactionDateTime = DateTime.Now;
                        log1.FK_TransactionID = pkTransactionID;
                        log1.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log1);

                        //Deposit to account
                        TransferLog log2 = new TransferLog();
                        log2.CurrencyID = toCurrID;
                        log2.Amount = Convert.ToDecimal(amount);
                        log2.TransactionType = "Deposit";
                        log2.AccountNumber = toAcc;
                        log2.TransactionDateTime = DateTime.Now;
                        log2.FK_TransactionID = pkTransactionID;
                        log2.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log2);
                    }

                    transferLogRepo.Save();
                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method logs fee transfer details in TransferLogs table
        /// </summary>
        /// <param name="fromAcc">fromAcc</param>
        /// <param name="fromCurrID">fromCurrID</param>
        /// <param name="toCurrID">toCurrID</param>
        /// <param name="fee">fee</param>
        /// <param name="organizationID">organizationID</param>
        public void AddTransferLogForFee(int pkTransactionID, string fromAcc, int fromCurrID, int toCurrID, double fee, int organizationID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    //If from account is landing account
                    if (fromAcc.Split('-')[1] == "000")
                    {
                        TransferLog log1 = new TransferLog();
                        log1.CurrencyID = fromCurrID;
                        log1.Amount = Convert.ToDecimal(fee);
                        log1.TransactionType = "Withdrawal";
                        log1.AccountNumber = fromAcc;
                        log1.TransactionDateTime = DateTime.Now;
                        log1.FK_TransactionID = pkTransactionID;
                        log1.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log1);
                    }
                    //If from account is trading account
                    else
                    {
                        //Withdrawal from trading acc
                        TransferLog log1 = new TransferLog();
                        log1.CurrencyID = fromCurrID;
                        log1.Amount = Convert.ToDecimal(fee);
                        log1.TransactionType = "Withdrawal";
                        log1.AccountNumber = fromAcc;
                        log1.TransactionDateTime = DateTime.Now;
                        log1.FK_TransactionID = pkTransactionID;
                        log1.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log1);

                        //Deposit in landing account
                        TransferLog log6 = new TransferLog();
                        log6.CurrencyID = fromCurrID;
                        log6.Amount = Convert.ToDecimal(fee);
                        log6.TransactionType = "Deposit";
                        log6.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                        log6.TransactionDateTime = DateTime.Now;
                        log6.FK_TransactionID = pkTransactionID;
                        log6.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log6);

                        //Withdrawal from landing account
                        TransferLog log7 = new TransferLog();
                        log7.CurrencyID = fromCurrID;
                        log7.Amount = Convert.ToDecimal(fee);
                        log7.TransactionType = "Withdrawal";
                        log7.AccountNumber = fromAcc.Split('-')[0] + "-" + "000" + "-" + fromAcc.Split('-')[2];
                        log7.TransactionDateTime = DateTime.Now;
                        log7.FK_TransactionID = pkTransactionID;
                        log7.FK_OrganizationID = organizationID;
                        transferLogRepo.Add(log7);
                    }
                    transferLogRepo.Save();
                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method gets latest 3 transactions of the account number
        /// </summary>
        /// <param name="accountNumber">accountNumber</param>
        /// <param name="organizationID">organizationID</param>
        /// <returns></returns>
        public List<TransferLog> GetLatestTransactionsForAccount(string accountNumber, int organizationID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    //Creating TransferLog ObjectSet to Query
                    ObjectSet<TransferLog> transferLogObjSet =
                      ((CurrentDeskClientsEntities)transferLogRepo.Repository.UnitOfWork.Context).TransferLogs;

                    //Return latest 3 transactions
                    return transferLogObjSet.Where(log => log.AccountNumber == accountNumber && log.FK_OrganizationID == organizationID).OrderByDescending(log => log.TransactionDateTime).Take(3).ToList();
                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This methods inserts transfer log for fund deposit transaction
        /// </summary>
        /// <param name="pkTransactionID">pkTransactionID</param>
        /// <param name="currID">currID</param>
        /// <param name="amount">amount</param>
        /// <param name="accountNumber">accountNumber</param>
        /// <param name="organizationID">organizationID</param>
        public void AddTransferLogForFundDeposit(int pkTransactionID, int currID, decimal amount, string accountNumber, int organizationID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    TransferLog newDeposit = new TransferLog();
                    newDeposit.CurrencyID = currID;
                    newDeposit.Amount = amount;
                    newDeposit.TransactionType = "Deposit";
                    newDeposit.AccountNumber = accountNumber;
                    newDeposit.TransactionDateTime = DateTime.UtcNow;
                    newDeposit.FK_TransactionID = pkTransactionID;
                    newDeposit.FK_OrganizationID = organizationID;

                    transferLogRepo.Add(newDeposit);
                    transferLogRepo.Save();
                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This methods inserts transfer log for fund withdraw transaction
        /// </summary>
        /// <param name="pkTransactionID">pkTransactionID</param>
        /// <param name="currID">currID</param>
        /// <param name="amount">amount</param>
        /// <param name="accountNumber">accountNumber</param>
        /// <param name="organizationID">organizationID</param>
        public void AddTransferLogForFundWithdraw(int pkTransactionID, int currID, decimal amount, string accountNumber, int organizationID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var transferLogRepo =
                     new TransferLogRepository(new EFRepository<TransferLog>(), unitOfWork);

                    TransferLog newWithdraw = new TransferLog();
                    newWithdraw.CurrencyID = currID;
                    newWithdraw.Amount = amount;
                    newWithdraw.TransactionType = "Withdraw";
                    newWithdraw.AccountNumber = accountNumber;
                    newWithdraw.TransactionDateTime = DateTime.UtcNow;
                    newWithdraw.FK_TransactionID = pkTransactionID;
                    newWithdraw.FK_OrganizationID = organizationID;

                    transferLogRepo.Add(newWithdraw);
                    transferLogRepo.Save();
                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
	}
}