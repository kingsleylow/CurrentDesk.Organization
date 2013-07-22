using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.Models;
using System.Data.Objects;
using CurrentDesk.DAL;
using CurrentDesk.Repository.Utility;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 12/25/2012 2:56:23 PM
//	   and this file should not get overridden 
//
//     Add your own data access methods.
// </auto-generated>
//------------------------------------------------------------------------------ 
	
namespace CurrentDesk.Repository.CurrentDesk
{   
	public class L_CurrencyValueBO
	{

        /// <summary>
        /// This Function Will return all the Currency
        /// </summary>
        /// <returns></returns>
        public List<L_CurrencyValue> GetCurrencies()
        {
            try
            {
                var currencyKey = CacheKey.CDS_CURRENCIES;
                var currencyList = new List<L_CurrencyValue>();

                if (StaticCache.Exist(currencyKey))
                {
                    currencyList = (List<L_CurrencyValue>)StaticCache.Get(currencyKey);
                }
                else
                {
                    using (var unitOfWork = new EFUnitOfWork())
                    {
                        var lCurrencyRepo =
                            new L_CurrencyValueRepository(new EFRepository<L_CurrencyValue>(), unitOfWork);

                        //Returning List Of Demo Lead
                        currencyList = lCurrencyRepo.All().ToList();

                        //Store it into the cache
                        StaticCache.Max(currencyKey, currencyList);
                    }
                }

                return currencyList;
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency account code for a particular currency
        /// </summary>
        /// <param name="pkCurrencyID">pkCurrencyID</param>
        /// <returns></returns>
        public string GetCurrencyAccountCode(int? pkCurrencyID)
        {
            try
            {
                return GetCurrencies().Where(curr => curr.PK_CurrencyValueID == pkCurrencyID).
                    Select(curr => curr.AccountCurrencyCode).SingleOrDefault();
                //using (var unitOfWork = new EFUnitOfWork())
                //{
                //    var lCurrencyRepo =
                //        new L_CurrencyValueRepository(new EFRepository<L_CurrencyValue>(), unitOfWork);

                //    //Creating Currency Objeset to Query
                //    ObjectSet<L_CurrencyValue> currencyObjSet =
                //      ((CurrentDeskClientsEntities)lCurrencyRepo.Repository.UnitOfWork.Context).L_CurrencyValue;

                //    //Return the selected string
                //    return currencyObjSet.Where(curr => curr.PK_CurrencyValueID == pkCurrencyID).
                //        Select(curr => curr.AccountCurrencyCode).SingleOrDefault();

                //}
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency symbol for a particular currency
        /// </summary>
        /// <param name="pkCurrencyID">pkCurrencyID</param>
        /// <returns></returns>
        public string GetCurrencySymbolFromID(int pkCurrencyID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var lCurrencyRepo =
                        new L_CurrencyValueRepository(new EFRepository<L_CurrencyValue>(), unitOfWork);

                    //Creating Currency Objeset to Query
                    ObjectSet<L_CurrencyValue> currencyObjSet =
                      ((CurrentDeskClientsEntities)lCurrencyRepo.Repository.UnitOfWork.Context).L_CurrencyValue;

                    //Return the selected string
                    return currencyObjSet.Where(curr => curr.PK_CurrencyValueID == pkCurrencyID).
                        Select(curr => curr.CurrencyValue).SingleOrDefault();
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency image class as per curr id
        /// </summary>
        /// <param name="pkCurrencyID"></param>
        /// <returns></returns>
        public string GetCurrencyImageClass(int pkCurrencyID)
        {
            try
            {
                if (pkCurrencyID == 5)
                {
                    return "usd";
                }
                if (pkCurrencyID == 4)
                {
                    return "gbp";
                }
                if (pkCurrencyID == 3)
                {
                    return "eur";
                }
                if (pkCurrencyID == 2)
                {
                    return "chf";
                }
                if (pkCurrencyID == 1)
                {
                    return "aud";
                }
                return "";
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency symbol based on its AccountCurrencyCode
        /// </summary>
        /// <param name="currencyAccCode"></param>
        /// <returns></returns>
        public string GetCurrencySymbolFromCurrencyAccountCode(string currencyAccCode)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var lCurrencyRepo =
                        new L_CurrencyValueRepository(new EFRepository<L_CurrencyValue>(), unitOfWork);

                    //Creating Currency Objset to Query
                    ObjectSet<L_CurrencyValue> currencyObjSet =
                      ((CurrentDeskClientsEntities)lCurrencyRepo.Repository.UnitOfWork.Context).L_CurrencyValue;

                    //Return the selected string
                    return currencyObjSet.Where(curr => curr.AccountCurrencyCode == currencyAccCode).
                        Select(curr => curr.CurrencyValue).SingleOrDefault();
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency pk_id based on AccountCurrencyCode
        /// </summary>
        /// <param name="accCode"></param>
        /// <returns></returns>
        public int GetCurrencyIDFromAccountCode(string accCode)
        {
            try
            {
                return GetCurrencies().Where(curr => curr.AccountCurrencyCode == accCode).
                    Select(curr => curr.PK_CurrencyValueID).SingleOrDefault();
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns currency ID based on symbol
        /// </summary>
        /// <param name="currSymbol">currSymbol</param>
        /// <returns></returns>
        public int GetCurrencyIDFromSymbol(string currSymbol)
        {
            try
            {
                return GetCurrencies().Where(curr => curr.CurrencyValue == currSymbol).
                    Select(curr => curr.PK_CurrencyValueID).SingleOrDefault();
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
		
	}
}