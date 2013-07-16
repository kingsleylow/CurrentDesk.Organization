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
	public class L_TradingExperienceBO
	{
        /// <summary>
        /// This Function Will return all the net worth euros values
        /// </summary>
        /// <returns></returns>
        public List<L_TradingExperience> GetTradingExpValues()
        {

            try
            {
                var tradingExpKey = CacheKey.CDS_TRADINGEXPS;
                var tradingExpList = new List<L_TradingExperience>();

                if (StaticCache.Exist(tradingExpKey))
                {
                    tradingExpList = (List<L_TradingExperience>)StaticCache.Get(tradingExpKey);
                }
                else
                {
                    using (var unitOfWork = new EFUnitOfWork())
                    {
                        var lTradingExpRepo =
                            new L_TradingExperienceRepository(new EFRepository<L_TradingExperience>(), unitOfWork);

                        //Returning list of trading exp look up values 
                        tradingExpList =  lTradingExpRepo.All().ToList();

                        StaticCache.Max(tradingExpKey, tradingExpList);
                    }
                }

                return tradingExpList;
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This Function Will Get the name of selected 
        /// Trading Experience depending upon tradingExperienceID
        /// </summary>
        /// <param name="tradingExperienceID"></param>
        /// <returns></returns>
        public string GetSelectedTradingExperience(int tradingExperienceID)
        {
            try
            {
                return GetTradingExpValues().Where(trEx => trEx.PK_ExperienceID == tradingExperienceID).
                              Select(trEx => trEx.Experience).SingleOrDefault();

                //using (var unitOfWork = new EFUnitOfWork())
                //{
                //    var tradingExperienceRepo =
                //        new L_TradingExperienceRepository(new EFRepository<L_TradingExperience>(), unitOfWork);

                //    //Creating Country Objeset to Query
                //    ObjectSet<L_TradingExperience> tradingExpObjSet =
                //      ((CurrentDeskClientsEntities)tradingExperienceRepo.Repository.UnitOfWork.Context).L_TradingExperience;

                //    //Return the selected string
                //    return tradingExpObjSet.Where(trEx => trEx.PK_ExperienceID == tradingExperienceID).
                //        Select(trEx => trEx.Experience).SingleOrDefault();
                //}
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

		
	}
}