


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;
using CurrentDesk.Models;


namespace CurrentDesk.DAL
{

public partial class CurrentDeskClientsEntities : ObjectContext
{
    public const string ConnectionString = "name=CurrentDeskClientsEntities";
    public const string ContainerName = "CurrentDeskClientsEntities";

    #region Constructors

    public CurrentDeskClientsEntities()
        : base(ConnectionString, ContainerName)
    {

        this.ContextOptions.LazyLoadingEnabled = true;

    }

    public CurrentDeskClientsEntities(string connectionString)
        : base(connectionString, ContainerName)
    {

        this.ContextOptions.LazyLoadingEnabled = true;

    }

    public CurrentDeskClientsEntities(EntityConnection connection)
        : base(connection, ContainerName)
    {

        this.ContextOptions.LazyLoadingEnabled = true;

    }

    #endregion

    #region ObjectSet Properties
    

    public ObjectSet<AccountCurrency> AccountCurrencies
    {
        get { return _accountCurrencies  ?? (_accountCurrencies = CreateObjectSet<AccountCurrency>("AccountCurrencies")); }
    }
    private ObjectSet<AccountCurrency> _accountCurrencies;


    public ObjectSet<AccountType> AccountTypes
    {
        get { return _accountTypes  ?? (_accountTypes = CreateObjectSet<AccountType>("AccountTypes")); }
    }
    private ObjectSet<AccountType> _accountTypes;


    public ObjectSet<AssetManager> AssetManagers
    {
        get { return _assetManagers  ?? (_assetManagers = CreateObjectSet<AssetManager>("AssetManagers")); }
    }
    private ObjectSet<AssetManager> _assetManagers;


    public ObjectSet<CorporateAccountInformation> CorporateAccountInformations
    {
        get { return _corporateAccountInformations  ?? (_corporateAccountInformations = CreateObjectSet<CorporateAccountInformation>("CorporateAccountInformations")); }
    }
    private ObjectSet<CorporateAccountInformation> _corporateAccountInformations;


    public ObjectSet<Employee> Employees
    {
        get { return _employees  ?? (_employees = CreateObjectSet<Employee>("Employees")); }
    }
    private ObjectSet<Employee> _employees;


    public ObjectSet<IndividualAccountInformation> IndividualAccountInformations
    {
        get { return _individualAccountInformations  ?? (_individualAccountInformations = CreateObjectSet<IndividualAccountInformation>("IndividualAccountInformations")); }
    }
    private ObjectSet<IndividualAccountInformation> _individualAccountInformations;


    public ObjectSet<IntroducingBroker> IntroducingBrokers
    {
        get { return _introducingBrokers  ?? (_introducingBrokers = CreateObjectSet<IntroducingBroker>("IntroducingBrokers")); }
    }
    private ObjectSet<IntroducingBroker> _introducingBrokers;


    public ObjectSet<JointAccountInformation> JointAccountInformations
    {
        get { return _jointAccountInformations  ?? (_jointAccountInformations = CreateObjectSet<JointAccountInformation>("JointAccountInformations")); }
    }
    private ObjectSet<JointAccountInformation> _jointAccountInformations;


    public ObjectSet<L_AccountCode> L_AccountCode
    {
        get { return _l_AccountCode  ?? (_l_AccountCode = CreateObjectSet<L_AccountCode>("L_AccountCode")); }
    }
    private ObjectSet<L_AccountCode> _l_AccountCode;


    public ObjectSet<L_AccountFormType> L_AccountFormType
    {
        get { return _l_AccountFormType  ?? (_l_AccountFormType = CreateObjectSet<L_AccountFormType>("L_AccountFormType")); }
    }
    private ObjectSet<L_AccountFormType> _l_AccountFormType;


    public ObjectSet<L_AccountTypeValue> L_AccountTypeValue
    {
        get { return _l_AccountTypeValue  ?? (_l_AccountTypeValue = CreateObjectSet<L_AccountTypeValue>("L_AccountTypeValue")); }
    }
    private ObjectSet<L_AccountTypeValue> _l_AccountTypeValue;


    public ObjectSet<L_AnnualIncomeValue> L_AnnualIncomeValue
    {
        get { return _l_AnnualIncomeValue  ?? (_l_AnnualIncomeValue = CreateObjectSet<L_AnnualIncomeValue>("L_AnnualIncomeValue")); }
    }
    private ObjectSet<L_AnnualIncomeValue> _l_AnnualIncomeValue;


    public ObjectSet<L_CommissionIncrementValue> L_CommissionIncrementValue
    {
        get { return _l_CommissionIncrementValue  ?? (_l_CommissionIncrementValue = CreateObjectSet<L_CommissionIncrementValue>("L_CommissionIncrementValue")); }
    }
    private ObjectSet<L_CommissionIncrementValue> _l_CommissionIncrementValue;


    public ObjectSet<L_CompanyTypeValue> L_CompanyTypeValue
    {
        get { return _l_CompanyTypeValue  ?? (_l_CompanyTypeValue = CreateObjectSet<L_CompanyTypeValue>("L_CompanyTypeValue")); }
    }
    private ObjectSet<L_CompanyTypeValue> _l_CompanyTypeValue;


    public ObjectSet<L_Country> L_Country
    {
        get { return _l_Country  ?? (_l_Country = CreateObjectSet<L_Country>("L_Country")); }
    }
    private ObjectSet<L_Country> _l_Country;


    public ObjectSet<L_CurrencyValue> L_CurrencyValue
    {
        get { return _l_CurrencyValue  ?? (_l_CurrencyValue = CreateObjectSet<L_CurrencyValue>("L_CurrencyValue")); }
    }
    private ObjectSet<L_CurrencyValue> _l_CurrencyValue;


    public ObjectSet<L_IDInformationType> L_IDInformationType
    {
        get { return _l_IDInformationType  ?? (_l_IDInformationType = CreateObjectSet<L_IDInformationType>("L_IDInformationType")); }
    }
    private ObjectSet<L_IDInformationType> _l_IDInformationType;


    public ObjectSet<L_InitialInvestment> L_InitialInvestment
    {
        get { return _l_InitialInvestment  ?? (_l_InitialInvestment = CreateObjectSet<L_InitialInvestment>("L_InitialInvestment")); }
    }
    private ObjectSet<L_InitialInvestment> _l_InitialInvestment;


    public ObjectSet<L_Languages> L_Languages
    {
        get { return _l_Languages  ?? (_l_Languages = CreateObjectSet<L_Languages>("L_Languages")); }
    }
    private ObjectSet<L_Languages> _l_Languages;


    public ObjectSet<L_LiquidAssetsValue> L_LiquidAssetsValue
    {
        get { return _l_LiquidAssetsValue  ?? (_l_LiquidAssetsValue = CreateObjectSet<L_LiquidAssetsValue>("L_LiquidAssetsValue")); }
    }
    private ObjectSet<L_LiquidAssetsValue> _l_LiquidAssetsValue;


    public ObjectSet<L_NetWorthEuros> L_NetWorthEuros
    {
        get { return _l_NetWorthEuros  ?? (_l_NetWorthEuros = CreateObjectSet<L_NetWorthEuros>("L_NetWorthEuros")); }
    }
    private ObjectSet<L_NetWorthEuros> _l_NetWorthEuros;


    public ObjectSet<L_RecievingBank> L_RecievingBank
    {
        get { return _l_RecievingBank  ?? (_l_RecievingBank = CreateObjectSet<L_RecievingBank>("L_RecievingBank")); }
    }
    private ObjectSet<L_RecievingBank> _l_RecievingBank;


    public ObjectSet<L_TicketSize> L_TicketSize
    {
        get { return _l_TicketSize  ?? (_l_TicketSize = CreateObjectSet<L_TicketSize>("L_TicketSize")); }
    }
    private ObjectSet<L_TicketSize> _l_TicketSize;


    public ObjectSet<L_TradingExperience> L_TradingExperience
    {
        get { return _l_TradingExperience  ?? (_l_TradingExperience = CreateObjectSet<L_TradingExperience>("L_TradingExperience")); }
    }
    private ObjectSet<L_TradingExperience> _l_TradingExperience;


    public ObjectSet<L_TradingPlatformValues> L_TradingPlatformValues
    {
        get { return _l_TradingPlatformValues  ?? (_l_TradingPlatformValues = CreateObjectSet<L_TradingPlatformValues>("L_TradingPlatformValues")); }
    }
    private ObjectSet<L_TradingPlatformValues> _l_TradingPlatformValues;


    public ObjectSet<L_TrusteeTypeValue> L_TrusteeTypeValue
    {
        get { return _l_TrusteeTypeValue  ?? (_l_TrusteeTypeValue = CreateObjectSet<L_TrusteeTypeValue>("L_TrusteeTypeValue")); }
    }
    private ObjectSet<L_TrusteeTypeValue> _l_TrusteeTypeValue;


    public ObjectSet<L_WidenSpreadsValue> L_WidenSpreadsValue
    {
        get { return _l_WidenSpreadsValue  ?? (_l_WidenSpreadsValue = CreateObjectSet<L_WidenSpreadsValue>("L_WidenSpreadsValue")); }
    }
    private ObjectSet<L_WidenSpreadsValue> _l_WidenSpreadsValue;


    public ObjectSet<LeadNote> LeadNotes
    {
        get { return _leadNotes  ?? (_leadNotes = CreateObjectSet<LeadNote>("LeadNotes")); }
    }
    private ObjectSet<LeadNote> _leadNotes;


    public ObjectSet<LiveLead> LiveLeads
    {
        get { return _liveLeads  ?? (_liveLeads = CreateObjectSet<LiveLead>("LiveLeads")); }
    }
    private ObjectSet<LiveLead> _liveLeads;


    public ObjectSet<R_Employee_Client> R_Employee_Client
    {
        get { return _r_Employee_Client  ?? (_r_Employee_Client = CreateObjectSet<R_Employee_Client>("R_Employee_Client")); }
    }
    private ObjectSet<R_Employee_Client> _r_Employee_Client;


    public ObjectSet<R_Employee_IntroducingBroker> R_Employee_IntroducingBroker
    {
        get { return _r_Employee_IntroducingBroker  ?? (_r_Employee_IntroducingBroker = CreateObjectSet<R_Employee_IntroducingBroker>("R_Employee_IntroducingBroker")); }
    }
    private ObjectSet<R_Employee_IntroducingBroker> _r_Employee_IntroducingBroker;


    public ObjectSet<R_IntroducingBroker_Client> R_IntroducingBroker_Client
    {
        get { return _r_IntroducingBroker_Client  ?? (_r_IntroducingBroker_Client = CreateObjectSet<R_IntroducingBroker_Client>("R_IntroducingBroker_Client")); }
    }
    private ObjectSet<R_IntroducingBroker_Client> _r_IntroducingBroker_Client;


    public ObjectSet<R_IntroducingBroker_IntroducingBroker> R_IntroducingBroker_IntroducingBroker
    {
        get { return _r_IntroducingBroker_IntroducingBroker  ?? (_r_IntroducingBroker_IntroducingBroker = CreateObjectSet<R_IntroducingBroker_IntroducingBroker>("R_IntroducingBroker_IntroducingBroker")); }
    }
    private ObjectSet<R_IntroducingBroker_IntroducingBroker> _r_IntroducingBroker_IntroducingBroker;


    public ObjectSet<Trader> Traders
    {
        get { return _traders  ?? (_traders = CreateObjectSet<Trader>("Traders")); }
    }
    private ObjectSet<Trader> _traders;


    public ObjectSet<TradingPlatform> TradingPlatforms
    {
        get { return _tradingPlatforms  ?? (_tradingPlatforms = CreateObjectSet<TradingPlatform>("TradingPlatforms")); }
    }
    private ObjectSet<TradingPlatform> _tradingPlatforms;


    public ObjectSet<TrustAccountInformation> TrustAccountInformations
    {
        get { return _trustAccountInformations  ?? (_trustAccountInformations = CreateObjectSet<TrustAccountInformation>("TrustAccountInformations")); }
    }
    private ObjectSet<TrustAccountInformation> _trustAccountInformations;


    public ObjectSet<Client> Clients
    {
        get { return _clients  ?? (_clients = CreateObjectSet<Client>("Clients")); }
    }
    private ObjectSet<Client> _clients;


    public ObjectSet<BankAccountInformation> BankAccountInformations
    {
        get { return _bankAccountInformations  ?? (_bankAccountInformations = CreateObjectSet<BankAccountInformation>("BankAccountInformations")); }
    }
    private ObjectSet<BankAccountInformation> _bankAccountInformations;


    public ObjectSet<ErrorLog> ErrorLogs
    {
        get { return _errorLogs  ?? (_errorLogs = CreateObjectSet<ErrorLog>("ErrorLogs")); }
    }
    private ObjectSet<ErrorLog> _errorLogs;


    public ObjectSet<User> Users
    {
        get { return _users  ?? (_users = CreateObjectSet<User>("Users")); }
    }
    private ObjectSet<User> _users;


    public ObjectSet<Transaction> Transactions
    {
        get { return _transactions  ?? (_transactions = CreateObjectSet<Transaction>("Transactions")); }
    }
    private ObjectSet<Transaction> _transactions;


    public ObjectSet<TransferLog> TransferLogs
    {
        get { return _transferLogs  ?? (_transferLogs = CreateObjectSet<TransferLog>("TransferLogs")); }
    }
    private ObjectSet<TransferLog> _transferLogs;


    public ObjectSet<L_BrokerExchangeRate> L_BrokerExchangeRate
    {
        get { return _l_BrokerExchangeRate  ?? (_l_BrokerExchangeRate = CreateObjectSet<L_BrokerExchangeRate>("L_BrokerExchangeRate")); }
    }
    private ObjectSet<L_BrokerExchangeRate> _l_BrokerExchangeRate;


    public ObjectSet<Trade> Trades
    {
        get { return _trades  ?? (_trades = CreateObjectSet<Trade>("Trades")); }
    }
    private ObjectSet<Trade> _trades;


    public ObjectSet<PartnerCommission> PartnerCommissions
    {
        get { return _partnerCommissions  ?? (_partnerCommissions = CreateObjectSet<PartnerCommission>("PartnerCommissions")); }
    }
    private ObjectSet<PartnerCommission> _partnerCommissions;


    public ObjectSet<UserImage> UserImages
    {
        get { return _userImages  ?? (_userImages = CreateObjectSet<UserImage>("UserImages")); }
    }
    private ObjectSet<UserImage> _userImages;


    public ObjectSet<Document> Documents
    {
        get { return _documents  ?? (_documents = CreateObjectSet<Document>("Documents")); }
    }
    private ObjectSet<Document> _documents;


    public ObjectSet<R_UserDocument> R_UserDocument
    {
        get { return _r_UserDocument  ?? (_r_UserDocument = CreateObjectSet<R_UserDocument>("R_UserDocument")); }
    }
    private ObjectSet<R_UserDocument> _r_UserDocument;


    public ObjectSet<UserDocument> UserDocuments
    {
        get { return _userDocuments  ?? (_userDocuments = CreateObjectSet<UserDocument>("UserDocuments")); }
    }
    private ObjectSet<UserDocument> _userDocuments;


    public ObjectSet<Agent> Agents
    {
        get { return _agents  ?? (_agents = CreateObjectSet<Agent>("Agents")); }
    }
    private ObjectSet<Agent> _agents;


    public ObjectSet<Price> Prices
    {
        get { return _prices  ?? (_prices = CreateObjectSet<Price>("Prices")); }
    }
    private ObjectSet<Price> _prices;


    public ObjectSet<Margin> Margins
    {
        get { return _margins  ?? (_margins = CreateObjectSet<Margin>("Margins")); }
    }
    private ObjectSet<Margin> _margins;


    public ObjectSet<UserRecord> UserRecords
    {
        get { return _userRecords  ?? (_userRecords = CreateObjectSet<UserRecord>("UserRecords")); }
    }
    private ObjectSet<UserRecord> _userRecords;


    public ObjectSet<ManagedAccountProgram> ManagedAccountPrograms
    {
        get { return _managedAccountPrograms  ?? (_managedAccountPrograms = CreateObjectSet<ManagedAccountProgram>("ManagedAccountPrograms")); }
    }
    private ObjectSet<ManagedAccountProgram> _managedAccountPrograms;


    public ObjectSet<ClientNote> ClientNotes
    {
        get { return _clientNotes  ?? (_clientNotes = CreateObjectSet<ClientNote>("ClientNotes")); }
    }
    private ObjectSet<ClientNote> _clientNotes;


    public ObjectSet<R_AssetManager_ClientAccount_Trader> R_AssetManager_ClientAccount_Trader
    {
        get { return _r_AssetManager_ClientAccount_Trader  ?? (_r_AssetManager_ClientAccount_Trader = CreateObjectSet<R_AssetManager_ClientAccount_Trader>("R_AssetManager_ClientAccount_Trader")); }
    }
    private ObjectSet<R_AssetManager_ClientAccount_Trader> _r_AssetManager_ClientAccount_Trader;


    public ObjectSet<InternalUserMessage> InternalUserMessages
    {
        get { return _internalUserMessages  ?? (_internalUserMessages = CreateObjectSet<InternalUserMessage>("InternalUserMessages")); }
    }
    private ObjectSet<InternalUserMessage> _internalUserMessages;


    public ObjectSet<DemoLead> DemoLeads
    {
        get { return _demoLeads  ?? (_demoLeads = CreateObjectSet<DemoLead>("DemoLeads")); }
    }
    private ObjectSet<DemoLead> _demoLeads;


    public ObjectSet<L_ActivityType> L_ActivityType
    {
        get { return _l_ActivityType  ?? (_l_ActivityType = CreateObjectSet<L_ActivityType>("L_ActivityType")); }
    }
    private ObjectSet<L_ActivityType> _l_ActivityType;


    public ObjectSet<ProfileActivity> ProfileActivities
    {
        get { return _profileActivities  ?? (_profileActivities = CreateObjectSet<ProfileActivity>("ProfileActivities")); }
    }
    private ObjectSet<ProfileActivity> _profileActivities;


    public ObjectSet<UserActivity> UserActivities
    {
        get { return _userActivities  ?? (_userActivities = CreateObjectSet<UserActivity>("UserActivities")); }
    }
    private ObjectSet<UserActivity> _userActivities;


    public ObjectSet<DocumentActivity> DocumentActivities
    {
        get { return _documentActivities  ?? (_documentActivities = CreateObjectSet<DocumentActivity>("DocumentActivities")); }
    }
    private ObjectSet<DocumentActivity> _documentActivities;


    public ObjectSet<AccountActivity> AccountActivities
    {
        get { return _accountActivities  ?? (_accountActivities = CreateObjectSet<AccountActivity>("AccountActivities")); }
    }
    private ObjectSet<AccountActivity> _accountActivities;


    public ObjectSet<L_AccountActivityType> L_AccountActivityType
    {
        get { return _l_AccountActivityType  ?? (_l_AccountActivityType = CreateObjectSet<L_AccountActivityType>("L_AccountActivityType")); }
    }
    private ObjectSet<L_AccountActivityType> _l_AccountActivityType;


    public ObjectSet<TransferActivity> TransferActivities
    {
        get { return _transferActivities  ?? (_transferActivities = CreateObjectSet<TransferActivity>("TransferActivities")); }
    }
    private ObjectSet<TransferActivity> _transferActivities;


    public ObjectSet<ConversionActivity> ConversionActivities
    {
        get { return _conversionActivities  ?? (_conversionActivities = CreateObjectSet<ConversionActivity>("ConversionActivities")); }
    }
    private ObjectSet<ConversionActivity> _conversionActivities;


    public ObjectSet<Symbol> Symbols
    {
        get { return _symbols  ?? (_symbols = CreateObjectSet<Symbol>("Symbols")); }
    }
    private ObjectSet<Symbol> _symbols;


    public ObjectSet<TradesHistory> TradesHistories
    {
        get { return _tradesHistories  ?? (_tradesHistories = CreateObjectSet<TradesHistory>("TradesHistories")); }
    }
    private ObjectSet<TradesHistory> _tradesHistories;


    public ObjectSet<BOMAMTrade> BOMAMTrades
    {
        get { return _bOMAMTrades  ?? (_bOMAMTrades = CreateObjectSet<BOMAMTrade>("BOMAMTrades")); }
    }
    private ObjectSet<BOMAMTrade> _bOMAMTrades;


    public ObjectSet<R_AssetManager_IntroducingBroker_ClientAccount> R_AssetManager_IntroducingBroker_ClientAccount
    {
        get { return _r_AssetManager_IntroducingBroker_ClientAccount  ?? (_r_AssetManager_IntroducingBroker_ClientAccount = CreateObjectSet<R_AssetManager_IntroducingBroker_ClientAccount>("R_AssetManager_IntroducingBroker_ClientAccount")); }
    }
    private ObjectSet<R_AssetManager_IntroducingBroker_ClientAccount> _r_AssetManager_IntroducingBroker_ClientAccount;


    public ObjectSet<BOMAMAlert> BOMAMAlerts
    {
        get { return _bOMAMAlerts  ?? (_bOMAMAlerts = CreateObjectSet<BOMAMAlert>("BOMAMAlerts")); }
    }
    private ObjectSet<BOMAMAlert> _bOMAMAlerts;


    public ObjectSet<AdminTransaction> AdminTransactions
    {
        get { return _adminTransactions  ?? (_adminTransactions = CreateObjectSet<AdminTransaction>("AdminTransactions")); }
    }
    private ObjectSet<AdminTransaction> _adminTransactions;


    public ObjectSet<FundingSourceAcceptedCurrency> FundingSourceAcceptedCurrencies
    {
        get { return _fundingSourceAcceptedCurrencies  ?? (_fundingSourceAcceptedCurrencies = CreateObjectSet<FundingSourceAcceptedCurrency>("FundingSourceAcceptedCurrencies")); }
    }
    private ObjectSet<FundingSourceAcceptedCurrency> _fundingSourceAcceptedCurrencies;


    public ObjectSet<L_AdminTransactionType> L_AdminTransactionType
    {
        get { return _l_AdminTransactionType  ?? (_l_AdminTransactionType = CreateObjectSet<L_AdminTransactionType>("L_AdminTransactionType")); }
    }
    private ObjectSet<L_AdminTransactionType> _l_AdminTransactionType;


    public ObjectSet<FundingSource> FundingSources
    {
        get { return _fundingSources  ?? (_fundingSources = CreateObjectSet<FundingSource>("FundingSources")); }
    }
    private ObjectSet<FundingSource> _fundingSources;


    public ObjectSet<TransactionSetting> TransactionSettings
    {
        get { return _transactionSettings  ?? (_transactionSettings = CreateObjectSet<TransactionSetting>("TransactionSettings")); }
    }
    private ObjectSet<TransactionSetting> _transactionSettings;


    public ObjectSet<DepositOrWithdrawActivity> DepositOrWithdrawActivities
    {
        get { return _depositOrWithdrawActivities  ?? (_depositOrWithdrawActivities = CreateObjectSet<DepositOrWithdrawActivity>("DepositOrWithdrawActivities")); }
    }
    private ObjectSet<DepositOrWithdrawActivity> _depositOrWithdrawActivities;


    public ObjectSet<Broker> Brokers
    {
        get { return _brokers  ?? (_brokers = CreateObjectSet<Broker>("Brokers")); }
    }
    private ObjectSet<Broker> _brokers;


    public ObjectSet<Organization> Organizations
    {
        get { return _organizations  ?? (_organizations = CreateObjectSet<Organization>("Organizations")); }
    }
    private ObjectSet<Organization> _organizations;


    public ObjectSet<Client_Account> Client_Account
    {
        get { return _client_Account  ?? (_client_Account = CreateObjectSet<Client_Account>("Client_Account")); }
    }
    private ObjectSet<Client_Account> _client_Account;


    public ObjectSet<AccountCreationRule> AccountCreationRules
    {
        get { return _accountCreationRules  ?? (_accountCreationRules = CreateObjectSet<AccountCreationRule>("AccountCreationRules")); }
    }
    private ObjectSet<AccountCreationRule> _accountCreationRules;

        #endregion

        #region Function Imports
    
    public ObjectResult<Nullable<int>> GetTradesVolumeByDay(Nullable<int> introducingBrokerID, Nullable<long> fromDate, Nullable<long> toDate)
    {


        ObjectParameter introducingBrokerIDParameter;

        if (introducingBrokerID.HasValue)
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", introducingBrokerID);
        }
        else
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", typeof(int));
        }


        ObjectParameter fromDateParameter;

        if (fromDate.HasValue)
        {
            fromDateParameter = new ObjectParameter("FromDate", fromDate);
        }
        else
        {
            fromDateParameter = new ObjectParameter("FromDate", typeof(long));
        }


        ObjectParameter toDateParameter;

        if (toDate.HasValue)
        {
            toDateParameter = new ObjectParameter("ToDate", toDate);
        }
        else
        {
            toDateParameter = new ObjectParameter("ToDate", typeof(long));
        }

        return base.ExecuteFunction<Nullable<int>>("GetTradesVolumeByDay", introducingBrokerIDParameter, fromDateParameter, toDateParameter);
    }

    public ObjectResult<Nullable<int>> sp_GetTradeVolumeForAM(Nullable<int> assetManagerId, Nullable<long> startTime, Nullable<long> endTime)
    {


        ObjectParameter assetManagerIdParameter;

        if (assetManagerId.HasValue)
        {
            assetManagerIdParameter = new ObjectParameter("assetManagerId", assetManagerId);
        }
        else
        {
            assetManagerIdParameter = new ObjectParameter("assetManagerId", typeof(int));
        }


        ObjectParameter startTimeParameter;

        if (startTime.HasValue)
        {
            startTimeParameter = new ObjectParameter("startTime", startTime);
        }
        else
        {
            startTimeParameter = new ObjectParameter("startTime", typeof(long));
        }


        ObjectParameter endTimeParameter;

        if (endTime.HasValue)
        {
            endTimeParameter = new ObjectParameter("endTime", endTime);
        }
        else
        {
            endTimeParameter = new ObjectParameter("endTime", typeof(long));
        }

        return base.ExecuteFunction<Nullable<int>>("sp_GetTradeVolumeForAM", assetManagerIdParameter, startTimeParameter, endTimeParameter);
    }

    public ObjectResult<Nullable<int>> GetTradeVolumeForAM(Nullable<int> assetManagerId, Nullable<long> startTime, Nullable<long> endTime)
    {


        ObjectParameter assetManagerIdParameter;

        if (assetManagerId.HasValue)
        {
            assetManagerIdParameter = new ObjectParameter("assetManagerId", assetManagerId);
        }
        else
        {
            assetManagerIdParameter = new ObjectParameter("assetManagerId", typeof(int));
        }


        ObjectParameter startTimeParameter;

        if (startTime.HasValue)
        {
            startTimeParameter = new ObjectParameter("startTime", startTime);
        }
        else
        {
            startTimeParameter = new ObjectParameter("startTime", typeof(long));
        }


        ObjectParameter endTimeParameter;

        if (endTime.HasValue)
        {
            endTimeParameter = new ObjectParameter("endTime", endTime);
        }
        else
        {
            endTimeParameter = new ObjectParameter("endTime", typeof(long));
        }

        return base.ExecuteFunction<Nullable<int>>("GetTradeVolumeForAM", assetManagerIdParameter, startTimeParameter, endTimeParameter);
    }

    public ObjectResult<Nullable<double>> GetCFDTradesVolumeByDay(Nullable<int> introducingBrokerID, Nullable<long> fromDate, Nullable<long> toDate)
    {


        ObjectParameter introducingBrokerIDParameter;

        if (introducingBrokerID.HasValue)
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", introducingBrokerID);
        }
        else
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", typeof(int));
        }


        ObjectParameter fromDateParameter;

        if (fromDate.HasValue)
        {
            fromDateParameter = new ObjectParameter("FromDate", fromDate);
        }
        else
        {
            fromDateParameter = new ObjectParameter("FromDate", typeof(long));
        }


        ObjectParameter toDateParameter;

        if (toDate.HasValue)
        {
            toDateParameter = new ObjectParameter("ToDate", toDate);
        }
        else
        {
            toDateParameter = new ObjectParameter("ToDate", typeof(long));
        }

        return base.ExecuteFunction<Nullable<double>>("GetCFDTradesVolumeByDay", introducingBrokerIDParameter, fromDateParameter, toDateParameter);
    }

    public ObjectResult<Nullable<double>> GetCurrencyTradesVolumeByDay(Nullable<int> introducingBrokerID, Nullable<long> fromDate, Nullable<long> toDate)
    {


        ObjectParameter introducingBrokerIDParameter;

        if (introducingBrokerID.HasValue)
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", introducingBrokerID);
        }
        else
        {
            introducingBrokerIDParameter = new ObjectParameter("IntroducingBrokerID", typeof(int));
        }


        ObjectParameter fromDateParameter;

        if (fromDate.HasValue)
        {
            fromDateParameter = new ObjectParameter("FromDate", fromDate);
        }
        else
        {
            fromDateParameter = new ObjectParameter("FromDate", typeof(long));
        }


        ObjectParameter toDateParameter;

        if (toDate.HasValue)
        {
            toDateParameter = new ObjectParameter("ToDate", toDate);
        }
        else
        {
            toDateParameter = new ObjectParameter("ToDate", typeof(long));
        }

        return base.ExecuteFunction<Nullable<double>>("GetCurrencyTradesVolumeByDay", introducingBrokerIDParameter, fromDateParameter, toDateParameter);
    }

        #endregion

    
}

}

