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
    public class L_RecievingBankBO
    {
        /// <summary>
        /// This Function Will return all the receiving banks
        /// </summary>
        /// <returns></returns>
        public List<L_RecievingBank> GetReceivingBankInfo()
        {

            try
            {
                var idInformationKey = CacheKey.CDS_RECEVING_BANK_INFORMATION;
                var receivingBankList = new List<L_RecievingBank>();

                if (StaticCache.Exist(idInformationKey))
                {
                    receivingBankList = (List<L_RecievingBank>)StaticCache.Get(idInformationKey);
                }
                else
                {
                    using (var unitOfWork = new EFUnitOfWork())
                    {
                        var lReceivingBankRepo =
                            new L_RecievingBankRepository(new EFRepository<L_RecievingBank>(), unitOfWork);

                        //Returning list of receiving bank values
                        receivingBankList = lReceivingBankRepo.All().ToList();

                        //Store it into the cache
                        StaticCache.Max(idInformationKey, receivingBankList);
                    }
                }
                return receivingBankList;
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }


        /// <summary>
        /// This Function Will return all the receiving banks
        /// </summary>
        /// <returns></returns>
        public List<L_RecievingBank> GetReceivingBankInfo(int organizationID)
        {

            try
            {

                using (var unitOfWork = new EFUnitOfWork())
                {
                    var lReceivingBankRepo =
                        new L_RecievingBankRepository(new EFRepository<L_RecievingBank>(), unitOfWork);

                    ObjectSet<L_RecievingBank> recievingBankObjectSet =
             ((CurrentDeskClientsEntities)lReceivingBankRepo.Repository.UnitOfWork.Context).L_RecievingBank;

                    return recievingBankObjectSet.Where(idInfo => idInfo.FK_OrganizationID == organizationID).ToList();
                }

            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectedRecievingBankInfo(int recievingBankInfoID)
        {

            try
            {
                var selectedBank = GetReceivingBankInfo().
                    Where(recBnk => recBnk.PK_RecievingBankID == recievingBankInfoID)
                    .Select(recBnk => recBnk.RecievingBankName).FirstOrDefault();
                return selectedBank;
                //using (var unitOfWork = new EFUnitOfWork())
                //{
                //    var lReceivingBankRepo =
                //        new L_RecievingBankRepository(new EFRepository<L_RecievingBank>(), unitOfWork);

                //    //Creating annualIncome Objeset to Query
                //    ObjectSet<L_RecievingBank> recievingBankObjSet =
                //      ((CurrentDeskClientsEntities)lReceivingBankRepo.Repository.UnitOfWork.Context).L_RecievingBank;

                //    //Returning list of receiving bank values
                //    return recievingBankObjSet.Where(reBnk => reBnk.PK_RecievingBankID == recievingBankInfoID).
                //        Select(reBnk => reBnk.RecievingBankName).SingleOrDefault();  

                //}
            }
            catch (Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

    }
}