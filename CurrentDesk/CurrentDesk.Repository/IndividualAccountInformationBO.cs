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
	public class IndividualAccountInformationBO
	{
		/// <summary>
        /// This function will insert individual account details for new client
        /// </summary>
        /// <returns></returns>
        public void AddIndividualAccDetailsForNewClient(IndividualAccountInformation newClient)
        {

            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var individualDetailsRepo =
                        new IndividualAccountInformationRepository(new EFRepository<IndividualAccountInformation>(), unitOfWork);

                    individualDetailsRepo.Add(newClient);
                    individualDetailsRepo.Save();
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This Function will get the individual Username depending upon the 
        /// ClientID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string GetLiveIndividualName(int clientID, LoginAccountType accType)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var individualDetailsRepo =
                        new IndividualAccountInformationRepository(new EFRepository<IndividualAccountInformation>(), unitOfWork);

                    ObjectSet<IndividualAccountInformation> individualObjSet =
                        ((CurrentDeskClientsEntities)individualDetailsRepo.Repository.UnitOfWork.Context).IndividualAccountInformations;
                   
                    if (accType == LoginAccountType.LiveAccount)
                    {
                        var res = individualObjSet.Where(ind => ind.FK_ClientID == clientID).FirstOrDefault();
                        return res != null ? res.FirstName + " " + res.LastName : null;
                    }
                    else if (accType == LoginAccountType.PartnerAccount)
                    {
                        var res = individualObjSet.Where(ind => ind.FK_IntroducingBrokerID == clientID).FirstOrDefault();
                        return res != null ? res.FirstName + " " + res.LastName : null;
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
        /// This Function will get the individual Username depending upon the 
        /// ClientID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string GetPartnerIndividualName(int introducingBrokerID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var individualDetailsRepo =
                        new IndividualAccountInformationRepository(new EFRepository<IndividualAccountInformation>(), unitOfWork);

                    ObjectSet<IndividualAccountInformation> individualObjSet =
                        ((CurrentDeskClientsEntities)individualDetailsRepo.Repository.UnitOfWork.Context).IndividualAccountInformations;

                    var res = individualObjSet.Where(ind => ind.FK_IntroducingBrokerID == introducingBrokerID).FirstOrDefault();

                    return res != null ? res.FirstName + " " + res.LastName : null;
                }
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        /// <summary>
        /// This method returns individual information
        /// of a particular client
        /// </summary>
        /// <param name="clientID">clientID</param>
        /// <returns></returns>
        public IndividualAccountInformation GetIndividualAccountDetails(int clientID)
        {
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {
                    var individualDetailsRepo =
                        new IndividualAccountInformationRepository(new EFRepository<IndividualAccountInformation>(), unitOfWork);

                    ObjectSet<IndividualAccountInformation> individualObjSet =
                        ((CurrentDeskClientsEntities)individualDetailsRepo.Repository.UnitOfWork.Context).IndividualAccountInformations;

                    return individualObjSet.Where(ind => ind.FK_ClientID == clientID).FirstOrDefault();
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