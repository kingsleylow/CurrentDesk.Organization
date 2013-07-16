using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.DAL;
using CurrentDesk.Models;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 5/24/2013 4:01:55 PM
//	   and this file should not get overridden 
//
//     Add your own data access methods.
// </auto-generated>
//------------------------------------------------------------------------------ 
	
namespace CurrentDesk.Repository.CurrentDesk
{   
	public class SymbolBO
	{
		// Add your own data access methods here.  If you wish to
		// expose your public method to a WCF service, marked them with
		// the attribute [NCPublish], and another T4 template will generate your service contract

        /// <summary>
        /// Clear existing data in symbole tables
        /// </summary>
        /// <returns></returns>
        public bool ClearExistingSymbol()
        {
            bool isClear = false;
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {

                    var context = (CurrentDeskClientsEntities)unitOfWork.Context;
                    context.ExecuteStoreCommand("TRUNCATE TABLE Symbol");

                    isClear = true;
                }
            }
            catch (Exception exception)
            {
                CommonErrorLogger.CommonErrorLog(exception, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return isClear;
        }


        /// <summary>
        /// Get Sumbol margin mode
        /// </summary>
        /// <returns></returns>
        public List<SymbolMarginMode> GetSymolMarginMode()
        {
            var lstSymbol = new List<SymbolMarginMode>();
            try
            {
                using (var unitOfWork = new EFUnitOfWork())
                {

                    var context = (CurrentDeskClientsEntities)unitOfWork.Context;
                    lstSymbol = (from s in context.Symbols
                                 select new SymbolMarginMode
                                 {

                                     Symbol = s.Symbol1,
                                     MarginMode = s.MarginMode

                                 }).ToList();
                  
                }
            }
            catch (Exception exception)
            {
                CommonErrorLogger.CommonErrorLog(exception, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return lstSymbol;
        }

	}

    /// <summary>
    /// Complex Type for SymbolMarginMode
    /// </summary>
    public class SymbolMarginMode
    {

        public string Symbol;

        public int ? MarginMode;

    }

}