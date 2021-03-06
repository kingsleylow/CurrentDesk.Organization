﻿/* **************************************************************
* File Name     :- AccountSignUpController.cs
* Author        :- Mukesh Nayak
* Copyright     :- Mindfire Solutions 
* Date          :- 2nd Jan 2013
* Modified Date :- 16th April 2013
* Description   :- This file will deal with all the 
                   operation related to all the account signup(live trust individual corporate)
****************************************************************/


#region Namespace
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CurrentDesk.BackOffice.Extension;
using CurrentDesk.BackOffice.Models;
using CurrentDesk.Common;
using CurrentDesk.Models;
using CurrentDesk.Repository.CurrentDesk;
using CurrentDesk.BackOffice.Security;
using CurrentDesk.BackOffice.Models.Error;
using CurrentDesk.Logging;
using CurrentDesk.BackOffice.Utilities;
#endregion

namespace CurrentDesk.BackOffice.Controllers
{
    /// <summary>
    /// Account sign up controller
    /// This Controller is responsible for all sign Up
    /// </summary>
    public class AccountSignUpController : Controller
    {
        #region Variables

        private L_CountryBO countryBO = new L_CountryBO();
        private AccountTypeBO accountTypeBO = new AccountTypeBO();
        private L_InitialInvestmentBO initialInvestmentBO = new L_InitialInvestmentBO();
        private TradingPlatformBO tradingPlatformBO = new TradingPlatformBO();
        private L_TicketSizeBO ticketSizeBO = new L_TicketSizeBO();
        private AccountCurrencyBO accountCurrencyBO = new AccountCurrencyBO();
        private L_LanguagesBO languageBO = new L_LanguagesBO();
        private L_AccountCodeBO accountCodeBO = new L_AccountCodeBO();
        private DemoLeadBO demoLeadBO = new DemoLeadBO();
        private L_IDInformationTypeBO idInfoTypeBO = new L_IDInformationTypeBO();
        private L_RecievingBankBO receivingBankInfoBO = new L_RecievingBankBO();
        private L_AnnualIncomeValueBO annualIncomeValuesBO = new L_AnnualIncomeValueBO();
        private L_LiquidAssetsValueBO liquidAssetsValuesBO = new L_LiquidAssetsValueBO();
        private L_NetWorthEurosBO netWorthEurosValuesBO = new L_NetWorthEurosBO();
        private L_TradingExperienceBO tradingExpValuesBO = new L_TradingExperienceBO();
        private L_TrusteeTypeValueBO trusteeTypeValuesBO = new L_TrusteeTypeValueBO();
        private L_CompanyTypeValueBO companyTypeValuesBO = new L_CompanyTypeValueBO();
        private L_CommissionIncrementValueBO commissionIncremantValuesBO = new L_CommissionIncrementValueBO();
        private L_WidenSpreadsValueBO widenSpreadValuesBO = new L_WidenSpreadsValueBO();
        private LiveLeadBO liveLeadBO = new LiveLeadBO();
        private ClientBO clientBO = new ClientBO();
        private UserBO userBO = new UserBO();
        private IndividualAccountInformationBO individualAccountInformationBO = new IndividualAccountInformationBO();
        private CorporateAccountInformationBO corporateAccountInformationBO = new CorporateAccountInformationBO();
        private IntroducingBrokerBO introducingBrokerBO = new IntroducingBrokerBO();
        private AssetManagerBO assetManagerBO = new AssetManagerBO();
        private CurrentDeskSecurity currentDeskSecurity;
        private PartnerCommissionBO partCommBO = new PartnerCommissionBO();
        private Client_AccountBO clientAccBO = new Client_AccountBO();
        private AgentBO agentBO = new AgentBO();

        public IndividualAccountModel individualAccountModel;

        #endregion

        #region Methods

        #region DEMO ACCOUNT

        /// <summary>
        /// This Function will return to DemoAccount View
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            try
            {
                //var organizationBO = new OrganizationBO();
                //var browserUrl = Request.Url.AbsoluteUri;

                ////browserUrl = "http://lmax.com"; //TODO Need to change with URL
                //var organizationID = organizationBO.GetOrganizationIDFromURL(browserUrl);

                var organizationID = OrganizationUtility.GetOrganizationID(Request.Url.AbsoluteUri);

                if (organizationID != null)
                {
                    //Add Organization ID to Session
                    SessionManagement.OrganizationID = organizationID;
                    //SessionManagement.OrganizationImagLogoHeaderURL = @"/Images/logo.png";
                    //SessionManagement.OrganizationImagLogoLoginURL =  ;
                    //SessionManagement.OrganizationImagLogoSignUpURL = ;


                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["AccountCurrency"] = new SelectList(accountCurrencyBO.GetSelectedCurrency(Constants.K_BROKER_DEMO, (int)organizationID),
                                                                 "PK_AccountCurrencyID", "L_CurrencyValue.CurrencyValue");
                    ViewData["AccountType"] = new SelectList(accountTypeBO.GetSelectedAccountType(Constants.K_BROKER_DEMO, (int)organizationID), "PK_AccountType",
                                                             "L_AccountTypeValue.AccountTypeValues");
                    ViewData["Initialinvestment"] = new SelectList(initialInvestmentBO.GetInitialInvestment(),
                                                                   "PK_InitialInvestmentID", "InvestmentValue");
                    ViewData["TradingPlatform"] = new SelectList(tradingPlatformBO.GetSelectedPlatform(Constants.K_BROKER_DEMO, (int)organizationID),
                                                                 "PK_TradingPlatformID",
                                                                 "L_TradingPlatformValues.TradingValue");
                    ViewData["TicketSize"] = new SelectList(ticketSizeBO.GetTickets(), "PK_TicketSizeID",
                                                            "TicketSizeValue");
                    ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");


                    return View("DemoAccount");
                }
                else
                {
                    //Return to Page Not Found Error
                    return RedirectToAction("PageNotFound", "Error");
                    //return View("ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from DemoAccount View.This function will insert
        /// new lead to the database.
        /// </summary>
        /// <param name="demoaccountModel"></param>
        /// <returns>ActionResult</returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(DemoAccountModel demoAccountModel)
        {
            try
            {
                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    //Checking whether model state is valid or not
                    if (ModelState.IsValid)
                    {
                        //Creating New Demo Lead
                        var newDemoLead = new DemoLead()
                            {
                                FirstName = demoAccountModel.FirstName,
                                LastName = demoAccountModel.LastName,
                                EmailAddress = demoAccountModel.EmailAddress,
                                PhoneNo = demoAccountModel.PhoneNumber,
                                FK_ResidenceCountry = demoAccountModel.CountryCode,
                                FK_AccountTypeID = demoAccountModel.AccountType,
                                FK_AccountCurrencyID = demoAccountModel.AccountCurrency,
                                FK_PlatformID = demoAccountModel.TradingPlatform,
                                FK_InvestmentID = demoAccountModel.InitialInvestment,
                                FK_TicketSizeID = demoAccountModel.TicketSize,
                                FK_PreferedCommunicationLanguage = demoAccountModel.LanguageCode,
                                IsEnglishSpeaking = demoAccountModel.IsEnglishSpeaking,
                                FK_ExperienceID = demoAccountModel.TradingExperience,
                                IntroducingBrokerOrAgent = demoAccountModel.IntroducingBrokerOrAgent,
                                FK_OrganizationID = (int) organizationID

                            };

                        //Instantiating DemoLead Buisness Object and creating new Lead.
                        var demoLeadBO = new DemoLeadBO();
                        demoLeadBO.AddNewDemoLead(newDemoLead);

                        //returning sucessfully to the view.
                        return View("RegistartionSuccess");
                    }
                    else
                    {
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["AccountCurrency"] =
                            new SelectList(accountCurrencyBO.GetSelectedCurrency(1, (int) organizationID),
                                           "PK_AccountCurrencyID", "L_CurrencyValue.CurrencyValue");
                        ViewData["AccountType"] =
                            new SelectList(accountTypeBO.GetSelectedAccountType(1, (int) organizationID),
                                           "PK_AccountType",
                                           "L_AccountTypeValue.AccountTypeValues");
                        ViewData["Initialinvestment"] = new SelectList(initialInvestmentBO.GetInitialInvestment(),
                                                                       "PK_InitialInvestmentID", "InvestmentValue");
                        ViewData["TradingPlatform"] =
                            new SelectList(tradingPlatformBO.GetSelectedPlatform(1, (int) organizationID),
                                           "PK_TradingPlatformID",
                                           "L_TradingPlatformValues.TradingValue");
                        ViewData["TicketSize"] = new SelectList(ticketSizeBO.GetTickets(), "PK_TicketSizeID",
                                                                "TicketSizeValue");
                        ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID",
                                                                "Experience");

                        return View("DemoAccount", demoAccountModel);
                    }
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        #endregion

        #region LIVE ACCOUNT INITIAL

        /// <summary>
        /// This Function will display LiveAccountInitial View
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LiveAccountInitial()
        {
            try
            {
                //var OrganizationBO = new OrganizationBO();
                //var browserUrl = Request.Url.AbsoluteUri;

                ////browserUrl = "http://lmax.com"; //TODO Need to change with URL
                //var organizationID = OrganizationBO.GetOrganizationIDFromURL(browserUrl);

                var organizationID = OrganizationUtility.GetOrganizationID(Request.Url.AbsoluteUri);

                if (organizationID != null)
                {
                    //Add Organization ID to Session
                    SessionManagement.OrganizationID = organizationID;

                    ViewData["AccountCode"] = new SelectList(accountCodeBO.GetSelectedAccount(Constants.K_BROKER_LIVE),
                                                             "PK_AccountID", "AccountName");
                    ViewData["AccountCurrency"] =
                        new SelectList(accountCurrencyBO.GetSelectedCurrency(Constants.K_BROKER_LIVE,(int)organizationID),
                                       "PK_AccountCurrencyID", "L_CurrencyValue.CurrencyValue");
                    ViewData["AccountType"] =
                        new SelectList(accountTypeBO.GetSelectedAccountType(Constants.K_BROKER_LIVE, (int)organizationID), "PK_AccountType",
                                       "L_AccountTypeValue.AccountTypeValues");
                    ViewData["TradingPlatform"] =
                        new SelectList(tradingPlatformBO.GetSelectedPlatform(Constants.K_BROKER_LIVE, (int)organizationID),
                                       "PK_TradingPlatformID", "L_TradingPlatformValues.TradingValue");
                    ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");


                    return View("LiveAccountInitial");
                }
                else
                {
                    return RedirectToAction("PageNotFound", "Error");
                    //return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from LiveAccountInitial View.This function will insert
        /// new Live lead to the database.
        /// </summary>
        /// <param name="liveAccountInitialModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiveAccountInitial(LiveAccountInitialModel liveAccountInitialModel)
        {
            try
            {
                //Creating Instance
                currentDeskSecurity = new CurrentDeskSecurity();
                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    if (ModelState.IsValid)
                    {
                        string referralID = Request.QueryString["referralID"];
                        int? agentID = null;
                        int? introducingBrokerID = null;
                        long accNumber = 0;

                        //If referral link present
                        if (referralID != null)
                        {
                            string accNumberOrCustomKeyword = referralID.Split('-')[0];
                            string agentCode = null;

                            if (referralID.Contains("-"))
                            {
                                agentCode = referralID.Split('-')[1];
                            }                            
                                                  

                            //If accountNumber
                            if (long.TryParse(accNumberOrCustomKeyword, out accNumber))
                            {
                                //Get IntroducingBrokerID from Client_Account table
                                introducingBrokerID =
                                    clientAccBO.GetIntroducingBrokerIDFromAccNumber(accNumberOrCustomKeyword, (int)organizationID);
                            }
                                //If custom keyword
                            else
                            {
                                //Get IntroducingBrokerID from IntroducingBroker table
                                introducingBrokerID =
                                    introducingBrokerBO.GetBrokerIDFromReferralKeyword(accNumberOrCustomKeyword, (int)organizationID);
                            }

                            if (agentCode != null)
                            {
                                //Get AgentID
                                agentID = agentBO.GetAgentIDFromAgentCodeAndIBID(Convert.ToInt32(agentCode),
                                                                                 introducingBrokerBO
                                                                                     .GetBrokerUserIDFromBrokerID(
                                                                                         (int) introducingBrokerID));
                            }

                        }

                        //Creating New Live Lead.
                        var newLiveLead = new LiveLead()
                            {
                                FK_AccountID = liveAccountInitialModel.AccountCode,
                                FK_AccountTypeID = liveAccountInitialModel.AccountType,
                                FK_PlatformID = liveAccountInitialModel.TradingPlatform,
                                FK_AccountCurrencyID = liveAccountInitialModel.AccountCurrency,
                                EmailAddress = liveAccountInitialModel.UserEmail,
                                Password = currentDeskSecurity.SetPassEncrypted(liveAccountInitialModel.Password),
                                FK_PreferedCommunicationLanguage = liveAccountInitialModel.LanguageCode,
                                IsEnglishSpeaking = liveAccountInitialModel.IsEnglishSpeaking,
                                FK_IntroducingBrokerID = introducingBrokerID,
                                FK_AgentID = agentID,
                                FK_OrganizationID = (int) organizationID
                            };

                        //Instantiating new business object for LiveLead and Adding new live lead to database
                        var liveLeadBO = new LiveLeadBO();
                        liveLeadBO.AddNewLiveLead(newLiveLead);

                        //Storing Registered Email in Session
                        Session["RegisteredEmailAddress"] = liveAccountInitialModel.UserEmail;

                        //Get account type
                        var accountType = accountTypeBO.GetAccountTypeValue(liveAccountInitialModel.AccountType);

                        //Depending Upon AccountType redirect ot corresponding View
                        if (accountType == Constants.K_ACCT_INDIVIDUAL)
                        {
                            return RedirectToAction("LiveIndividualAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_JOINT)
                        {
                            return RedirectToAction("LiveJointAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_CORPORATE)
                        {
                            return RedirectToAction("LiveCorporateAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_TRUST)
                        {
                            return RedirectToAction("LiveTrustAccountComplete");
                        }
                        else
                        {
                            return View("ErrorMessage");
                        }

                    }
                    else
                    {
                        ViewData["AccountCode"] =
                            new SelectList(accountCodeBO.GetSelectedAccount(Constants.K_BROKER_LIVE),
                                           "PK_AccountID", "AccountName");
                        ViewData["AccountCurrency"] =
                            new SelectList(
                                accountCurrencyBO.GetSelectedCurrency(Constants.K_BROKER_LIVE, (int) organizationID),
                                "PK_AccountCurrencyID", "L_CurrencyValue.CurrencyValue");
                        ViewData["AccountType"] =
                            new SelectList(
                                accountTypeBO.GetSelectedAccountType(Constants.K_BROKER_LIVE, (int) organizationID),
                                "PK_AccountType",
                                "L_AccountTypeValue.AccountTypeValues");
                        ViewData["TradingPlatform"] =
                            new SelectList(
                                tradingPlatformBO.GetSelectedPlatform(Constants.K_BROKER_LIVE, (int) organizationID),
                                "PK_TradingPlatformID", "L_TradingPlatformValues.TradingValue");
                        ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");

                        return View(liveAccountInitialModel);

                    }
                }
                else
                {
                    //return View("ErrorMessage");
                    return RedirectToAction("PageNotFound", "Error");
                }
            }
            catch (Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        #endregion

        #region LIVE JOINT ACCOUNT

        /// <summary>
        /// This Function will display Joint Account View for the Live
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LiveJointAccountComplete()
        {
            try
            {
                if (Session["RegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("LiveAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int) organizationID), "PK_IDTypeID",
                                                        "IDValue");
                    ViewData["ReceivingBankInfo"] =
                        new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int) organizationID),
                                       "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");


                    return View("JointAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from LiveJointAccount View.This function will insert
        /// client and account information to database.
        /// </summary>
        /// <param name="jointAccountModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiveJointAccountComplete(JointAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JointAccountReviewModel jointAccountReviewModel = new JointAccountReviewModel()
                    {
                        PrimaryAccountHolderTitle = model.PrimaryAccountHolderTitle == "1" ? "Mr" : "Mrs",
                        PrimaryAccountHolderFirstName = model.PrimaryAccountHolderFirstName,
                        PrimaryAccountHolderMiddleName = model.PrimaryAccountHolderMiddleName,
                        PrimaryAccountHolderLastName = model.PrimaryAccountHolderLastName,
                        PrimaryAccountHolderDobMonth = model.PrimaryAccountHolderDobMonth.ToString("D2"),
                        PrimaryAccountHolderDobDay = model.PrimaryAccountHolderDobDay.ToString("D2"),
                        PrimaryAccountHolderDobYear = model.PrimaryAccountHolderDobYear,
                        PrimaryAccountHolderGender = model.PrimaryAccountHolderGender ? "Male" : "Female",
                        PrimaryAccountHolderCitizenship = countryBO.GetSelectedCountry(model.PrimaryAccountHolderCitizenship),
                        PrimaryAccountHolderIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.PrimaryAccountHolderIdInfo),
                        PrimaryAccountHolderIdNumber = model.PrimaryAccountHolderIdNumber,
                        PrimaryAccountHolderResidenceCountry = countryBO.GetSelectedCountry(model.PrimaryAccountHolderResidenceCountry),

                        SecondaryAccountHolderTitle = model.SecondaryAccountHolderTitle == "1" ? "Mr" : "Mrs",
                        SecondaryAccountHolderFirstName = model.SecondaryAccountHolderFirstName,
                        SecondaryAccountHolderMiddleName = model.SecondaryAccountHolderMiddleName,
                        SecondaryAccountHolderLastName = model.SecondaryAccountHolderLastName,
                        SecondaryAccountHolderDobMonth = model.SecondaryAccountHolderDobMonth.ToString("D2"),
                        SecondaryAccountHolderDobDay = model.SecondaryAccountHolderDobDay.ToString("D2"),
                        SecondaryAccountHolderDobYear = model.SecondaryAccountHolderDobYear,
                        SecondaryAccountHolderGender = model.SecondaryAccountHolderGender ? "Male" : "Female",
                        SecondaryAccountHolderCitizenship = countryBO.GetSelectedCountry(model.SecondaryAccountHolderCitizenship),
                        SecondaryAccountHolderIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.SecondaryAccountHolderIdInfo),
                        SecondaryAccountHolderIdNumber = model.SecondaryAccountHolderIdNumber,
                        SecondaryAccountHolderResidenceCountry = countryBO.GetSelectedCountry(model.SecondaryAccountHolderResidenceCountry),


                        PrimaryAccountHolderResidentialAddLine1 = model.PrimaryAccountHolderResidentialAddLine1,
                        PrimaryAccountHoldeResidentialAddLine2 = model.PrimaryAccountHoldeResidentialAddLine2,
                        PrimaryAccountHoldeResidentialCity = model.PrimaryAccountHoldeResidentialCity,
                        PrimaryAccountHoldeResidentialCountry = countryBO.GetSelectedCountry(model.PrimaryAccountHoldeResidentialCountry),
                        PrimaryAccountHoldeResidentialPostalCode = model.PrimaryAccountHoldeResidentialPostalCode,
                        PrimaryAccountHoldeYearsInCurrentAdd = model.PrimaryAccountHoldeYearsInCurrentAdd,
                        PrimaryAccountHoldeMonthsInCurrentAdd = model.PrimaryAccountHoldeMonthsInCurrentAdd,
                        PrimaryAccountHoldePreviousAddLine1 = model.PrimaryAccountHoldePreviousAddLine1,
                        PrimaryAccountHoldePreviousAddLine2 = model.PrimaryAccountHoldePreviousAddLine2,
                        PrimaryAccountHoldePreviousCity = model.PrimaryAccountHoldePreviousCity,
                        PrimaryAccountHoldePreviousCountry = model.PrimaryAccountHoldePreviousCountry != null ? countryBO.GetSelectedCountry((int)model.PrimaryAccountHoldePreviousCountry) : null,
                        PrimaryAccountHoldePreviousPostalCode = model.PrimaryAccountHoldePreviousPostalCode,
                        PrimaryAccountHoldeTelNumberCountryCode = model.PrimaryAccountHoldeTelNumberCountryCode,
                        PrimaryAccountHoldeTelNumber = model.PrimaryAccountHoldeTelNumber,
                        PrimaryAccountHoldeMobileNumberCountryCode = model.PrimaryAccountHoldeMobileNumberCountryCode,
                        PrimaryAccountHoldeMobileNumber = model.PrimaryAccountHoldeMobileNumber,
                        
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        JointModel = model,
                        IsLive = true
                       

                    };
                    return View("JointAccountReview", jointAccountReviewModel);
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int) organizationID),
                                                            "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] =
                            new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int) organizationID),
                                           "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View("JointAccount", model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }

        }

        /// <summary>
        /// Insert Individual Live Information
        /// </summary>
        /// <param name="model"></param>
        public void InsertLiveJointInformation(JointAccountModel jointAccountModel)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["RegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["RegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_LIVE;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);
                
                //Add details to Client table
                Client newClient = new Client();

                //Information From the Lead
                newClient.FK_AccountID = liveLead.FK_AccountID;
                newClient.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newClient.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newClient.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newClient.FK_PrefferedLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newClient.FK_AgentID = liveLead.FK_AgentID;
                newClient.FK_IntroducingBrokerID = liveLead.FK_IntroducingBrokerID;
                newClient.IsSeen = false;
                newClient.IsDismissed = false;
                newClient.IsActivitySeen = false;
                newClient.IsActivityDismissed = false;
                newClient.Activity = "new";
                newClient.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;

                newClient.EstimatedAnnualIncome = jointAccountModel.EstimatedAnnualIncome.ToString();
                newClient.LiquidAssets = jointAccountModel.LiquidAssets.ToString();
                newClient.NetWorthInEuros = jointAccountModel.NetWorthEuros.ToString();
                newClient.FK_TradingSecurityExperienceID = jointAccountModel.DrpHaveExperienceTradingSecurities;
                newClient.FK_TradingOptionExperienceID = jointAccountModel.DrpHaveExperienceTradingOptions;
                newClient.FK_TradingForeignExchangeExperienceID = jointAccountModel.DrpHaveExperienceTradingForeignExchange;
                newClient.IsHavingAccount = jointAccountModel.HaveAccWithFqSecurities;
                newClient.IsRegisterdWithEntity = jointAccountModel.RegisteredPerson;
                newClient.IsRegisteredWithRegulator = jointAccountModel.RequiredToBeRegisteredWithRegulator;
                newClient.IsEmployeeOfExchangeOrRegulator = jointAccountModel.EmployeeOfExchangeOrRegulatorOperator;
                newClient.IsDeclaredBankruptcy = jointAccountModel.EverDeclaredBankruptcy;
                newClient.PIC = "";
                newClient.FK_UserID = newUser.PK_UserID;
                newClient.Status = "Missing Documents";
                newClient.FK_OrganizationID = (int)organizationID;

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = jointAccountModel.BankName,
                    AccountNumber = jointAccountModel.AccountNumber,
                    BicNumberOrSwiftCode = jointAccountModel.BicOrSwiftCode,
                    BankingAddress = jointAccountModel.BankAddLine1 + "@" + jointAccountModel.BankAddLine2,
                    FK_ReceivingBankInformationID = jointAccountModel.ReceivingBankInfoId,
                    ReceivingBankInfo = jointAccountModel.ReceivingBankInfo,
                    City = jointAccountModel.BankCity,
                    FK_CountryID = jointAccountModel.BankCountry,
                    PostalCode = jointAccountModel.BankPostalCode.ToString()
                };

                newClient.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };


                //Add details to IndividualAccountInformation table
                JointAccountInformation jointAccountInformation = new JointAccountInformation();

                //Primary Account Information
                jointAccountInformation.PrimaryAccountHolderTitle = jointAccountModel.PrimaryAccountHolderTitle;
                jointAccountInformation.PrimaryAccountHolderFirstName = jointAccountModel.PrimaryAccountHolderFirstName;
                jointAccountInformation.PrimaryAccountHolderMiddleName = jointAccountModel.PrimaryAccountHolderMiddleName;
                jointAccountInformation.PrimaryAccountHolderLastName = jointAccountModel.PrimaryAccountHolderLastName;
                jointAccountInformation.PrimaryAccountHolderBirthDate =
                                                            new DateTime(jointAccountModel.PrimaryAccountHolderDobYear,
                                                            jointAccountModel.PrimaryAccountHolderDobMonth,
                                                            jointAccountModel.PrimaryAccountHolderDobDay);
                jointAccountInformation.PrimaryAccountHolderGender = jointAccountModel.PrimaryAccountHolderGender;
                jointAccountInformation.FK_PrimaryAccountHolderCitizenshipCountryID = jointAccountModel.PrimaryAccountHolderCitizenship;
                jointAccountInformation.FK_PrimaryAccountHolderIDTypeID = jointAccountModel.PrimaryAccountHolderIdInfo;
                jointAccountInformation.PrimaryAccountHolderIDNumber = jointAccountModel.PrimaryAccountHolderIdNumber;
                jointAccountInformation.FK_PrimaryAccountHolderResidenceCountryID = jointAccountModel.PrimaryAccountHolderResidenceCountry;

                //Secondary Account Information
                jointAccountInformation.SecondaryAccountHolderTitle = jointAccountModel.SecondaryAccountHolderTitle;
                jointAccountInformation.SecondaryAccountHolderFirstName = jointAccountModel.SecondaryAccountHolderFirstName;
                jointAccountInformation.SecondaryAccountHolderMiddleName = jointAccountModel.SecondaryAccountHolderMiddleName;
                jointAccountInformation.SecondaryAccountHolderLastName = jointAccountModel.SecondaryAccountHolderLastName;
                jointAccountInformation.SecondaryAccountHolderBirthDate =
                                                            new DateTime(jointAccountModel.SecondaryAccountHolderDobYear,
                                                            jointAccountModel.SecondaryAccountHolderDobMonth,
                                                            jointAccountModel.SecondaryAccountHolderDobDay);
                jointAccountInformation.SecondaryAccountHolderGender = jointAccountModel.SecondaryAccountHolderGender;
                jointAccountInformation.FK_SecondaryAccountHolderCitizenshipCountryID = jointAccountModel.SecondaryAccountHolderCitizenship;
                jointAccountInformation.FK_SecondaryAccountHolderIDTypeID = jointAccountModel.SecondaryAccountHolderIdInfo;
                jointAccountInformation.SecondaryAccountHolderIDNumber = jointAccountModel.SecondaryAccountHolderIdNumber;
                jointAccountInformation.FK_SecondaryAccountHolderResidenceCountryID = jointAccountModel.SecondaryAccountHolderResidenceCountry;

                //Primary Account HolderContact Information
                jointAccountInformation.ResidentialAddress = jointAccountModel.PrimaryAccountHolderResidentialAddLine1 + "@" +
                                                            jointAccountModel.PrimaryAccountHoldeResidentialAddLine2;
                jointAccountInformation.ResidentialAddressCity = jointAccountModel.PrimaryAccountHoldeResidentialCity;
                jointAccountInformation.FK_ResidentialAddressCountryID = jointAccountModel.PrimaryAccountHoldeResidentialCountry;
                jointAccountInformation.ResidentialAddressPostalCode = jointAccountModel.PrimaryAccountHoldeResidentialPostalCode;
                jointAccountInformation.MonthsAtCurrentAddress =
                    (jointAccountModel.PrimaryAccountHoldeYearsInCurrentAdd * 12) + jointAccountModel.PrimaryAccountHoldeMonthsInCurrentAdd;

                jointAccountInformation.PreviousAddress = jointAccountModel.PrimaryAccountHoldePreviousAddLine1 + "@" +
                                                            jointAccountModel.PrimaryAccountHoldePreviousAddLine2;
                jointAccountInformation.PreviousAddressCity = jointAccountModel.PrimaryAccountHoldePreviousCity;
                jointAccountInformation.FK_PreviousAddressCounrtyID = jointAccountModel.PrimaryAccountHoldePreviousCountry;
                jointAccountInformation.PreviousAddressPostalCode = jointAccountModel.PrimaryAccountHoldePreviousPostalCode;
                jointAccountInformation.TelephoneNumber = jointAccountModel.PrimaryAccountHoldeTelNumberCountryCode + "-" +
                                                        jointAccountModel.PrimaryAccountHoldeTelNumber;
                jointAccountInformation.MobileNumber = jointAccountModel.PrimaryAccountHoldeMobileNumberCountryCode + "-" +
                                                        jointAccountModel.PrimaryAccountHoldeMobileNumber;

                //jointAccountInformation.EmailAddress = jointAccountModel.PrimaryAccountHoldeEmailAddress;


                newClient.JointAccountInformations = new List<JointAccountInformation>() { jointAccountInformation };
                clientBO.AddNewClient(newClient);

                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Create account number for the client
                var pkClientAccID = AccountCreationController.CreateAccountNumberForUser(newClient);

                //Create account in trading platform if trading account type selected
                CreateTradingPlatformAccount(pkClientAccID, liveLead.FK_PlatformID, newUser, liveLead, LoginAccountType.LiveAccount);

                Session.Remove("RegisteredEmailAddress");
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }

        #endregion

        #region LIVE TRUST ACCOUNT

        /// <summary>
        /// This  Function will display Trust Account View for the Live
        /// </summary>
        /// <returns></returns>
        public ActionResult LiveTrustAccountComplete()
        {
            try
            {
                if (Session["RegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("LiveAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");
                    ViewData["TrusteeTypes"] = new SelectList(trusteeTypeValuesBO.GetTrusteeType(), "PK_TrusteeTypeID",
                                                              "TrusteeTypeName");
                    ViewData["CompanyTypes"] = new SelectList(companyTypeValuesBO.GetCompanyType(), "PK_CompanyTypeID",
                                                              "CompanyType");

                    return View("TrustAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from LiveTrustAccount View.This function will insert
        /// client and Trust information to database.
        /// </summary>
        /// <param name="trustAccountModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiveTrustAccountComplete(TrustAccountModel model)
        {
            try
            {
                TrustAccountReviewModel trustAccountReviewModel = new TrustAccountReviewModel()
                    {
                        TrustName = model.TrustName,
                        TrusteeType = trusteeTypeValuesBO.GetSelectedTrusteeType(model.TrusteeType),
                        TrustAddressLine1 = model.TrustAddressLine1,
                        TrustAddressLine2 = model.TrustAddressLine2,
                        TrustCity = model.TrustCity,
                        TrustCountry = countryBO.GetSelectedCountry(model.TrustCountry),
                        TrustPostalCode = model.TrustPostalCode,

                        TrusteeCompanyName = model.TrusteeCompanyName,
                        TrusteeCompanyType = companyTypeValuesBO.GetSelectedCompany(model.TrusteeCompanyType),
                        TrusteeAddressLine1 = model.TrusteeAddressLine1,
                        TrusteeAddressLine2 = model.TrusteeAddressLine2,
                        TrusteeCity = model.TrusteeCity,
                        TrusteeCountry = countryBO.GetSelectedCountry(model.TrusteeCountry),
                        TrusteePostalCode = model.TrusteePostalCode,

                        AuthorizedOfficerTitle = model.AuthorizedOfficerTitle == "1" ? "Mr" : "Mrs",
                        AuthorizedOfficerFirstName = model.AuthorizedOfficerFirstName,
                        AuthorizedOfficerMiddleName = model.AuthorizedOfficerMiddleName,
                        AuthorizedOfficerLastName = model.AuthorizedOfficerLastName,
                        AuthorizedOfficerDobMonth = model.AuthorizedOfficerDobMonth.ToString("D2"),
                        AuthorizedOfficerDobDay = model.AuthorizedOfficerDobDay.ToString("D2"),
                        AuthorizedOfficerDobYear = model.AuthorizedOfficerDobYear,
                        AuthorizedOfficerGender = model.AuthorizedOfficerGender ? "Male" : "Female",
                        AuthorizedOfficerCitizenship =
                            countryBO.GetSelectedCountry(model.AuthorizedOfficerCitizenship),
                        AuthorizedOfficerIdInfo =
                            idInfoTypeBO.GetSelectedIDInformation(model.AuthorizedOfficerIdInfo),
                        AuthorizedOfficerIdNumber = model.AuthorizedOfficerIdNumber,
                        AuthorizedOfficerResidenceCountry =
                            countryBO.GetSelectedCountry(model.AuthorizedOfficerResidenceCountry),


                        AuthorizedOfficerAddressLine1 = model.AuthorizedOfficerAddressLine1,
                        AuthorizedOfficerAddressLine2 = model.AuthorizedOfficerAddressLine2,
                        AuthorizedOfficerCity = model.AuthorizedOfficerCity,
                        AuthorizedOfficerCountry = countryBO.GetSelectedCountry(model.AuthorizedOfficerCountry),
                        AuthorizedOfficerPostalCode = model.AuthorizedOfficerPostalCode,
                        AuthorizedOfficerTelCountryCode = model.AuthorizedOfficerTelCountryCode,
                        AuthorizedOfficerTelephoneNumber = model.AuthorizedOfficerTelephoneNumber,
                        AuthorizedOfficerMobCountryCode = model.AuthorizedOfficerMobCountryCode,
                        AuthorizedOfficerMobileNumber = model.AuthorizedOfficerMobileNumber,

                        IndividualTitle = model.IndividualTitle == "1" ? "Mr" : "Mrs",
                        IndividualFirstName = model.IndividualFirstName,
                        IndividualMiddleName = model.IndividualLastName,
                        IndividualLastName = model.IndividualLastName,
                        IndividualDobMonth = model.IndividualDobMonth.ToString("D2"),
                        IndividualDobDay = model.IndividualDobDay.ToString("D2"),
                        IndividualDobYear = model.IndividualDobYear,
                        IndividualGender = model.IndividualGender ? "Male" : "Female",
                        IndividualCitizenship = countryBO.GetSelectedCountry(model.IndividualCitizenship),
                        IndividualIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.IndividualIdInfo),
                        IndividualIdNumber = model.IndividualIdNumber,
                        IndividualResidenceCountry = countryBO.GetSelectedCountry(model.IndividualResidenceCountry),


                        IndividualResidentialAddLine1 = model.IndividualResidentialAddLine1,
                        IndividualResidentialAddLine2 = model.IndividualResidentialAddLine2,
                        IndividualResidentialCity = model.IndividualResidentialCity,
                        IndividualResidentialCountry =
                            countryBO.GetSelectedCountry(model.IndividualResidentialCountry),
                        IndividualResidentialPostalCode = model.IndividualResidentialPostalCode,
                        IndividualYearsInCurrentAdd = model.IndividualYearsInCurrentAdd,
                        IndividualMonthsInCurrentAdd = model.IndividualMonthsInCurrentAdd,
                        IndividualPreviousAddLine1 = model.IndividualPreviousAddLine1,
                        IndividualPreviousAddLine2 = model.IndividualPreviousAddLine2,
                        IndividualPreviousCity = model.IndividualPreviousCity,
                        IndividualPreviousCountry = countryBO.GetSelectedCountry(model.IndividualPreviousCountry),
                        IndividualPreviousPostalCode = model.IndividualPreviousPostalCode,
                        IndividualTelNumberCountryCode = model.IndividualTelNumberCountryCode,
                        IndividualTelNumber = model.IndividualTelNumber,
                        IndividualMobileNumberCountryCode = model.IndividualMobileNumberCountryCode,
                        IndividualMobileNumber = model.IndividualMobileNumber,

                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId =
                            receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome =
                            annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities =
                            tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions =
                            tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange =
                            tradingExpValuesBO.GetSelectedTradingExperience(
                                model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator =
                            model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator =
                            model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        TrustModel = model,
                        IsLive = true
                    };
                return View("TrustAccountReview", trustAccountReviewModel);

            }
            catch (Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will Insert Live trust Information
        /// </summary>
        /// <param name="trustAccountModel"></param>
        public void InsertLiveTrustInformation(TrustAccountModel trustAccountModel)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["RegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["RegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_LIVE;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);
                
                //Add details to Client table
                Client newClient = new Client();

                newClient.FK_AccountID = liveLead.FK_AccountID;
                newClient.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newClient.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newClient.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newClient.FK_PrefferedLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newClient.FK_AgentID = liveLead.FK_AgentID;
                newClient.FK_IntroducingBrokerID = liveLead.FK_IntroducingBrokerID;
                newClient.IsSeen = false;
                newClient.IsDismissed = false;
                newClient.IsActivitySeen = false;
                newClient.IsActivityDismissed = false;
                newClient.Activity = "new";
                newClient.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;

                newClient.EstimatedAnnualIncome = trustAccountModel.EstimatedAnnualIncome.ToString();
                newClient.LiquidAssets = trustAccountModel.LiquidAssets.ToString();
                newClient.NetWorthInEuros = trustAccountModel.NetWorthEuros.ToString();
                newClient.FK_TradingSecurityExperienceID = trustAccountModel.DrpHaveExperienceTradingSecurities;
                newClient.FK_TradingOptionExperienceID = trustAccountModel.DrpHaveExperienceTradingOptions;
                newClient.FK_TradingForeignExchangeExperienceID = trustAccountModel.DrpHaveExperienceTradingForeignExchange;
                newClient.IsHavingAccount = trustAccountModel.HaveAccWithFqSecurities;
                newClient.IsRegisterdWithEntity = trustAccountModel.RegisteredPerson;
                newClient.IsRegisteredWithRegulator = trustAccountModel.RequiredToBeRegisteredWithRegulator;
                newClient.IsEmployeeOfExchangeOrRegulator = trustAccountModel.EmployeeOfExchangeOrRegulatorOperator;
                newClient.IsDeclaredBankruptcy = trustAccountModel.EverDeclaredBankruptcy;
                newClient.PIC = "";
                newClient.FK_UserID = newUser.PK_UserID;
                newClient.Status = "Missing Documents";
                newClient.FK_OrganizationID = (int)organizationID;

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = trustAccountModel.BankName,
                    AccountNumber = trustAccountModel.AccountNumber,
                    BicNumberOrSwiftCode = trustAccountModel.BicOrSwiftCode,
                    BankingAddress = trustAccountModel.BankAddLine1 + "@" + trustAccountModel.BankAddLine2,
                    FK_ReceivingBankInformationID = trustAccountModel.ReceivingBankInfoId,
                    ReceivingBankInfo = trustAccountModel.ReceivingBankInfo,
                    City = trustAccountModel.BankCity,
                    FK_CountryID = trustAccountModel.BankCountry,
                    PostalCode = trustAccountModel.BankPostalCode.ToString()
                };

                newClient.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };

                //Add details to IndividualAccountInformation table
                TrustAccountInformation trustAccountInformation = new TrustAccountInformation()
                {
                    TrustName = trustAccountModel.TrustName,
                    FK_TrusteeTypeID = trustAccountModel.TrusteeType,
                    TrustAddress = trustAccountModel.TrustAddressLine1 + "@" + trustAccountModel.TrustAddressLine2,
                    TrustCity = trustAccountModel.TrustCity,
                    FK_TrusteeCountryID = trustAccountModel.TrustCountry,
                    TrustPostalCode = trustAccountModel.TrustPostalCode
                };

                if (trustAccountModel.TrusteeType == Constants.K_TRUSTEETYPE_INDIVIDUAL)
                {
                    //Individual Information

                    trustAccountInformation.TrusteeIndividualTitle = trustAccountModel.IndividualTitle;
                    trustAccountInformation.TrusteeIndividualFirstName = trustAccountModel.IndividualFirstName;
                    trustAccountInformation.TrusteeIndividualLastName = trustAccountModel.IndividualLastName;
                    trustAccountInformation.TrusteeIndividualMiddleName = trustAccountModel.IndividualMiddleName;
                    trustAccountInformation.TrusteeIndividualBirthDate =
                            new DateTime(trustAccountModel.IndividualDobYear, trustAccountModel.IndividualDobMonth, trustAccountModel.IndividualDobDay);
                    trustAccountInformation.TrusteeIndividualGender = trustAccountModel.IndividualGender;
                    trustAccountInformation.FK_TrusteeIndividualCitizenshipID = trustAccountModel.IndividualCitizenship;
                    trustAccountInformation.FK_TrusteeIndividualIDTypeId = trustAccountModel.IndividualIdInfo;
                    trustAccountInformation.TrusteeIndividualIDNumber = trustAccountModel.IndividualIdNumber;
                    trustAccountInformation.FK_TrusteeIndividualResidenceCountryID = trustAccountModel.IndividualResidenceCountry;

                    //Individual Contact Information
                    trustAccountInformation.TrusteeIndividualResidentialAddress = trustAccountModel.IndividualResidentialAddLine1 + "@" +
                                                                                 trustAccountModel.IndividualResidentialAddLine2;
                    trustAccountInformation.TrusteeIndividualResidentialCity = trustAccountModel.IndividualResidentialCity;
                    trustAccountInformation.FK_TrusteeIndividualResidentialCountryID = trustAccountModel.IndividualResidentialCountry;
                    trustAccountInformation.TrusteeIndividualResidentialPostalCode = trustAccountModel.IndividualResidentialPostalCode.ToString();
                    trustAccountInformation.TrusteeIndividualTotalMonthsAtAddress = (trustAccountModel.IndividualYearsInCurrentAdd * 12)
                                                                                    + trustAccountModel.IndividualMonthsInCurrentAdd;
                    trustAccountInformation.TrusteeIndividualPreviousAddress = trustAccountModel.IndividualPreviousAddLine1 + "@" +
                                                                                 trustAccountModel.IndividualPreviousAddLine2;

                    trustAccountInformation.TrusteeIndividualPreviousCity = trustAccountModel.IndividualPreviousCity;
                    trustAccountInformation.FK_TrusteeIndividualPreviousCountryID = (trustAccountModel.IndividualPreviousCountry != 0) ?
                                                                                    (int?)trustAccountModel.IndividualPreviousCountry : null;
                    trustAccountInformation.TrusteeIndividualPreviousPostalCode = trustAccountModel.IndividualPreviousPostalCode;
                    trustAccountInformation.TrusteeIndividualTelephoneNumber = trustAccountModel.IndividualTelNumberCountryCode + "-" +
                                                                                trustAccountModel.IndividualTelNumber;
                    trustAccountInformation.TrusteeIndividualMobileNumber = trustAccountModel.IndividualMobileNumberCountryCode + "-" +
                                                                            trustAccountModel.IndividualMobileNumber;
                }
                else if (trustAccountModel.TrusteeType == Constants.K_TRUSTEETYPE_COMPANY)
                {
                    //Company Information
                    trustAccountInformation.TrusteeCompanyName = trustAccountModel.TrusteeCompanyName;
                    trustAccountInformation.FK_TrusteeCompanyTypeID = trustAccountModel.TrusteeCompanyType;
                    trustAccountInformation.TrusteeAddress = trustAccountModel.TrusteeAddressLine1 + "@" + trustAccountModel.TrusteeAddressLine2;
                    trustAccountInformation.TrusteeCity = trustAccountModel.TrusteeCity;
                    trustAccountInformation.FK_TrusteeCountryID = trustAccountModel.TrusteeCountry;
                    trustAccountInformation.TrusteePostalCode = trustAccountModel.TrusteePostalCode;

                    //Trustee Company Authorized Officer Information
                    trustAccountInformation.TrusteeAuthOfficerTitle = trustAccountModel.AuthorizedOfficerTitle;
                    trustAccountInformation.TrusteeAuthOfficerFirstName = trustAccountModel.AuthorizedOfficerFirstName;
                    trustAccountInformation.TrusteeAuthOfficerMiddleName = trustAccountModel.AuthorizedOfficerMiddleName;
                    trustAccountInformation.TrusteeAuthOfficerLastName = trustAccountModel.AuthorizedOfficerLastName;
                    trustAccountInformation.TrusteeAuthOfficerBirthDate =
                            new DateTime(trustAccountModel.AuthorizedOfficerDobYear,
                                trustAccountModel.AuthorizedOfficerDobMonth,
                                trustAccountModel.AuthorizedOfficerDobDay);
                    trustAccountInformation.TrusteeAuthOfficerGender = trustAccountModel.AuthorizedOfficerGender;
                    trustAccountInformation.FK_TrusteeAuthOfficerCitizenshipCountryID = trustAccountModel.AuthorizedOfficerCitizenship;
                    trustAccountInformation.FK_TrusteeAuthOfficerIDType = trustAccountModel.AuthorizedOfficerIdInfo;
                    trustAccountInformation.TrusteeAuthOfficerIDNumber = trustAccountModel.AuthorizedOfficerIdNumber;
                    trustAccountInformation.FK_TrusteeAuthOfficerResidenceCountryID = trustAccountModel.AuthorizedOfficerResidenceCountry;

                    //Trustee Company Authorized Officer Contact Information
                    trustAccountInformation.TrusteeAuthOfficerAddrerss = trustAccountModel.AuthorizedOfficerAddressLine1
                                                                        + "@" + trustAccountModel.AuthorizedOfficerAddressLine2;
                    trustAccountInformation.TrusteeAuthOfficerCity = trustAccountModel.AuthorizedOfficerCity;
                    trustAccountInformation.FK_TrusteeAuthOfficerCountryID = trustAccountModel.AuthorizedOfficerCountry;
                    trustAccountInformation.TrusteeAuthOfficerPostalCode = trustAccountModel.AuthorizedOfficerPostalCode;
                    trustAccountInformation.TrusteeAuthOfficerTelephoneNumber = trustAccountModel.AuthorizedOfficerTelCountryCode + "-"
                                                                                + trustAccountModel.AuthorizedOfficerTelephoneNumber;
                    trustAccountInformation.TrusteeAuthOfficerMobileNumber = trustAccountModel.AuthorizedOfficerMobCountryCode + "-"
                                                                            + trustAccountModel.AuthorizedOfficerMobileNumber;
                }



                newClient.TrustAccountInformations = new List<TrustAccountInformation>() { trustAccountInformation };
                clientBO.AddNewClient(newClient);

                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Create account number for the client
                int pkClientAccID = AccountCreationController.CreateAccountNumberForUser(newClient);

                //Create account in trading platform if trading account type selected
                CreateTradingPlatformAccount(pkClientAccID, liveLead.FK_PlatformID, newUser, liveLead, LoginAccountType.LiveAccount);

                Session.Remove("RegisteredEmailAddress");                
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }

        #endregion

        #region LIVE INDIVIDUAL ACCOUNT

        /// <summary>
        /// This action returns IndividualForm view
        /// </summary>
        /// <returns></returns>
        public ActionResult LiveIndividualAccountComplete()
        {
            try
            {
                if (Session["RegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("LiveAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                    return View("IndividualAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This action inserts individual account details in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult LiveIndividualAccountComplete(IndividualAccountModel model)
        {          
            try
            {
                if (ModelState.IsValid)
                {                    
                    IndividualAccountReviewModel individualAccountReviewModel = new IndividualAccountReviewModel()
                    {
                        Title = model.Title == "1" ? "Mr" : "Mrs",
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        DobMonth = model.DobMonth.ToString("D2"),
                        DobDay = model.DobDay.ToString("D2"),
                        DobYear = model.DobYear,
                        Gender = model.Gender ? "Male" : "Female",
                        Citizenship = countryBO.GetSelectedCountry(model.Citizenship),
                        IdInfo = idInfoTypeBO.GetSelectedIDInformation(model.IdInfo),
                        IdNumber = model.IdNumber,
                        ResidenceCountry = countryBO.GetSelectedCountry(model.ResidenceCountry),
                        ResidentialAddLine1 = model.ResidentialAddLine1,
                        ResidentialAddLine2 = model.ResidentialAddLine2,
                        ResidentialCity = model.ResidentialCity,
                        ResidentialCountry = countryBO.GetSelectedCountry(model.ResidentialCountry),
                        ResidentialPostalCode = model.ResidentialPostalCode,
                        YearsInCurrentAdd = model.YearsInCurrentAdd,
                        MonthsInCurrentAdd = model.MonthsInCurrentAdd,
                        PreviousAddLine1 = model.PreviousAddLine1,
                        PreviousAddLine2 = model.PreviousAddLine2,
                        PreviousCity = model.PreviousCity,
                        PreviousCountry = model.PreviousCountry != null ? countryBO.GetSelectedCountry((int)model.PreviousCountry) : null,
                        PreviousPostalCode = model.PreviousPostalCode,
                        TelNumberCountryCode = model.TelNumberCountryCode,
                        TelNumber = model.TelNumber,
                        MobileNumberCountryCode = model.MobileNumberCountryCode,
                        MobileNumber = model.MobileNumber,
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        IndividualModel = model,
                        IsLive = true
                    };
                    return View("IndividualAccountReview", individualAccountReviewModel);
                    
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                       "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View("IndividualAccount", model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// Insert Individual Live Information
        /// </summary>
        /// <param name="model"></param>
        public void InsertLiveIndividualInformation(IndividualAccountModel model)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["RegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["RegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_LIVE;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);

                //Add details to Client table
                Client newClient = new Client();
                newClient.FK_AccountID = liveLead.FK_AccountID;
                newClient.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newClient.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newClient.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newClient.FK_PrefferedLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newClient.FK_AgentID = liveLead.FK_AgentID;
                newClient.FK_IntroducingBrokerID = liveLead.FK_IntroducingBrokerID;
                newClient.IsSeen = false;
                newClient.IsDismissed = false;
                newClient.IsActivitySeen = false;
                newClient.IsActivityDismissed = false;
                newClient.Activity = "new";
                newClient.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;

                newClient.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                newClient.LiquidAssets = model.LiquidAssets.ToString();
                newClient.NetWorthInEuros = model.NetWorthEuros.ToString();
                newClient.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                newClient.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                newClient.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                newClient.IsHavingAccount = model.HaveAccWithFqSecurities;
                newClient.IsRegisterdWithEntity = model.RegisteredPerson;
                newClient.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                newClient.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                newClient.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                newClient.PIC = "";
                newClient.FK_UserID = newUser.PK_UserID;
                newClient.Status = "Missing Documents";
                newClient.FK_OrganizationID = (int)organizationID;

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    BicNumberOrSwiftCode = model.BicOrSwiftCode,
                    BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2,
                    FK_ReceivingBankInformationID = model.ReceivingBankInfoId,
                    ReceivingBankInfo = model.ReceivingBankInfo,
                    City = model.BankCity,
                    FK_CountryID = model.BankCountry,
                    PostalCode = model.BankPostalCode.ToString()
                };

                newClient.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };

                clientBO.AddNewClient(newClient);

                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Add details to IndividualAccountInformation table
                IndividualAccountInformation individualAccount = new IndividualAccountInformation();
                individualAccount.Title = model.Title;
                individualAccount.FirstName = model.FirstName;
                individualAccount.MiddleName = model.MiddleName;
                individualAccount.LastName = model.LastName;
                individualAccount.BirthDate = new DateTime(model.DobYear, model.DobMonth, model.DobDay);
                individualAccount.Gender = model.Gender;
                individualAccount.FK_CitizenShipCountryID = model.Citizenship;
                individualAccount.FK_IDInformationID = model.IdInfo;
                individualAccount.IDNumber = model.IdNumber;
                individualAccount.FK_ResidenceCountryID = model.ResidenceCountry;
                individualAccount.ResidentialAddress = model.ResidentialAddLine1 + "@" + model.ResidentialAddLine2;
                individualAccount.ResidentialAddressCity = model.ResidentialCity;
                individualAccount.FK_ResidentialAddressCountryID = model.ResidentialCountry;
                individualAccount.ResidentialAddressPostalCode = model.ResidentialPostalCode.ToString();
                individualAccount.MonthsAtCurrentAddress = (model.YearsInCurrentAdd * 12) + model.MonthsInCurrentAdd;
                individualAccount.PreviousAddress = model.PreviousAddLine1 + "@" + model.PreviousAddLine2;
                individualAccount.PreviousAddressCity = model.PreviousCity;
                individualAccount.FK_PreviousAddressCounrtyID = model.PreviousCountry;
                individualAccount.PreviousAddressPostalCode = model.PreviousPostalCode;
                individualAccount.TelephoneNumber = model.TelNumberCountryCode + "-" + model.TelNumber;
                individualAccount.MobileNumber = model.MobileNumberCountryCode + "-" + model.MobileNumber;
                individualAccount.FK_ClientID = newClient.PK_ClientID;

                individualAccountInformationBO.AddIndividualAccDetailsForNewClient(individualAccount);

                //Create account number for the client
                int pkClientAccID = AccountCreationController.CreateAccountNumberForUser(newClient);

                //Create account in trading platform if trading account type selected
                CreateTradingPlatformAccount(pkClientAccID, liveLead.FK_PlatformID, newUser, liveLead, LoginAccountType.LiveAccount);

                Session.Remove("RegisteredEmailAddress");
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }

        #endregion

        #region LIVE CORPORATE ACCOUNT

        /// <summary>
        /// This action returns CorporateForm view
        /// </summary>
        /// <returns></returns>
        public ActionResult LiveCorporateAccountComplete()
        {
            try
            {
                if (Session["RegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("LiveAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["CompanyTypes"] = new SelectList(companyTypeValuesBO.GetCompanyType(), "PK_CompanyTypeID",
                                                              "CompanyType");
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                    return View("CorporateAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This action inserts corporate account details for Live in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiveCorporateAccountComplete(CorporateAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CorporateAccountReviewModel individualAccountReviewModel = new CorporateAccountReviewModel()
                    {
                        CompanyName = model.CompanyName,
                        CompanyType = companyTypeValuesBO.GetSelectedCompany(model.CompanyType),
                        CompanyAddLine1 = model.CompanyAddLine1,
                        CompanyAddLine2 = model.CompanyAddLine2,
                        CompanyCity = model.CompanyCity,
                        CompanyCountry = countryBO.GetSelectedCountry(model.CompanyCountry),
                        CompanyPostalCode = model.CompanyPostalCode,

                        AuthOfficerTitle = model.AuthOfficerTitle == "1" ? "Mr" : "Mrs",
                        AuthOfficerFirstName = model.AuthOfficerFirstName,
                        AuthOfficerMiddleName = model.AuthOfficerMiddleName,
                        AuthOfficerLastName = model.AuthOfficerLastName,
                        AuthOfficerDobMonth = model.AuthOfficerDobMonth.ToString("D2"),
                        AuthOfficerDobDay = model.AuthOfficerDobDay.ToString("D2"),
                        AuthOfficerDobYear = model.AuthOfficerDobYear,
                        AuthOfficerGender = model.AuthOfficerGender ? "Male" : "Female",
                        AuthOfficerCitizenship = countryBO.GetSelectedCountry(model.AuthOfficerCitizenship),
                        AuthOfficerIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.AuthOfficerIdInfo),
                        AuthOfficerIdNumber = model.AuthOfficerIdNumber,

                        AuthOfficerResidenceCountry = countryBO.GetSelectedCountry(model.AuthOfficerResidenceCountry),
                        AuthOfficerAddLine1 = model.AuthOfficerAddLine1,
                        AuthOfficerAddLine2 = model.AuthOfficerAddLine2,
                        AuthOfficerCity = model.AuthOfficerCity,
                        AuthOfficerCountry = countryBO.GetSelectedCountry(model.AuthOfficerCountry),
                        AuthOfficerPostalCode = model.AuthOfficerPostalCode,
                        AuthOfficerTelNumberCountryCode = model.AuthOfficerTelNumberCountryCode,
                        AuthOfficerTelNumber = model.AuthOfficerTelNumber,
                        AuthOfficerMobileNumberCountryCode = model.AuthOfficerMobileNumberCountryCode,
                        AuthOfficerMobileNumber = model.AuthOfficerMobileNumber,
                        
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        CorporateModel= model,
                        IsLive = true
                    };
                    return View("CorporateAccountReview", individualAccountReviewModel);
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["CompanyTypes"] = new SelectList(companyTypeValuesBO.GetCompanyType(),
                                                                  "PK_CompanyTypeID", "CompanyType");
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                       "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View(model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }
      
        /// <summary>
        /// This function will insert Live Corporate Information
        /// </summary>
        /// <param name="model"></param>
        public void InsertLiveCorporateInformation(CorporateAccountModel model)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["RegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["RegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_LIVE;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);
                
                //Add details to Client table
                Client newClient = new Client();
                newClient.FK_AccountID = liveLead.FK_AccountID;
                newClient.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newClient.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newClient.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newClient.FK_PrefferedLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newClient.FK_AgentID = liveLead.FK_AgentID;
                newClient.FK_IntroducingBrokerID = liveLead.FK_IntroducingBrokerID;
                newClient.IsSeen = false;
                newClient.IsDismissed = false;
                newClient.IsActivitySeen = false;
                newClient.IsActivityDismissed = false;
                newClient.Activity = "new";
                newClient.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;

                newClient.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                newClient.LiquidAssets = model.LiquidAssets.ToString();
                newClient.NetWorthInEuros = model.NetWorthEuros.ToString();
                newClient.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                newClient.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                newClient.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                newClient.IsHavingAccount = model.HaveAccWithFqSecurities;
                newClient.IsRegisterdWithEntity = model.RegisteredPerson;
                newClient.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                newClient.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                newClient.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                newClient.PIC = "";
                newClient.FK_UserID = newUser.PK_UserID;
                newClient.Status = "Missing Documents";
                newClient.FK_OrganizationID = (int)organizationID;

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    BicNumberOrSwiftCode = model.BicOrSwiftCode,
                    BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2,
                    FK_ReceivingBankInformationID = model.ReceivingBankInfoId,
                    ReceivingBankInfo = model.ReceivingBankInfo,
                    City = model.BankCity,
                    FK_CountryID = model.BankCountry,
                    PostalCode = model.BankPostalCode.ToString()
                };

                newClient.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };


                clientBO.AddNewClient(newClient);
                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Add details to CorporateAccountInformation table
                CorporateAccountInformation corporateAcc = new CorporateAccountInformation();
                corporateAcc.CompanyName = model.CompanyName;
                corporateAcc.FK_CompanyTypeID = model.CompanyType;
                corporateAcc.CompanyAddress = model.CompanyAddLine1 + "@" + model.CompanyAddLine2;
                corporateAcc.City = model.CompanyCity;
                corporateAcc.FK_CompanyCountryID = model.CompanyCountry;
                corporateAcc.CompanyPostalCode = model.CompanyPostalCode.ToString();
                corporateAcc.AuthOfficerTitle = model.AuthOfficerTitle;
                corporateAcc.AuthOfficerFirstName = model.AuthOfficerFirstName;
                corporateAcc.AuthOfficerMiddleName = model.AuthOfficerMiddleName;
                corporateAcc.AuthOfficerLastName = model.AuthOfficerLastName;
                corporateAcc.AuthOfficerBirthDate = new DateTime(model.AuthOfficerDobYear, model.AuthOfficerDobMonth, model.AuthOfficerDobDay);
                corporateAcc.AuthOfficerGender = model.AuthOfficerGender;
                corporateAcc.FK_AuthOfficerCitizenshipCountryID = model.AuthOfficerCitizenship;
                corporateAcc.FK_AuthOfficerInformationTypeID = model.AuthOfficerIdInfo;
                corporateAcc.AuthOfficerIDNumber = model.AuthOfficerIdNumber;
                corporateAcc.FK_AuthOfficerResidenceCountryID = model.AuthOfficerResidenceCountry;
                corporateAcc.AuthOfficerAddress = model.AuthOfficerAddLine1 + "@" + model.AuthOfficerAddLine2;
                corporateAcc.AuthOfficerCity = model.AuthOfficerCity;
                corporateAcc.FK_AuthOfficerCountryID = model.AuthOfficerCountry;
                corporateAcc.AuthOfficerPostalCode = model.AuthOfficerPostalCode.ToString();
                corporateAcc.AuthOfficerTelephoneNumber = model.AuthOfficerTelNumberCountryCode + "-" + model.AuthOfficerTelNumber;
                corporateAcc.AuthOfficerMobileNumber = model.AuthOfficerMobileNumberCountryCode + "-" + model.AuthOfficerMobileNumber;
                corporateAcc.FK_ClientID = newClient.PK_ClientID;

                corporateAccountInformationBO.AddCorporateAccDetailsForNewClient(corporateAcc);

                //Create account number for the client
                int pkClientAccID = AccountCreationController.CreateAccountNumberForUser(newClient);

                //Create account in trading platform if trading account type selected
                CreateTradingPlatformAccount(pkClientAccID, liveLead.FK_PlatformID, newUser, liveLead, LoginAccountType.LiveAccount);

                Session.Remove("RegisteredEmailAddress");
                
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
            
        }

        #endregion

        #region PARTNERS ACCOUNT INITIAL

        /// <summary>
        /// This Function will display PartnersAccountInitial View
        /// </summary>
        /// <returns></returns>
        public ActionResult PartnerAccountInitial()
        {
            try
            {
                //var OrganizationBO = new OrganizationBO();
                //var browserUrl = Request.Url.AbsoluteUri;

                ////browserUrl = "http://fqsecurities.com"; //TODO Need to change with URL
                //var organizationID = OrganizationBO.GetOrganizationIDFromURL(browserUrl);

                var organizationID = OrganizationUtility.GetOrganizationID(Request.Url.AbsoluteUri);

                if (organizationID != null)
                {
                    //Add Organization ID to Session
                    SessionManagement.OrganizationID = organizationID;

                    ViewData["AccountCode"] =
                        new SelectList(accountCodeBO.GetSelectedAccount(Constants.K_BROKER_PARTNER), "PK_AccountID",
                                       "AccountName");
                    ViewData["AccountCurrency"] =
                        new SelectList(accountCurrencyBO.GetSelectedCurrency(Constants.K_BROKER_PARTNER, (int)organizationID),
                                       "PK_AccountCurrencyID", "L_CurrencyValue.CurrencyValue");
                    ViewData["AccountType"] =
                        new SelectList(accountTypeBO.GetSelectedAccountType(Constants.K_BROKER_PARTNER, (int)organizationID),
                                       "PK_AccountType", "L_AccountTypeValue.AccountTypeValues");
                    ViewData["TradingPlatform"] =
                        new SelectList(tradingPlatformBO.GetSelectedPlatform(Constants.K_BROKER_PARTNER, (int)organizationID),
                                       "PK_TradingPlatformID", "L_TradingPlatformValues.TradingValue");
                    ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");
                    ViewData["WideSpreads"] = new SelectList(widenSpreadValuesBO.GetWidenSpreadValues(),
                                                             "PK_WidenSpreadsID", "WidenSpreadsValue");
                    ViewData["IncreasedCommissions"] =
                        new SelectList(commissionIncremantValuesBO.GetCommissionIncrementValues(),
                                       "PK_CommissionIncrementID", "CommissionIncrementValue");

                    return View("PartnerAccountInitial");
                }
                else
                {
                    //return View("ErrorMessage");
                    return RedirectToAction("PageNotFound", "Error");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from PartnersAccountInitial View.This function will insert
        /// new Live lead to the database.
        /// </summary>
        /// <param name="partnerAccountInitialModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartnerAccountInitial(PartnerAccountInitialModel partnerAccountInitialModel)
        {
            try
            {
                //Creating Instance
                currentDeskSecurity = new CurrentDeskSecurity();
                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    if (ModelState.IsValid)
                    {
                        //Creating New Live Lead 
                        var newLiveLead = new LiveLead();
                        newLiveLead.FK_AccountID = partnerAccountInitialModel.AccountCode;
                        newLiveLead.FK_AccountTypeID = partnerAccountInitialModel.AccountType;
                        newLiveLead.FK_PlatformID = partnerAccountInitialModel.TradingPlatform;
                        newLiveLead.FK_AccountCurrencyID = partnerAccountInitialModel.AccountCurrency;
                        newLiveLead.EmailAddress = partnerAccountInitialModel.UserEmail;
                        newLiveLead.Password = currentDeskSecurity.SetPassEncrypted(partnerAccountInitialModel.Password);
                        newLiveLead.FK_PreferedCommunicationLanguage = partnerAccountInitialModel.LanguageCode;
                        newLiveLead.IsEnglishSpeaking = partnerAccountInitialModel.IsEnglishSpeaking;
                        newLiveLead.FK_OrganizationID = (int)organizationID;
                        newLiveLead.FK_WidenSpreadsID = partnerAccountInitialModel.WidenSpread;
                        if (partnerAccountInitialModel.AccountCode == Constants.K_ACCTCODE_AM)
                        {
                            newLiveLead.FK_CommissionIncrementID = partnerAccountInitialModel.IncreasedCommission;
                        }
                        if (partnerAccountInitialModel.WidenSpread == 7)
                        {
                            newLiveLead.WidenSpreadValue = partnerAccountInitialModel.WidenSpreadOthers;
                        }
                        if (partnerAccountInitialModel.IncreasedCommission == 6 &&
                            partnerAccountInitialModel.AccountCode == Constants.K_ACCTCODE_AM)
                        {
                            newLiveLead.CommissionValue = partnerAccountInitialModel.IncreasedCommissionOthers;
                        }


                        //Instantiating Business Object for Live lead and adding to the database.
                        var liveLeadBO = new LiveLeadBO();
                        liveLeadBO.AddNewLiveLead(newLiveLead);

                        //Storing Registered Email in Session 
                        Session["PartnersRegisteredEmailAddress"] = partnerAccountInitialModel.UserEmail;

                        //Get account type
                        var accountType = accountTypeBO.GetAccountTypeValue(partnerAccountInitialModel.AccountType);

                        //Depending upon selection move to corresponding view
                        if (accountType == Constants.K_ACCT_INDIVIDUAL)
                        {
                            return RedirectToAction("PartnerIndividualAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_JOINT)
                        {
                            return RedirectToAction("PartnersJointAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_CORPORATE)
                        {
                            return RedirectToAction("PartnerCorporateAccountComplete");
                        }
                        else if (accountType == Constants.K_ACCT_TRUST)
                        {
                            return RedirectToAction("PartnersTrustAccountComplete");
                        }
                        else
                        {
                            return View("ErrorMessage");
                        }
                    }
                    else
                    {


                        ViewData["AccountCode"] = new SelectList(accountCodeBO.GetSelectedAccount(Constants.K_BROKER_PARTNER), "PK_AccountID",
                                                                 "AccountName");
                        ViewData["AccountCurrency"] = new SelectList(accountCurrencyBO.GetSelectedCurrency(Constants.K_BROKER_PARTNER, (int)organizationID),
                                                                     "PK_AccountCurrencyID",
                                                                     "L_CurrencyValue.CurrencyValue");
                        ViewData["AccountType"] = new SelectList(accountTypeBO.GetSelectedAccountType(Constants.K_BROKER_PARTNER, (int)organizationID),
                                                                 "PK_AccountType",
                                                                 "L_AccountTypeValue.AccountTypeValues");
                        ViewData["TradingPlatform"] = new SelectList(tradingPlatformBO.GetSelectedPlatform(Constants.K_BROKER_PARTNER, (int)organizationID),
                                                                     "PK_TradingPlatformID",
                                                                     "L_TradingPlatformValues.TradingValue");
                        ViewData["Language"] = new SelectList(languageBO.GetLanguages(), "PK_LanguageID", "LanguageName");
                        ViewData["WideSpreads"] = new SelectList(widenSpreadValuesBO.GetWidenSpreadValues(),
                                                                 "PK_WidenSpreadsID", "WidenSpreadsValue");
                        ViewData["IncreasedCommissions"] =
                            new SelectList(commissionIncremantValuesBO.GetCommissionIncrementValues(),
                                           "PK_CommissionIncrementID", "CommissionIncrementValue");

                        return View(partnerAccountInitialModel);
                    }
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }


        #endregion

        #region PARTNERS JOINT ACCOUNT

        /// <summary>
        ///This Function will display Joint Account View for the Partner
        /// </summary>
        /// <returns></returns>
        public ActionResult PartnersJointAccountComplete()
        {
            try
            {
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("PartnerAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");


                    return View("JointAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will called when user post's
        /// data from PartnersJointAccount View.This function will insert
        /// client and joint information to database.
        /// </summary>
        /// <param name="jointAccountModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartnersJointAccountComplete(JointAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    JointAccountReviewModel jointAccountReviewModel = new JointAccountReviewModel()
                    {
                        PrimaryAccountHolderTitle = model.PrimaryAccountHolderTitle == "1" ? "Mr" : "Mrs",
                        PrimaryAccountHolderFirstName = model.PrimaryAccountHolderFirstName,
                        PrimaryAccountHolderMiddleName = model.PrimaryAccountHolderMiddleName,
                        PrimaryAccountHolderLastName = model.PrimaryAccountHolderLastName,
                        PrimaryAccountHolderDobMonth = model.PrimaryAccountHolderDobMonth.ToString("D2"),
                        PrimaryAccountHolderDobDay = model.PrimaryAccountHolderDobDay.ToString("D2"),
                        PrimaryAccountHolderDobYear = model.PrimaryAccountHolderDobYear,
                        PrimaryAccountHolderGender = model.PrimaryAccountHolderGender ? "Male" : "Female",
                        PrimaryAccountHolderCitizenship = countryBO.GetSelectedCountry(model.PrimaryAccountHolderCitizenship),
                        PrimaryAccountHolderIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.PrimaryAccountHolderIdInfo),
                        PrimaryAccountHolderIdNumber = model.PrimaryAccountHolderIdNumber,
                        PrimaryAccountHolderResidenceCountry = countryBO.GetSelectedCountry(model.PrimaryAccountHolderResidenceCountry),

                        SecondaryAccountHolderTitle = model.SecondaryAccountHolderTitle == "1" ? "Mr" : "Mrs",
                        SecondaryAccountHolderFirstName = model.SecondaryAccountHolderFirstName,
                        SecondaryAccountHolderMiddleName = model.SecondaryAccountHolderMiddleName,
                        SecondaryAccountHolderLastName = model.SecondaryAccountHolderLastName,
                        SecondaryAccountHolderDobMonth = model.SecondaryAccountHolderDobMonth.ToString("D2"),
                        SecondaryAccountHolderDobDay = model.SecondaryAccountHolderDobDay.ToString("D2"),
                        SecondaryAccountHolderDobYear = model.SecondaryAccountHolderDobYear,
                        SecondaryAccountHolderGender = model.SecondaryAccountHolderGender ? "Male" : "Female",
                        SecondaryAccountHolderCitizenship = countryBO.GetSelectedCountry(model.SecondaryAccountHolderCitizenship),
                        SecondaryAccountHolderIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.SecondaryAccountHolderIdInfo),
                        SecondaryAccountHolderIdNumber = model.SecondaryAccountHolderIdNumber,
                        SecondaryAccountHolderResidenceCountry = countryBO.GetSelectedCountry(model.SecondaryAccountHolderResidenceCountry),


                        PrimaryAccountHolderResidentialAddLine1 = model.PrimaryAccountHolderResidentialAddLine1,
                        PrimaryAccountHoldeResidentialAddLine2 = model.PrimaryAccountHoldeResidentialAddLine2,
                        PrimaryAccountHoldeResidentialCity = model.PrimaryAccountHoldeResidentialCity,
                        PrimaryAccountHoldeResidentialCountry = countryBO.GetSelectedCountry(model.PrimaryAccountHoldeResidentialCountry),
                        PrimaryAccountHoldeResidentialPostalCode = model.PrimaryAccountHoldeResidentialPostalCode,
                        PrimaryAccountHoldeYearsInCurrentAdd = model.PrimaryAccountHoldeYearsInCurrentAdd,
                        PrimaryAccountHoldeMonthsInCurrentAdd = model.PrimaryAccountHoldeMonthsInCurrentAdd,
                        PrimaryAccountHoldePreviousAddLine1 = model.PrimaryAccountHoldePreviousAddLine1,
                        PrimaryAccountHoldePreviousAddLine2 = model.PrimaryAccountHoldePreviousAddLine2,
                        PrimaryAccountHoldePreviousCity = model.PrimaryAccountHoldePreviousCity,
                        PrimaryAccountHoldePreviousCountry = model.PrimaryAccountHoldePreviousCountry != null ? countryBO.GetSelectedCountry((int)model.PrimaryAccountHoldePreviousCountry) : null,
                        PrimaryAccountHoldePreviousPostalCode = model.PrimaryAccountHoldePreviousPostalCode,
                        PrimaryAccountHoldeTelNumberCountryCode = model.PrimaryAccountHoldeTelNumberCountryCode,
                        PrimaryAccountHoldeTelNumber = model.PrimaryAccountHoldeTelNumber,
                        PrimaryAccountHoldeMobileNumberCountryCode = model.PrimaryAccountHoldeMobileNumberCountryCode,
                        PrimaryAccountHoldeMobileNumber = model.PrimaryAccountHoldeMobileNumber,
                        
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        JointModel = model,
                        IsLive = false

                    };
                    return View("JointAccountReview", jointAccountReviewModel);
                    
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                       "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View("JointAccount", model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This function will insert partners joint information
        /// </summary>
        /// <param name="jointAccountModel"></param>
        public void InsertPartnerJointInformation(JointAccountModel jointAccountModel)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["PartnersRegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_PARTNER;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);

                //Adding widenspread and commission
                PartnerCommission partComm = new PartnerCommission();
                partComm.FK_WidenSpreadID = liveLead.FK_WidenSpreadsID;
                partComm.FK_CommissionID = liveLead.FK_CommissionIncrementID;
                partComm.WidenSpreadValue = liveLead.WidenSpreadValue;
                partComm.CommissionValue = liveLead.CommissionValue;
                partComm.FeeStructureName = "Initial Fee Structure";
                partComm.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                partComm.FK_UserID = newUser.PK_UserID;
                partComm.IsDefault = true;

                partCommBO.AddNewPartnerSpread(partComm);

                IntroducingBroker newIB = new IntroducingBroker()
                {
                    FK_AccountID = liveLead.FK_AccountID,
                    FK_AccountTypeID = liveLead.FK_AccountTypeID,
                    FK_TradingPlatformID = liveLead.FK_PlatformID,
                    FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID,
                    FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage,
                    IsEnglishSpeaking = liveLead.IsEnglishSpeaking,
                    BankName = jointAccountModel.BankName,
                    AccountNumber = jointAccountModel.AccountNumber,
                    BicNumberOrSwiftCode = jointAccountModel.BicOrSwiftCode,
                    BankingAddress = jointAccountModel.BankAddLine1 + "@" + jointAccountModel.BankAddLine2,
                    FK_ReceivingBankInformationID = jointAccountModel.ReceivingBankInfoId,
                    ReceivingBankInfo = jointAccountModel.ReceivingBankInfo,
                    City = jointAccountModel.BankCity,
                    FK_CountryID = jointAccountModel.BankCountry,
                    PostalCode = jointAccountModel.BankPostalCode.ToString(),
                    EstimatedAnnualIncome = jointAccountModel.EstimatedAnnualIncome.ToString(),
                    LiquidAssets = jointAccountModel.LiquidAssets.ToString(),
                    NetWorthInEuros = jointAccountModel.NetWorthEuros.ToString(),
                    FK_TradingSecurityExperienceID = jointAccountModel.DrpHaveExperienceTradingSecurities,
                    FK_TradingOptionExperienceID = jointAccountModel.DrpHaveExperienceTradingOptions,
                    FK_TradingForeignExchangeExperienceID = jointAccountModel.DrpHaveExperienceTradingForeignExchange,
                    IsHavingAccount = jointAccountModel.HaveAccWithFqSecurities,
                    IsRegisterdWithEntity = jointAccountModel.RegisteredPerson,
                    IsRegisteredWithRegulator = jointAccountModel.RequiredToBeRegisteredWithRegulator,
                    IsEmployeeOfExchangeOrRegulator = jointAccountModel.EmployeeOfExchangeOrRegulatorOperator,
                    IsDeclaredBankruptcy = jointAccountModel.EverDeclaredBankruptcy,
                    FK_UserID = newUser.PK_UserID,
                    FK_OrganizationID = (int)organizationID
                };

                //Add details to IndividualAccountInformation table
                JointAccountInformation jointAccountInformation = new JointAccountInformation()
                {

                    //Primary Account Information
                    PrimaryAccountHolderTitle = jointAccountModel.PrimaryAccountHolderTitle,
                    PrimaryAccountHolderFirstName = jointAccountModel.PrimaryAccountHolderFirstName,
                    PrimaryAccountHolderMiddleName = jointAccountModel.PrimaryAccountHolderMiddleName,
                    PrimaryAccountHolderLastName = jointAccountModel.PrimaryAccountHolderLastName,
                    PrimaryAccountHolderBirthDate =
                                        new DateTime(jointAccountModel.PrimaryAccountHolderDobYear,
                                                    jointAccountModel.PrimaryAccountHolderDobMonth,
                                                    jointAccountModel.PrimaryAccountHolderDobDay),
                    PrimaryAccountHolderGender = jointAccountModel.PrimaryAccountHolderGender,
                    FK_PrimaryAccountHolderCitizenshipCountryID = jointAccountModel.PrimaryAccountHolderCitizenship,
                    FK_PrimaryAccountHolderIDTypeID = jointAccountModel.PrimaryAccountHolderIdInfo,
                    PrimaryAccountHolderIDNumber = jointAccountModel.PrimaryAccountHolderIdNumber,
                    FK_PrimaryAccountHolderResidenceCountryID = jointAccountModel.PrimaryAccountHolderResidenceCountry,

                    //Secondary Account Information
                    SecondaryAccountHolderTitle = jointAccountModel.SecondaryAccountHolderTitle,
                    SecondaryAccountHolderFirstName = jointAccountModel.SecondaryAccountHolderFirstName,
                    SecondaryAccountHolderMiddleName = jointAccountModel.SecondaryAccountHolderMiddleName,
                    SecondaryAccountHolderLastName = jointAccountModel.SecondaryAccountHolderLastName,
                    SecondaryAccountHolderBirthDate =
                                        new DateTime(jointAccountModel.SecondaryAccountHolderDobYear,
                                            jointAccountModel.SecondaryAccountHolderDobMonth,
                                            jointAccountModel.SecondaryAccountHolderDobDay),
                    SecondaryAccountHolderGender = jointAccountModel.SecondaryAccountHolderGender,
                    FK_SecondaryAccountHolderCitizenshipCountryID = jointAccountModel.SecondaryAccountHolderCitizenship,
                    FK_SecondaryAccountHolderIDTypeID = jointAccountModel.SecondaryAccountHolderIdInfo,
                    SecondaryAccountHolderIDNumber = jointAccountModel.SecondaryAccountHolderIdNumber,
                    FK_SecondaryAccountHolderResidenceCountryID = jointAccountModel.SecondaryAccountHolderResidenceCountry,

                    //Primary Account HolderContact Information
                    ResidentialAddress = jointAccountModel.PrimaryAccountHolderResidentialAddLine1 + "@" +
                                                                jointAccountModel.PrimaryAccountHoldeResidentialAddLine2,
                    ResidentialAddressCity = jointAccountModel.PrimaryAccountHoldeResidentialCity,
                    FK_ResidentialAddressCountryID = jointAccountModel.PrimaryAccountHoldeResidentialCountry,
                    ResidentialAddressPostalCode = jointAccountModel.PrimaryAccountHoldeResidentialPostalCode.ToString(),
                    MonthsAtCurrentAddress =
                        (jointAccountModel.PrimaryAccountHoldeYearsInCurrentAdd * 12) + jointAccountModel.PrimaryAccountHoldeMonthsInCurrentAdd,

                    PreviousAddress = jointAccountModel.PrimaryAccountHoldePreviousAddLine1 + "@" +
                                                                jointAccountModel.PrimaryAccountHoldePreviousAddLine2,
                    PreviousAddressCity = jointAccountModel.PrimaryAccountHoldePreviousCity,
                    FK_PreviousAddressCounrtyID = jointAccountModel.PrimaryAccountHoldePreviousCountry,
                    PreviousAddressPostalCode = jointAccountModel.PrimaryAccountHoldePreviousPostalCode,
                    TelephoneNumber = jointAccountModel.PrimaryAccountHoldeTelNumberCountryCode + "-" +
                                                            jointAccountModel.PrimaryAccountHoldeTelNumber,
                    MobileNumber = jointAccountModel.PrimaryAccountHoldeMobileNumberCountryCode + "-" +
                                                            jointAccountModel.PrimaryAccountHoldeMobileNumber
                };

                newIB.JointAccountInformations = new List<JointAccountInformation>() { jointAccountInformation };

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = jointAccountModel.BankName,
                    AccountNumber = jointAccountModel.AccountNumber,
                    BicNumberOrSwiftCode = jointAccountModel.BicOrSwiftCode,
                    BankingAddress = jointAccountModel.BankAddLine1 + "@" + jointAccountModel.BankAddLine2,
                    FK_ReceivingBankInformationID = jointAccountModel.ReceivingBankInfoId,
                    ReceivingBankInfo = jointAccountModel.ReceivingBankInfo,
                    City = jointAccountModel.BankCity,
                    FK_CountryID = jointAccountModel.BankCountry,
                    PostalCode = jointAccountModel.BankPostalCode.ToString()
                };

                newIB.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };

                //If Introducing Broker
                if (liveLead.FK_AccountID == Constants.K_ACCTCODE_IB)
                {
                    newIB.IsIntroducingBroker = true;
                    newIB.IsAssetManager = false;
                }

                //Add Data To Asset Manager
                if (liveLead.FK_AccountID == Constants.K_ACCTCODE_AM)
                {
                    newIB.IsAssetManager = true;
                    newIB.IsIntroducingBroker = true;
                    //AssetManager newAssetMng = new AssetManager()
                    //{
                    //    FK_AccountID = liveLead.FK_AccountID,
                    //    FK_AccountTypeID = liveLead.FK_AccountTypeID,
                    //    FK_TradingPlatformID = liveLead.FK_PlatformID,
                    //    FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID,
                    //    FK_WidenSpreadsID = liveLead.FK_WidenSpreadsID,
                    //    FK_CommissionIncrementID = liveLead.FK_CommissionIncrementID,
                    //    FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage,
                    //    UserEmail = liveLead.EmailAddress,
                    //    Password = liveLead.Password,
                    //    IsEnglishSpeaking = liveLead.IsEnglishSpeaking,

                    //    BankName = jointAccountModel.BankName,
                    //    AccountNumber = jointAccountModel.AccountNumber,
                    //    BicNumberOrSwiftCode = jointAccountModel.BicOrSwiftCode,
                    //    BankingAddress = jointAccountModel.BankAddLine1 + "@" + jointAccountModel.BankAddLine2,
                    //    FK_ReceivingBankInformationID = jointAccountModel.ReceivingBankInfoId,
                    //    ReceivingBankInfo = jointAccountModel.ReceivingBankInfo,
                    //    City = jointAccountModel.BankCity,
                    //    FK_CountryID = jointAccountModel.BankCountry,
                    //    PostalCode = jointAccountModel.BankPostalCode.ToString(),
                    //    EstimatedAnnualIncome = jointAccountModel.EstimatedAnnualIncome.ToString(),
                    //    LiquidAssets = jointAccountModel.LiquidAssets.ToString(),
                    //    NetWorthInEuros = jointAccountModel.NetWorthEuros.ToString(),
                    //    FK_TradingSecurityExperienceID = jointAccountModel.DrpHaveExperienceTradingSecurities,
                    //    FK_TradingOptionExperienceID = jointAccountModel.DrpHaveExperienceTradingOptions,
                    //    FK_TradingForeignExchangeExperienceID = jointAccountModel.DrpHaveExperienceTradingForeignExchange,
                    //    IsHavingAccount = jointAccountModel.HaveAccWithFqSecurities,
                    //    IsRegisterdWithEntity = jointAccountModel.RegisteredPerson,
                    //    IsRegisteredWithRegulator = jointAccountModel.RequiredToBeRegisteredWithRegulator,
                    //    IsEmployeeOfExchangeOrRegulator = jointAccountModel.EmployeeOfExchangeOrRegulatorOperator,
                    //    IsDeclaredBankruptcy = jointAccountModel.EverDeclaredBankruptcy
                    //};
                    ////Add Asset Manager To Database
                    //newIB.AssetManagers = new List<AssetManager>() { newAssetMng };
                }

                introducingBrokerBO.AddIntroducingBrokerDetails(newIB);

                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Create account number for partner user
                int pkClientAccID = AccountCreationController.CreateAccountNumberForPartnerUser(newIB);

                Session.Remove("PartnersRegisteredEmailAddress");
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }

        #endregion

        #region PARTNERS INDIVIDUAL ACCOUNT

        /// <summary>
        /// This action returns IndividualForm view for Partner Account
        /// </summary>
        /// <returns></returns>
        public ActionResult PartnerIndividualAccountComplete()
        {
            try
            {
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("PartnerAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                    return View("IndividualAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This action inserts individual account details for Partners in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartnerIndividualAccountComplete(IndividualAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IndividualAccountReviewModel individualAccountReviewModel = new IndividualAccountReviewModel()
                    {
                        Title = model.Title == "1" ? "Mr" : "Mrs",
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        DobMonth = model.DobMonth.ToString("D2"),
                        DobDay = model.DobDay.ToString("D2"),
                        DobYear = model.DobYear,
                        Gender = model.Gender ? "Male" : "Female",
                        Citizenship = countryBO.GetSelectedCountry(model.Citizenship),
                        IdInfo = idInfoTypeBO.GetSelectedIDInformation(model.IdInfo),
                        IdNumber = model.IdNumber,
                        ResidenceCountry = countryBO.GetSelectedCountry(model.ResidenceCountry),
                        ResidentialAddLine1 = model.ResidentialAddLine1,
                        ResidentialAddLine2 = model.ResidentialAddLine2,
                        ResidentialCity = model.ResidentialCity,
                        ResidentialCountry = countryBO.GetSelectedCountry(model.ResidentialCountry),
                        ResidentialPostalCode = model.ResidentialPostalCode,
                        YearsInCurrentAdd = model.YearsInCurrentAdd,
                        MonthsInCurrentAdd = model.MonthsInCurrentAdd,
                        PreviousAddLine1 = model.PreviousAddLine1,
                        PreviousAddLine2 = model.PreviousAddLine2,
                        PreviousCity = model.PreviousCity,
                        PreviousCountry = model.PreviousCountry != null ? countryBO.GetSelectedCountry((int)model.PreviousCountry) : null,
                        PreviousPostalCode = model.PreviousPostalCode,
                        TelNumberCountryCode = model.TelNumberCountryCode,
                        TelNumber = model.TelNumber,
                        MobileNumberCountryCode = model.MobileNumberCountryCode,
                        MobileNumber = model.MobileNumber,
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        IndividualModel = model,
                        IsLive = false
                    };
                    return View("IndividualAccountReview", individualAccountReviewModel);
                   
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                       "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View("IndividualAccount", model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function Will Insert Partners Individual Information
        /// </summary>
        /// <param name="model"></param>
        public void InsertPartnerIndividualInformation(IndividualAccountModel model)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["PartnersRegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);
                IntroducingBroker newIB = new IntroducingBroker();

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_PARTNER;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);

                //Adding widenspread and commission
                PartnerCommission partComm = new PartnerCommission();
                partComm.FK_WidenSpreadID = liveLead.FK_WidenSpreadsID;
                partComm.FK_CommissionID = liveLead.FK_CommissionIncrementID;
                partComm.WidenSpreadValue = liveLead.WidenSpreadValue;
                partComm.CommissionValue = liveLead.CommissionValue;
                partComm.FeeStructureName = "Initial Fee Structure";
                partComm.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                partComm.FK_UserID = newUser.PK_UserID;
                partComm.IsDefault = true;

                partCommBO.AddNewPartnerSpread(partComm);

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    BicNumberOrSwiftCode = model.BicOrSwiftCode,
                    BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2,
                    FK_ReceivingBankInformationID = model.ReceivingBankInfoId,
                    ReceivingBankInfo = model.ReceivingBankInfo,
                    City = model.BankCity,
                    FK_CountryID = model.BankCountry,
                    PostalCode = model.BankPostalCode.ToString()
                };

                newIB.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };
                newIB.FK_AccountID = liveLead.FK_AccountID;
                newIB.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newIB.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newIB.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newIB.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newIB.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                newIB.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                newIB.LiquidAssets = model.LiquidAssets.ToString();
                newIB.NetWorthInEuros = model.NetWorthEuros.ToString();
                newIB.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                newIB.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                newIB.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                newIB.IsHavingAccount = model.HaveAccWithFqSecurities;
                newIB.IsRegisterdWithEntity = model.RegisteredPerson;
                newIB.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                newIB.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                newIB.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                newIB.FK_UserID = newUser.PK_UserID;
                newIB.FK_OrganizationID = (int)organizationID;

                //If Introducing broker
                if (liveLead.FK_AccountID == Constants.K_ACCTCODE_IB)
                {
                    newIB.IsIntroducingBroker = true;
                    newIB.IsAssetManager = false;
                }
                //If Asset manager
                else if (liveLead.FK_AccountID == Constants.K_ACCTCODE_AM)
                {
                    newIB.IsIntroducingBroker = true;
                    newIB.IsAssetManager = true;
                }

                //Add to IntroducingBroker table
                introducingBrokerBO.AddIntroducingBrokerDetails(newIB);

                //If Asset manager
                //else if (liveLead.FK_AccountID == 5)
                //{
                //    newIB.FK_AccountID = liveLead.FK_AccountID;
                //    newIB.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                //    newIB.FK_TradingPlatformID = liveLead.FK_PlatformID;
                //    newIB.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                //    newIB.FK_WidenSpreadsID = liveLead.FK_WidenSpreadsID;
                //    newIB.FK_CommissionIncrementID = liveLead.FK_CommissionIncrementID;
                //    newIB.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                //    newIB.UserEmail = liveLead.EmailAddress;
                //    newIB.Password = liveLead.Password;
                //    newIB.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                //    //newIB.BankName = model.BankName;
                //    //newIB.AccountNumber = model.AccountNumber;
                //    //newIB.BicNumberOrSwiftCode = model.BicOrSwiftCode;
                //    //newIB.BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2;
                //    //newIB.FK_ReceivingBankInformationID = model.ReceivingBankInfoId;
                //    //newIB.ReceivingBankInfo = model.ReceivingBankInfo;
                //    //newIB.City = model.BankCity;
                //    //newIB.FK_CountryID = model.BankCountry;
                //    //newIB.PostalCode = model.BankPostalCode.ToString();
                //    newIB.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                //    newIB.LiquidAssets = model.LiquidAssets.ToString();
                //    newIB.NetWorthInEuros = model.NetWorthEuros.ToString();
                //    newIB.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                //    newIB.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                //    newIB.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                //    newIB.IsHavingAccount = model.HaveAccWithFqSecurities;
                //    newIB.IsRegisterdWithEntity = model.RegisteredPerson;
                //    newIB.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                //    newIB.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                //    newIB.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;

                //    //Add to IntroducingBroker table
                //    introducingBrokerBO.AddIntroducingBrokerDetails(newIB);

                //    AssetManager newAssetMng = new AssetManager();
                //    newAssetMng.FK_AccountID = liveLead.FK_AccountID;
                //    newAssetMng.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                //    newAssetMng.FK_TradingPlatformID = liveLead.FK_PlatformID;
                //    newAssetMng.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                //    newAssetMng.FK_WidenSpreadsID = liveLead.FK_WidenSpreadsID;
                //    newAssetMng.FK_CommissionIncrementID = liveLead.FK_CommissionIncrementID;
                //    newAssetMng.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                //    newAssetMng.UserEmail = liveLead.EmailAddress;
                //    newAssetMng.Password = liveLead.Password;
                //    newAssetMng.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                //    newAssetMng.BankName = model.BankName;
                //    newAssetMng.AccountNumber = model.AccountNumber;
                //    newAssetMng.BicNumberOrSwiftCode = model.BicOrSwiftCode;
                //    newAssetMng.BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2;
                //    newAssetMng.FK_ReceivingBankInformationID = model.ReceivingBankInfoId;
                //    newAssetMng.ReceivingBankInfo = model.ReceivingBankInfo;
                //    newAssetMng.City = model.BankCity;
                //    newAssetMng.FK_CountryID = model.BankCountry;
                //    newAssetMng.PostalCode = model.BankPostalCode;
                //    newAssetMng.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                //    newAssetMng.LiquidAssets = model.LiquidAssets.ToString();
                //    newAssetMng.NetWorthInEuros = model.NetWorthEuros.ToString();
                //    newAssetMng.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                //    newAssetMng.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                //    newAssetMng.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                //    newAssetMng.IsHavingAccount = model.HaveAccWithFqSecurities;
                //    newAssetMng.IsRegisterdWithEntity = model.RegisteredPerson;
                //    newAssetMng.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                //    newAssetMng.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                //    newAssetMng.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                //    newAssetMng.FK_IntroducingBrokerID = newIB.PK_IntroducingBrokerID;

                //    //Add to AssetManager table
                //    assetManagerBO.AddAssetManagerDetails(newAssetMng);
                //}

                //Delete corresponding live lead
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Add details to IndividualAccountInformation table
                IndividualAccountInformation individualAccount = new IndividualAccountInformation();
                individualAccount.Title = model.Title;
                individualAccount.FirstName = model.FirstName;
                individualAccount.MiddleName = model.MiddleName;
                individualAccount.LastName = model.LastName;
                individualAccount.BirthDate = new DateTime(model.DobYear, model.DobMonth, model.DobDay);
                individualAccount.Gender = model.Gender;
                individualAccount.FK_CitizenShipCountryID = model.Citizenship;
                individualAccount.FK_IDInformationID = model.IdInfo;
                individualAccount.IDNumber = model.IdNumber;
                individualAccount.FK_ResidenceCountryID = model.ResidenceCountry;
                individualAccount.ResidentialAddress = model.ResidentialAddLine1 + "@" + model.ResidentialAddLine2;
                individualAccount.ResidentialAddressCity = model.ResidentialCity;
                individualAccount.FK_ResidentialAddressCountryID = model.ResidentialCountry;
                individualAccount.ResidentialAddressPostalCode = model.ResidentialPostalCode.ToString();
                individualAccount.MonthsAtCurrentAddress = (model.YearsInCurrentAdd * 12) + model.MonthsInCurrentAdd;
                individualAccount.PreviousAddress = model.PreviousAddLine1 + "@" + model.PreviousAddLine2;
                individualAccount.PreviousAddressCity = model.PreviousCity;
                individualAccount.FK_PreviousAddressCounrtyID = model.PreviousCountry;
                individualAccount.PreviousAddressPostalCode = model.PreviousPostalCode;
                individualAccount.TelephoneNumber = model.TelNumberCountryCode + "-" + model.TelNumber;
                individualAccount.MobileNumber = model.MobileNumberCountryCode + "-" + model.MobileNumber;
                individualAccount.FK_IntroducingBrokerID = newIB.PK_IntroducingBrokerID;

                individualAccountInformationBO.AddIndividualAccDetailsForNewClient(individualAccount);

                //Create account number for partner user
                int pkClientAccID = AccountCreationController.CreateAccountNumberForPartnerUser(newIB);

                Session.Remove("PartnersRegisteredEmailAddress");
            }
            catch (Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }

        #endregion

        #region PARTNERS CORPORATE ACCOUNT

        /// <summary>
        /// This action returns CorporateForm view for Partner account
        /// </summary>
        /// <returns></returns>
        public ActionResult PartnerCorporateAccountComplete()
        {
            try
            {
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    //Session is not yet registered redirect to initial
                    return RedirectToAction("PartnerAccountInitial");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID != null)
                {
                    ViewData["CompanyTypes"] = new SelectList(companyTypeValuesBO.GetCompanyType(), "PK_CompanyTypeID",
                                                              "CompanyType");
                    ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                    ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                    ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                   "PK_RecievingBankID", "RecievingBankName");
                    ViewData["EstimatedAnnualIncome"] =
                        new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                       "AnnualIncome");
                    ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                              "PK_LiquidAssetID", "LiquidAsset");
                    ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                               "PK_WorthID", "Worth");
                    ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(), "PK_ExperienceID",
                                                            "Experience");
                    ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                    ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                    ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                    ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                    return View("CorporateAccount");
                }
                else
                {
                    return View("ErrorMessage");
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This action inserts corporate account details for Partner in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartnerCorporateAccountComplete(CorporateAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CorporateAccountReviewModel individualAccountReviewModel = new CorporateAccountReviewModel()
                    {
                        CompanyName = model.CompanyName,
                        CompanyType = companyTypeValuesBO.GetSelectedCompany(model.CompanyType),
                        CompanyAddLine1 = model.CompanyAddLine1,
                        CompanyAddLine2 = model.CompanyAddLine2,
                        CompanyCity = model.CompanyCity,
                        CompanyCountry = countryBO.GetSelectedCountry(model.CompanyCountry),
                        CompanyPostalCode = model.CompanyPostalCode,

                        AuthOfficerTitle = model.AuthOfficerTitle == "1" ? "Mr" : "Mrs",
                        AuthOfficerFirstName = model.AuthOfficerFirstName,
                        AuthOfficerMiddleName = model.AuthOfficerMiddleName,
                        AuthOfficerLastName = model.AuthOfficerLastName,
                        AuthOfficerDobMonth = model.AuthOfficerDobMonth.ToString("D2"),
                        AuthOfficerDobDay = model.AuthOfficerDobDay.ToString("D2"),
                        AuthOfficerDobYear = model.AuthOfficerDobYear,
                        AuthOfficerGender = model.AuthOfficerGender ? "Male" : "Female",
                        AuthOfficerCitizenship = countryBO.GetSelectedCountry(model.AuthOfficerCitizenship),
                        AuthOfficerIdInfo = idInfoTypeBO.GetSelectedIDInformation(model.AuthOfficerIdInfo),
                        AuthOfficerIdNumber = model.AuthOfficerIdNumber,

                        AuthOfficerResidenceCountry = countryBO.GetSelectedCountry(model.AuthOfficerResidenceCountry),
                        AuthOfficerAddLine1 = model.AuthOfficerAddLine1,
                        AuthOfficerAddLine2 = model.AuthOfficerAddLine2,
                        AuthOfficerCity = model.AuthOfficerCity,
                        AuthOfficerCountry = countryBO.GetSelectedCountry(model.AuthOfficerCountry),
                        AuthOfficerPostalCode = model.AuthOfficerPostalCode,
                        AuthOfficerTelNumberCountryCode = model.AuthOfficerTelNumberCountryCode,
                        AuthOfficerTelNumber = model.AuthOfficerTelNumber,
                        AuthOfficerMobileNumberCountryCode = model.AuthOfficerMobileNumberCountryCode,
                        AuthOfficerMobileNumber = model.AuthOfficerMobileNumber,
                        
                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        BicOrSwiftCode = model.BicOrSwiftCode,
                        BankAddLine1 = model.BankAddLine1,
                        BankAddLine2 = model.BankAddLine2,
                        ReceivingBankInfoId = receivingBankInfoBO.GetSelectedRecievingBankInfo(model.ReceivingBankInfoId),
                        ReceivingBankInfo = model.ReceivingBankInfo,
                        BankCity = model.BankCity,
                        BankCountry = countryBO.GetSelectedCountry(model.BankCountry),
                        BankPostalCode = model.BankPostalCode,
                        EstimatedAnnualIncome = annualIncomeValuesBO.GetSelectedAnnualIncome(model.EstimatedAnnualIncome),
                        LiquidAssets = liquidAssetsValuesBO.GetSelectedLiquidAsset(model.LiquidAssets),
                        NetWorthEuros = netWorthEurosValuesBO.GetSelectedNetWorthEuros(model.NetWorthEuros),
                        DrpHaveExperienceTradingSecurities = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingSecurities),
                        DrpHaveExperienceTradingOptions = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingOptions),
                        DrpHaveExperienceTradingForeignExchange = tradingExpValuesBO.GetSelectedTradingExperience(model.DrpHaveExperienceTradingForeignExchange),
                        HaveAccWithFqSecurities = model.HaveAccWithFqSecurities ? "Yes" : "No",
                        RequiredToBeRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator ? "Yes" : "No",
                        EverDeclaredBankruptcy = model.EverDeclaredBankruptcy ? "Yes" : "No",
                        RegisteredPerson = model.RegisteredPerson ? "Yes" : "No",
                        EmployeeOfExchangeOrRegulatorOperator = model.EmployeeOfExchangeOrRegulatorOperator ? "Yes" : "No",
                        CorporateModel= model,
                        IsLive = false
                    };
                    return View("CorporateAccountReview", individualAccountReviewModel);
                }
                else
                {
                    var organizationID = SessionManagement.OrganizationID;

                    if (organizationID != null)
                    {
                        ViewData["CompanyTypes"] = new SelectList(companyTypeValuesBO.GetCompanyType(),
                                                                  "PK_CompanyTypeID", "CompanyType");
                        ViewData["Country"] = new SelectList(countryBO.GetCountries(), "PK_CountryID", "CountryName");
                        ViewData["IdInfo"] = new SelectList(idInfoTypeBO.GetIdInfoType((int)organizationID), "PK_IDTypeID", "IDValue");
                        ViewData["ReceivingBankInfo"] = new SelectList(receivingBankInfoBO.GetReceivingBankInfo((int)organizationID),
                                                                       "PK_RecievingBankID", "RecievingBankName");
                        ViewData["EstimatedAnnualIncome"] =
                            new SelectList(annualIncomeValuesBO.GetEstimatedAnnualIncomeValues(), "PK_AnnualIncomeID",
                                           "AnnualIncome");
                        ViewData["LiquidAssets"] = new SelectList(liquidAssetsValuesBO.GetLiquidAssetsValues(),
                                                                  "PK_LiquidAssetID", "LiquidAsset");
                        ViewData["NetWorthEuros"] = new SelectList(netWorthEurosValuesBO.GetNetWorthEurosValues(),
                                                                   "PK_WorthID", "Worth");
                        ViewData["TradingExp"] = new SelectList(tradingExpValuesBO.GetTradingExpValues(),
                                                                "PK_ExperienceID", "Experience");
                        ViewData["Titles"] = new SelectList(ExtensionUtility.GetTitle(), "ID", "Value");
                        ViewData["Months"] = new SelectList(ExtensionUtility.GetMonths(), "ID", "Name");
                        ViewData["Days"] = new SelectList(ExtensionUtility.GetDays(), "ID", "Name");
                        ViewData["Years"] = new SelectList(ExtensionUtility.GetYears(), "ID", "Name");

                        return View("CorporateAccount", model);
                    }
                    else
                    {
                        return View("ErrorMessage");
                    }
                }
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return View("ErrorMessage");
            }
        }

        /// <summary>
        /// This Function will Insert Partners Corporate Information
        /// </summary>
        /// <param name="model"></param>
        public void InsertPartnerCorporateInformation(CorporateAccountModel model)
        {
            try
            {
                //if the registered email doesn't exit
                if (Session["PartnersRegisteredEmailAddress"] == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                var organizationID = SessionManagement.OrganizationID;

                if (organizationID == null)
                {
                    throw new Exception("Session Expired Or Invalid Attempt");
                }

                string userEmail = Session["PartnersRegisteredEmailAddress"].ToString();
                var liveLead = liveLeadBO.GetLiveLeadDetailsForUser(userEmail);
                IntroducingBroker newIB = new IntroducingBroker();

                //Add details to Users table
                User newUser = new User();
                newUser.UserEmailID = liveLead.EmailAddress;
                newUser.Password = liveLead.Password;
                newUser.FK_UserTypeID = Constants.K_BROKER_PARTNER;
                newUser.FK_OrganizationID = (int)organizationID;

                userBO.AddNewUser(newUser);

                //Adding widenspread and commission
                PartnerCommission partComm = new PartnerCommission();
                partComm.FK_WidenSpreadID = liveLead.FK_WidenSpreadsID;
                partComm.FK_CommissionID = liveLead.FK_CommissionIncrementID;
                partComm.WidenSpreadValue = liveLead.WidenSpreadValue;
                partComm.CommissionValue = liveLead.CommissionValue;
                partComm.FeeStructureName = "Initial Fee Structure";
                partComm.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                partComm.FK_UserID = newUser.PK_UserID;
                partComm.IsDefault = true;

                partCommBO.AddNewPartnerSpread(partComm);

                //Adding Bank Information
                var bankAccountInformation = new BankAccountInformation()
                {
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    BicNumberOrSwiftCode = model.BicOrSwiftCode,
                    BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2,
                    FK_ReceivingBankInformationID = model.ReceivingBankInfoId,
                    ReceivingBankInfo = model.ReceivingBankInfo,
                    City = model.BankCity,
                    FK_CountryID = model.BankCountry,
                    PostalCode = model.BankPostalCode.ToString()
                };

                newIB.BankAccountInformations = new List<BankAccountInformation>() { bankAccountInformation };
                newIB.FK_AccountID = liveLead.FK_AccountID;
                newIB.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                newIB.FK_TradingPlatformID = liveLead.FK_PlatformID;
                newIB.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                newIB.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                newIB.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                newIB.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                newIB.LiquidAssets = model.LiquidAssets.ToString();
                newIB.NetWorthInEuros = model.NetWorthEuros.ToString();
                newIB.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                newIB.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                newIB.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                newIB.IsHavingAccount = model.HaveAccWithFqSecurities;
                newIB.IsRegisterdWithEntity = model.RegisteredPerson;
                newIB.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                newIB.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                newIB.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                newIB.FK_UserID = newUser.PK_UserID;
                newIB.FK_OrganizationID = (int)organizationID;

                //If Introducing broker
                if (liveLead.FK_AccountID == Constants.K_ACCTCODE_IB)
                {
                    newIB.IsAssetManager = false;
                    newIB.IsIntroducingBroker = true;
                }
                //If Asset manager
                else if (liveLead.FK_AccountID == Constants.K_ACCTCODE_AM)
                {
                    newIB.IsIntroducingBroker = true;
                    newIB.IsAssetManager = true;

                    //newIB.FK_AccountID = liveLead.FK_AccountID;
                    //newIB.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                    //newIB.FK_TradingPlatformID = liveLead.FK_PlatformID;
                    //newIB.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                    //newIB.FK_WidenSpreadsID = liveLead.FK_WidenSpreadsID;
                    //newIB.FK_CommissionIncrementID = liveLead.FK_CommissionIncrementID;
                    //newIB.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                    //newIB.UserEmail = liveLead.EmailAddress;
                    //newIB.Password = liveLead.Password;
                    //newIB.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                    ////newIB.BankName = model.BankName;
                    ////newIB.AccountNumber = model.AccountNumber;
                    ////newIB.BicNumberOrSwiftCode = model.BicOrSwiftCode;
                    ////newIB.BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2;
                    ////newIB.FK_ReceivingBankInformationID = model.ReceivingBankInfoId;
                    ////newIB.ReceivingBankInfo = model.ReceivingBankInfo;
                    ////newIB.City = model.BankCity;
                    ////newIB.FK_CountryID = model.BankCountry;
                    ////newIB.PostalCode = model.BankPostalCode.ToString();
                    //newIB.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                    //newIB.LiquidAssets = model.LiquidAssets.ToString();
                    //newIB.NetWorthInEuros = model.NetWorthEuros.ToString();
                    //newIB.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                    //newIB.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                    //newIB.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                    //newIB.IsHavingAccount = model.HaveAccWithFqSecurities;
                    //newIB.IsRegisterdWithEntity = model.RegisteredPerson;
                    //newIB.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                    //newIB.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                    //newIB.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;

                    ////Add to IntroducingBroker table
                    //introducingBrokerBO.AddIntroducingBrokerDetails(newIB);

                    //AssetManager newAssetMng = new AssetManager();
                    //newAssetMng.FK_AccountID = liveLead.FK_AccountID;
                    //newAssetMng.FK_AccountTypeID = liveLead.FK_AccountTypeID;
                    //newAssetMng.FK_TradingPlatformID = liveLead.FK_PlatformID;
                    //newAssetMng.FK_AccountCurrencyID = liveLead.FK_AccountCurrencyID;
                    //newAssetMng.FK_WidenSpreadsID = liveLead.FK_WidenSpreadsID;
                    //newAssetMng.FK_CommissionIncrementID = liveLead.FK_CommissionIncrementID;
                    //newAssetMng.FK_PreferredLanguage = liveLead.FK_PreferedCommunicationLanguage;
                    //newAssetMng.UserEmail = liveLead.EmailAddress;
                    //newAssetMng.Password = liveLead.Password;
                    //newAssetMng.IsEnglishSpeaking = liveLead.IsEnglishSpeaking;
                    //newAssetMng.BankName = model.BankName;
                    //newAssetMng.AccountNumber = model.AccountNumber;
                    //newAssetMng.BicNumberOrSwiftCode = model.BicOrSwiftCode;
                    //newAssetMng.BankingAddress = model.BankAddLine1 + "@" + model.BankAddLine2;
                    //newAssetMng.FK_ReceivingBankInformationID = model.ReceivingBankInfoId;
                    //newAssetMng.ReceivingBankInfo = model.ReceivingBankInfo;
                    //newAssetMng.City = model.BankCity;
                    //newAssetMng.FK_CountryID = model.BankCountry;
                    //newAssetMng.PostalCode = model.BankPostalCode.ToString();
                    //newAssetMng.EstimatedAnnualIncome = model.EstimatedAnnualIncome.ToString();
                    //newAssetMng.LiquidAssets = model.LiquidAssets.ToString();
                    //newAssetMng.NetWorthInEuros = model.NetWorthEuros.ToString();
                    //newAssetMng.FK_TradingSecurityExperienceID = model.DrpHaveExperienceTradingSecurities;
                    //newAssetMng.FK_TradingOptionExperienceID = model.DrpHaveExperienceTradingOptions;
                    //newAssetMng.FK_TradingForeignExchangeExperienceID = model.DrpHaveExperienceTradingForeignExchange;
                    //newAssetMng.IsHavingAccount = model.HaveAccWithFqSecurities;
                    //newAssetMng.IsRegisterdWithEntity = model.RegisteredPerson;
                    //newAssetMng.IsRegisteredWithRegulator = model.RequiredToBeRegisteredWithRegulator;
                    //newAssetMng.IsEmployeeOfExchangeOrRegulator = model.EmployeeOfExchangeOrRegulatorOperator;
                    //newAssetMng.IsDeclaredBankruptcy = model.EverDeclaredBankruptcy;
                    //newAssetMng.FK_IntroducingBrokerID = newIB.PK_IntroducingBrokerID;

                    ////Add to AssetManager table
                    //assetManagerBO.AddAssetManagerDetails(newAssetMng);
                }

                //Add to IntroducingBroker table
                introducingBrokerBO.AddIntroducingBrokerDetails(newIB);

                //Delete corresponding live lead after insertion in Client table
                liveLeadBO.DeleteLiveLead(liveLead.EmailAddress);

                //Add details to CorporateAccountInformation table
                CorporateAccountInformation corporateAcc = new CorporateAccountInformation();
                corporateAcc.CompanyName = model.CompanyName;
                corporateAcc.FK_CompanyTypeID = model.CompanyType;
                corporateAcc.CompanyAddress = model.CompanyAddLine1 + "@" + model.CompanyAddLine2;
                corporateAcc.City = model.CompanyCity;
                corporateAcc.FK_CompanyCountryID = model.CompanyCountry;
                corporateAcc.CompanyPostalCode = model.CompanyPostalCode.ToString();
                corporateAcc.AuthOfficerTitle = model.AuthOfficerTitle;
                corporateAcc.AuthOfficerFirstName = model.AuthOfficerFirstName;
                corporateAcc.AuthOfficerMiddleName = model.AuthOfficerMiddleName;
                corporateAcc.AuthOfficerLastName = model.AuthOfficerLastName;
                corporateAcc.AuthOfficerBirthDate = new DateTime(model.AuthOfficerDobYear, model.AuthOfficerDobMonth, model.AuthOfficerDobDay);
                corporateAcc.AuthOfficerGender = model.AuthOfficerGender;
                corporateAcc.FK_AuthOfficerCitizenshipCountryID = model.AuthOfficerCitizenship;
                corporateAcc.FK_AuthOfficerInformationTypeID = model.AuthOfficerIdInfo;
                corporateAcc.AuthOfficerIDNumber = model.AuthOfficerIdNumber;
                corporateAcc.FK_AuthOfficerResidenceCountryID = model.AuthOfficerResidenceCountry;
                corporateAcc.AuthOfficerAddress = model.AuthOfficerAddLine1 + "@" + model.AuthOfficerAddLine2;
                corporateAcc.AuthOfficerCity = model.AuthOfficerCity;
                corporateAcc.FK_AuthOfficerCountryID = model.AuthOfficerCountry;
                corporateAcc.AuthOfficerPostalCode = model.AuthOfficerPostalCode.ToString();
                corporateAcc.AuthOfficerTelephoneNumber = model.AuthOfficerTelNumberCountryCode + "-" + model.AuthOfficerTelNumber;
                corporateAcc.AuthOfficerMobileNumber = model.AuthOfficerMobileNumberCountryCode + "-" + model.AuthOfficerMobileNumber;
                corporateAcc.FK_IntroducingBrokerID = newIB.PK_IntroducingBrokerID;

                corporateAccountInformationBO.AddCorporateAccDetailsForNewClient(corporateAcc);

                //Create account number for partner user
                int pkClientAccID = AccountCreationController.CreateAccountNumberForPartnerUser(newIB);

                Session.Remove("PartnersRegisteredEmailAddress");                
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                throw;
            }
        }


        #endregion

        #region INDVIDUAL COMMON

        /// <summary>
        /// Model Will get submitted from review to data.
        /// </summary>
        /// <param name="individualAccountReviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IndividualAccountReview(IndividualAccountReviewModel individualAccountReviewModel)
        {
            var model = individualAccountReviewModel.IndividualModel;
            try
            {
                if (individualAccountReviewModel.IsLive)
                {
                    InsertLiveIndividualInformation(model);
                }
                else
                {
                    InsertPartnerIndividualInformation(model);
                }

                return View("RegistartionSuccess");
            }
            catch(Exception exceptionMessage)
            {
                CurrentDeskLog.Error(exceptionMessage.Message, exceptionMessage);
                return RedirectToAction("Index", "Error", new ErrorModel() { ErrorMessage = "Error in Registration for Individual Account", DisplayMessage = exceptionMessage.Message });
            }
        }

        #endregion

        #region JOINT COMMON

        /// <summary>
        /// Model Will get submitted from review to data.
        /// </summary>
        /// <param name="individualAccountReviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JointAccountReview(JointAccountReviewModel jointAccountReviewModel)
        {
            var model = jointAccountReviewModel.JointModel;
            try
            {
                if (jointAccountReviewModel.IsLive)
                {
                    InsertLiveJointInformation(model);
                }
                else
                {
                    InsertPartnerJointInformation(model);
                }

                return View("RegistartionSuccess");
            }
            catch (Exception exceptionMessage)
            {
                CurrentDeskLog.Error(exceptionMessage.Message, exceptionMessage);
                return RedirectToAction("Index", "Error", new ErrorModel() { ErrorMessage = "Error in Registration for Joint Account", DisplayMessage = exceptionMessage.Message });
            }
        }

        #endregion

        #region CORPORATE COMMON

        /// <summary>
        /// Model Will get submitted from review to data.
        /// </summary>
        /// <param name="individualAccountReviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CorporateAccountReview(CorporateAccountReviewModel corporateAccountReviewModel)
        {
            var model = corporateAccountReviewModel.CorporateModel;
            try
            {
                if (corporateAccountReviewModel.IsLive)
                {
                    InsertLiveCorporateInformation(model);
                }
                else
                {
                    InsertPartnerCorporateInformation(model);
                }

                return View("RegistartionSuccess");
            }
            catch (Exception exceptionMessage)
            {
                CurrentDeskLog.Error(exceptionMessage.Message, exceptionMessage);
                return RedirectToAction("Index", "Error", new ErrorModel() { ErrorMessage = "Error in Registration for Corporate Account", DisplayMessage = exceptionMessage.Message });
            }
        }

        #endregion

        #region TRUST COMMON

        /// <summary>
        /// Model Will get submitted from review to data.
        /// </summary>
        /// <param name="individualAccountReviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TrustAccountReview(TrustAccountReviewModel trustAccountReviewModel)
        {
            var model = trustAccountReviewModel.TrustModel;
            try
            {
                if (trustAccountReviewModel.IsLive)
                {
                    InsertLiveTrustInformation(model);
                }
                else
                {
                }

                return View("RegistartionSuccess");
            }
            catch (Exception exceptionMessage)
            {
                CurrentDeskLog.Error(exceptionMessage.Message, exceptionMessage);
                return RedirectToAction("Index", "Error", new ErrorModel() { ErrorMessage = "Error in Registration for Trust Account", DisplayMessage = exceptionMessage.Message });
            }
        }

        #endregion

        #region EMAIL DUPLICATION CHECK
        
        /// <summary>
        /// This action returns false if emailID already exists in Users table
        /// </summary>
        /// <param name="UserEmail">UserEmail</param>
        /// <returns></returns>
        public ActionResult CheckIfDuplicateEmail(string UserEmail)
        {
            try
            {
                //Check if email present in Users or LiveLead table
                return Json(!(userBO.CheckIfEmailExistsInUser(UserEmail, (int)SessionManagement.OrganizationID) || liveLeadBO.CheckIfEmailExistsInLiveLead(UserEmail, (int)SessionManagement.OrganizationID)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// This action returns false if emailID already exists in DemoLead table
        /// </summary>
        /// <param name="EmailAddress">EmailAddress</param>
        /// <returns></returns>
        public ActionResult CheckIfDuplicateDemoAccountEmail(string EmailAddress)
        {
            try
            {
                //Check if email present in DemoLead table
                return Json(!demoLeadBO.CheckIfEmailExistsInDemoLead(EmailAddress, (int)SessionManagement.OrganizationID), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                CurrentDeskLog.Error(ex.Message, ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region TRADING PLATFORM ACCOUNT CREATION COMMON

        /// <summary>
        /// This method creates new trading platform account depending
        /// on trading platform id
        /// </summary>
        /// <param name="newUser">newUser</param>
        /// <param name="liveLead">liveLead</param>
        /// <param name="accType">accType</param>
        public void CreateTradingPlatformAccount(int pkClientAccID, int? fkPlatformID, User newUser, LiveLead liveLead, LoginAccountType accType)
        {
            //If Trading account chosen then create account in platform
            if (liveLead.FK_AccountID == Constants.K_TRADING_ACCOUNT)
            {
                switch (tradingPlatformBO.GetTradingPlatformLookUpID((int)liveLead.FK_PlatformID))
                {
                    case Constants.K_META_TRADER_ID:
                        AccountCreationController.CreateMetaTraderAccountForUser(pkClientAccID, fkPlatformID, newUser, accType);
                        break;
                }
            }
        }

        /// <summary>
        /// This method creates new trading platform account for AM depending
        /// on trading platform id
        /// </summary>
        /// <param name="newUser">newUser</param>
        /// <param name="liveLead">liveLead</param>
        /// <param name="accType">accType</param>
        public void CreateAssetManagerTradingPlatformAccount(int pkClientAccID, int? fkPlatformID, User newUser, LiveLead liveLead, LoginAccountType accType)
        {
            //If Trading account chosen then create account in platform
            if (liveLead.FK_AccountID == Constants.K_ACCTCODE_AM)
            {
                switch (liveLead.FK_PlatformID)
                {
                    case Constants.K_AM_META_TRADER:
                        AccountCreationController.CreateMetaTraderAccountForUser(pkClientAccID, fkPlatformID, newUser, accType);
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// This Function will get you signup Images
        /// </summary>
        /// <returns>File With Organization ID</returns>
        public ActionResult GetSignUpImage()
        {
            //return File("/Images/logo.png", "image/png");

            //if (SessionManagement.OrganizationID != null)
            //{
            //    var fileName = @"/Images/logosignup" + SessionManagement.OrganizationID + ".png";
            //    return File(fileName, "image/png");
            //}
            //else
            //{
            //    return File("/Images/logo.png", "image/png");
            //}

            var fileName = SessionManagement.OrganizationID != null ? @"/Images/logosignup" + SessionManagement.OrganizationID + ".png" : @"/Images/logo.png";
            return File(fileName, "image/png");
        }

        #endregion
    }
}
