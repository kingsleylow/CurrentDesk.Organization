using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.DAL;
using CurrentDesk.Models;
using System.Data.Objects;
using CurrentDesk.Common;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 4/24/2013 2:53:15 PM
//	   and this file should not get overridden 
//
//     Add your own data access methods.
// </auto-generated>
//------------------------------------------------------------------------------ 

namespace CurrentDesk.Repository.CurrentDesk
{
    public class UserRecordBO
    {

        private int GroupType = 2;

        // Add your own data access methods here.  If you wish to
        // expose your public method to a WCF service, marked them with
        // the attribute [NCPublish], and another T4 template will generate your service contract

        //Synch Trades table
        public void SaveUserRecord(List<UserRecord> lstUserRecord)
        {
            try
            {


                using (var unitOfWork = new EFUnitOfWork())
                {
                    var userRecordRepo =
                    new UserRecordRepository(new EFRepository<UserRecord>(), unitOfWork);

                    ObjectSet<UserRecord> userRecordObj =
           ((CurrentDeskClientsEntities)userRecordRepo.Repository.UnitOfWork.Context).UserRecords;

                    var lstDBUserRecord = userRecordObj.ToList();



                    var lstNewUserRecord = lstUserRecord.Where(nt => !lstDBUserRecord.Select(db => db.Login).Contains(nt.Login)).ToList();
                    foreach (var trades in lstNewUserRecord)
                    {
                        userRecordRepo.Add(trades);
                    }

                    // Delete close trades
                    var lstOldDBUserRecord = lstDBUserRecord.Where(od => !lstUserRecord.Select(db => db.Login).Contains(od.Login)).ToList();
                    foreach (var od in lstOldDBUserRecord)
                    {
                        userRecordRepo.Delete(od);
                    }

                    userRecordRepo.Save();

                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        //Get Credit amount
        public double CreditAmount(int logingid)
        {
            double credit = 0;
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {

                    var userRecordRepo =
                   new UserRecordRepository(new EFRepository<UserRecord>(), unitOfWork);

                    ObjectSet<UserRecord> userRecordObj =
           ((CurrentDeskClientsEntities)userRecordRepo.Repository.UnitOfWork.Context).UserRecords;


                    credit = userRecordObj.Where(u => u.Login == logingid).Select(s => s.Credit ?? 0).FirstOrDefault();

                }
            }
            catch (Exception exception)
            {
                CommonErrorLogger.CommonErrorLog(exception, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return credit;
        }

        /// <summary>
        /// Added Client Records for Migrated users
        /// </summary>
        public void AddClientRecord()
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {

                    var userRecordRepo = new UserRecordRepository(new EFRepository<UserRecord>(), unitOfWork);


                    var context = (CurrentDeskClientsEntities)unitOfWork.Context;

                    //2 is used for grouping              
                    int cnt = 0;
                    while (cnt <= GroupType)
                    {

                        List<UserRecord> lstUserRecordsDB = new List<UserRecord>();
                        List<GroupUserRecord> lstUserRecordGrp = null;

                        if (cnt == 0)
                        {
                            //Get All UserRecords from DB
                            lstUserRecordsDB = (from u in context.UserRecords
                                                let ur = from em in context.Users select em.UserEmailID
                                                where ur.Contains(u.Email) == false && !(u.Email == null || u.Email.Equals(""))
                                                select u).ToList();


                            //Group userrecords by Name,Email and Address
                            var lstUserRecordGrpTemp = lstUserRecordsDB.GroupBy(cm => new { cm.Email },
                              (key, group) => new
                              {
                                  Email = key.Email
                              }).ToList();

                            //Create list by adding name and address
                            lstUserRecordGrp = (from email in lstUserRecordGrpTemp
                                                select new GroupUserRecord
                                                {

                                                    Email = email.Email,
                                                    Address = string.Empty,
                                                    Name = string.Empty
                                                }).ToList();


                        }
                        else
                        {
                            lstUserRecordsDB = (from u in context.UserRecords
                                                let ur = from em in context.Client_Account where em.IsTradingAccount == true select em.PlatformLogin
                                                where ur.Contains(u.Login) == false && (u.Email == null || u.Email.Equals(""))
                                                select u).ToList();

                            //Get all User without email
                            //lstUserRecordsDB = (from u in context.UserRecords
                            //                    where (u.Email == null || u.Email.Equals(""))
                            //                    select u).ToList();


                            //Group userrecords by Name,Email and Address
                            var lstUserRecordGrpTempName = lstUserRecordsDB.GroupBy(cm => new { cm.Name, cm.Address },
                              (key, group) => new
                              {
                                  Name = key.Name,
                                  Address = key.Address
                              }).ToList();

                            lstUserRecordGrp = (from email in lstUserRecordGrpTempName
                                                select new GroupUserRecord
                                                {

                                                    Email = string.Empty,
                                                    Address = email.Address,
                                                    Name = email.Name
                                                }).ToList();

                        }


                        // var uniqueClientRecord = lstUserRecordGrp.Where(g => g.Count == 1).ToList();
                        foreach (var clientrecord in lstUserRecordGrp)
                        {
                            var clientRecordsDB = new List<UserRecord>();
                            if (cnt == 0)
                            {
                                //Get the records from DB list
                                clientRecordsDB = lstUserRecordsDB.Where(u => u.Email == clientrecord.Email).ToList();
                            }
                            else
                            {
                                clientRecordsDB = lstUserRecordsDB.Where(u => u.Address == clientrecord.Address && u.Name == clientrecord.Name).ToList();
                            }

                            //Get the first account for Client Table
                            var firstAccount = clientRecordsDB.First();


                            //Create password
                            User user = new User
                            {
                                UserEmailID = string.IsNullOrEmpty(firstAccount.Email) ? "mu" + firstAccount.Login + "@bo.com" : firstAccount.Email,
                                Password = new Common.CurrentDeskSecurity().SetPassEncrypted("mu" + firstAccount.Login),
                                FK_UserTypeID = Constants.K_BROKER_LIVE
                            };

                            Client client = new Client
                            {
                                FK_AccountID = Common.Constants.K_TRADING_ACCOUNT,
                                FK_AccountTypeID = Common.Constants.K_LIVE_INDIVIDUAL,
                                FK_TradingPlatformID = Common.Constants.K_META_TRADER,
                                FK_AccountCurrencyID = 7
                            };

                            int countryId = (from c in context.L_Country
                                             where c.CountryName.Trim() == firstAccount.Country.Trim()
                                             select c.PK_CountryID
                                                 ).FirstOrDefault();

                            if (countryId == 0)
                            {
                                countryId = 1;
                            }

                            IndividualAccountInformation iac = new IndividualAccountInformation
                            {
                                FirstName = firstAccount.Name,
                                ResidentialAddress = firstAccount.Address,
                                EmailAddress = firstAccount.Email,
                                TelephoneNumber = firstAccount.Phone,
                                IDNumber = firstAccount.ID,
                                Client = client,

                                FK_ResidenceCountryID = countryId,
                                FK_CitizenShipCountryID = countryId,
                                FK_ResidentialAddressCountryID = countryId,


                                ResidentialAddressCity = firstAccount.City,
                                ResidentialAddressPostalCode = firstAccount.ZipCode,
                                State = firstAccount.State,
                                Status = firstAccount.Status,
                                Agent = Convert.ToString(firstAccount.AgentAccount),
                                TaxRate = firstAccount.Taxes

                            };

                            client.IndividualAccountInformations.Add(iac);
                            user.Clients.Add(client);
                            context.Users.AddObject(user);

                            context.SaveChanges();


                            string landingAcn = string.Empty;
                            string tradingAcn = string.Empty;
                            long lAccountNo = 0;
                            CreateAccountNumberForUser(client, out tradingAcn, out landingAcn, out lAccountNo);


                            Client_Account clientAccountLanding = new Client_Account
                            {
                                AccountNumber = lAccountNo,
                                LandingAccount = landingAcn,
                                FK_CurrencyID = 5,
                                IsLandingAccount = true,
                                IsTradingAccount = false
                            };

                            client.Client_Account.Add(clientAccountLanding);

                            foreach (var crb in clientRecordsDB)
                            {

                                var equity = context.Margins.Where(l => l.Login == crb.Login).Select(m => m.Equity).FirstOrDefault();
                                Client_Account clientAccountTrading = new Client_Account
                                {
                                    AccountNumber = lAccountNo,
                                    LandingAccount = landingAcn,
                                    TradingAccount = tradingAcn,
                                    CurrentBalance = Convert.ToDecimal(crb.Balance),
                                    PlatformLogin = crb.Login,
                                    PlatformPassword = crb.Password,
                                    FK_CurrencyID = 5,

                                    Group = crb.Group,
                                    Leverage = Convert.ToString(crb.Leverage),
                                    Credit = Convert.ToDecimal(crb.Credit),
                                    Equity = equity,
                                    Comment = crb.Comment,
                                    IsTradingAccount = true,
                                    IsLandingAccount = false
                                };


                                client.Client_Account.Add(clientAccountTrading);

                                context.SaveChanges();

                                var newLandingAC = string.Empty;
                                tradingAcn = string.Empty;
                                lAccountNo = 0;
                                CreateAccountNumberForUser(client, out tradingAcn, out newLandingAC, out lAccountNo);
                            }
                        }

                        //Increment
                        cnt++;
                    }


                }
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }


        /// <summary>
        /// Create Account Number
        /// </summary>
        /// <param name="newClient"></param>
        /// <returns></returns>
        public static void CreateAccountNumberForUser(Client newClient, out string tradingAcn, out string landingAcn, out long actualAccount)
        {
            try
            {
                var accountCreationRuleBO = new AccountCreationRuleBO();
                AccountCurrencyBO curencyBO = new AccountCurrencyBO();

                string tradingAccountNumber = string.Empty;
                var landingAccountNumber = string.Empty;
                string landingAccCurrencyCode = string.Empty;
                long lAccNumber = 0;

                var rulelist = accountCreationRuleBO.GetRule().OrderBy(c => c.Position);
                var currencyID = curencyBO.GetCurrencyLookUpID(newClient.FK_AccountCurrencyID);

                //Iterating through each rule/steps of account creation
                foreach (var item in rulelist)
                {
                    //Currency
                    if (item.PK_ID == (int)AccountCreationPosition.Currency)
                    {
                        L_CurrencyValueBO currencyBO = new L_CurrencyValueBO();
                        tradingAccountNumber += currencyBO.GetCurrencyAccountCode(currencyID) + "-";
                    }
                    //Account Number Belonging to that Currency
                    else if (item.PK_ID == (int)AccountCreationPosition.AccountNumberBelongingToThatCurrency)
                    {
                        var template = item.Template;
                        Client_AccountBO clientAccBO = new Client_AccountBO();
                        var tradingAccCount = clientAccBO.GetNumberOfSameCurrencyTradingAccountForUser(LoginAccountType.LiveAccount, newClient.PK_ClientID, currencyID);

                        tradingAccountNumber += (tradingAccCount + 1).ToString("D" + template.Length) + "-";

                        //Required while creating landing account
                        for (var ctDigit = 0; ctDigit < template.Length; ctDigit++)
                        {
                            landingAccCurrencyCode += "0";
                        }
                    }
                    //Client Account Number
                    else if (item.PK_ID == (int)AccountCreationPosition.ClientAccountNumber)
                    {
                        var template = item.Template;
                        Client_AccountBO clientAccBO = new Client_AccountBO();
                        var existingAccNumber = clientAccBO.GetUserExistingAccountNumber(LoginAccountType.LiveAccount, newClient.PK_ClientID);
                        var latestAccNumber = clientAccBO.GetLatestAccountNumber();

                        //If no previous account exists for the user
                        if (existingAccNumber == "")
                        {
                            //If any acc number exists in db
                            if (latestAccNumber != "")
                            {
                                lAccNumber = Convert.ToInt64(latestAccNumber);
                                tradingAccountNumber += (lAccNumber + 1).ToString("D" + template.Length) + "-";
                                lAccNumber++;
                            }
                            //If no acc number exists in db
                            else
                            {
                                lAccNumber = 1;
                                tradingAccountNumber += lAccNumber.ToString("D" + template.Length) + "-";
                            }
                        }
                        //If user has acc number in system
                        else
                        {
                            tradingAccountNumber += existingAccNumber.Split('-')[((int)item.Position - 1)] + "-";
                            lAccNumber = Convert.ToInt64(existingAccNumber.Split('-')[((int)item.Position - 1)]);
                        }
                    }
                }

                //Landing account
                landingAccountNumber = tradingAccountNumber.Split('-')[0] + "-" + landingAccCurrencyCode + "-" + tradingAccountNumber.Split('-')[2];

                landingAcn = landingAccountNumber;
                tradingAcn = tradingAccountNumber.TrimEnd('-');
                actualAccount = lAccNumber;

            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }

    public class GroupUserRecord
    {

        public string Email;

        public string Name;

        public string Address;

    }
}