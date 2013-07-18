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
	public class L_CompanyTypeValueBO
	{
        /// <summary>
        /// This Function will Return All Trustee Type
        /// </summary>
        /// <returns></returns>
        public List<L_CompanyTypeValue> GetCompanyType()
        {
            try
            {
                var companyTypeKey = CacheKey.CDS_COMPANYTYPE;
                var companyTypeList = new List<L_CompanyTypeValue>();

                if (StaticCache.Exist(companyTypeKey))
                {
                    companyTypeList = (List<L_CompanyTypeValue>)StaticCache.Get(companyTypeKey);
                }
                else
                {
                    using (var unitOfWork = new EFUnitOfWork())
                    {
                        var lCompanyTypeRepo =
                            new L_CompanyTypeValueRepository(new EFRepository<L_CompanyTypeValue>(), unitOfWork);

                        //Returning List Of Demo Lead
                        companyTypeList =  lCompanyTypeRepo.All().ToList();

                        //Store it into the cache
                        StaticCache.Max(companyTypeKey, companyTypeList);

                    }
                }

                return companyTypeList;
            }
            catch(Exception ex)
            {
                CommonErrorLogger.CommonErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public string GetSelectedCompany(int selectedCompanyID)
        {
            try
            {
                return GetCompanyType().Where(cmp => cmp.PK_CompanyTypeID == selectedCompanyID).Select(cmp => cmp.CompanyType).SingleOrDefault();

                //using (var unitOfWork = new EFUnitOfWork())
                //{
                //    var lCompanyTypeRepo =
                //        new L_CompanyTypeValueRepository(new EFRepository<L_CompanyTypeValue>(), unitOfWork);

                //    //Creating Country Objeset to Query
                //    ObjectSet<L_CompanyTypeValue> companyTypeObjSet =
                //      ((CurrentDeskClientsEntities)lCompanyTypeRepo.Repository.UnitOfWork.Context).L_CompanyTypeValue;

                //    //Return the selected string
                //    return companyTypeObjSet.Where(cmp => cmp.PK_CompanyTypeID == selectedCompanyID).
                //        Select(cmp => cmp.CompanyType).SingleOrDefault();
                   
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