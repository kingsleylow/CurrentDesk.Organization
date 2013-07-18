using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.Models;
using System.Data.Objects;
using CurrentDesk.DAL;
using CurrentDesk.Common;

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
	public class TrustAccountInformationBO
	{
        /// <summary>
        /// This Function Will Get The Individual Name
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string GetLiveIndividualName(int clientID, LoginAccountType accType)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var trustDetailsRepo =
                        new TrustAccountInformationRepository(new EFRepository<TrustAccountInformation>(), unitOfWork);

                    ObjectSet<TrustAccountInformation> trustObjSet =
                        ((CurrentDeskClientsEntities)trustDetailsRepo.Repository.UnitOfWork.Context).TrustAccountInformations;

                    if (accType == LoginAccountType.LiveAccount)
                    {
                        var res = trustObjSet.Where(ind => ind.FK_ClientID == clientID).FirstOrDefault();
                        return res != null ? res.TrustName : null;
                    }
                    else if (accType == LoginAccountType.PartnerAccount)
                    {
                        var res = trustObjSet.Where(ind => ind.FK_IntroducingBrokerID == clientID).FirstOrDefault();
                        return res != null ? res.TrustName : null;
                    }
                    return string.Empty;
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }


        /// <summary>
        /// This Function Will Get The Individual Name
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string GetPartnerIndividualName(int introducingBrokerID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var trustDetailsRepo =
                        new TrustAccountInformationRepository(new EFRepository<TrustAccountInformation>(), unitOfWork);

                    ObjectSet<TrustAccountInformation> trustObjSet =
                        ((CurrentDeskClientsEntities)trustDetailsRepo.Repository.UnitOfWork.Context).TrustAccountInformations;

                    var res = trustObjSet.Where(ind => ind.FK_IntroducingBrokerID == introducingBrokerID).FirstOrDefault();

                    return res != null ? res.TrustName : null;
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns trust account details of a particular client
        /// </summary>
        /// <param name="clientID">clientID</param>
        /// <returns></returns>
        public TrustAccountInformation GetTrustAccountDetails(int clientID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var trustDetailsRepo =
                        new TrustAccountInformationRepository(new EFRepository<TrustAccountInformation>(), unitOfWork);

                    ObjectSet<TrustAccountInformation> trustObjSet =
                        ((CurrentDeskClientsEntities)trustDetailsRepo.Repository.UnitOfWork.Context).TrustAccountInformations;

                    return trustObjSet.Where(trst => trst.FK_ClientID == clientID).FirstOrDefault();
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