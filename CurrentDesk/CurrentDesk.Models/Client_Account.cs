
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace CurrentDesk.Models
{

public partial class Client_Account
{
    #region Primitive Properties
    

    public virtual int PK_ClientAccountID
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_ClientID
    {

        get { return _fK_ClientID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_ClientID != value)

            {

                if (Client != null && Client.PK_ClientID != value)

                {

                    Client = null;

                }

                _fK_ClientID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_ClientID;


    public virtual Nullable<int> FK_CurrencyID
    {

        get { return _fK_CurrencyID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_CurrencyID != value)

            {

                if (L_CurrencyValue != null && L_CurrencyValue.PK_CurrencyValueID != value)

                {

                    L_CurrencyValue = null;

                }

                _fK_CurrencyID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_CurrencyID;


    public virtual Nullable<int> FK_InitialInvestmentID
    {

        get { return _fK_InitialInvestmentID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_InitialInvestmentID != value)

            {

                if (L_InitialInvestment != null && L_InitialInvestment.PK_InitialInvestmentID != value)

                {

                    L_InitialInvestment = null;

                }

                _fK_InitialInvestmentID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_InitialInvestmentID;


    public virtual Nullable<int> NumberOfDeposit
    {

        get;
        set;

    }


    public virtual Nullable<decimal> TotalDeposit
    {

        get;
        set;

    }


    public virtual Nullable<decimal> CurrentBalance
    {

        get;
        set;

    }


    public virtual Nullable<decimal> LastMonthBalance
    {

        get;
        set;

    }


    public virtual string HighWaterMark
    {

        get;
        set;

    }


    public virtual string LandingAccount
    {

        get;
        set;

    }


    public virtual string TradingAccount
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_IntroducingBrokerID
    {

        get { return _fK_IntroducingBrokerID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_IntroducingBrokerID != value)

            {

                if (IntroducingBroker != null && IntroducingBroker.PK_IntroducingBrokerID != value)

                {

                    IntroducingBroker = null;

                }

                _fK_IntroducingBrokerID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_IntroducingBrokerID;


    public virtual Nullable<long> AccountNumber
    {

        get;
        set;

    }


    public virtual Nullable<bool> IsLandingAccount
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_PlatformID
    {

        get;
        set;

    }


    public virtual Nullable<int> PlatformLogin
    {

        get;
        set;

    }


    public virtual string AccountName
    {

        get;
        set;

    }


    public virtual Nullable<bool> IsTradingAccount
    {

        get;
        set;

    }


    public virtual string PlatformPassword
    {

        get;
        set;

    }


    public virtual Nullable<System.DateTime> LastTradingDate
    {

        get;
        set;

    }


    public virtual Nullable<System.DateTime> LastFundingDate
    {

        get;
        set;

    }


    public virtual string Group
    {

        get;
        set;

    }


    public virtual string Leverage
    {

        get;
        set;

    }


    public virtual Nullable<decimal> Credit
    {

        get;
        set;

    }


    public virtual Nullable<decimal> Equity
    {

        get;
        set;

    }


    public virtual string Comment
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_FeeStructureID
    {

        get { return _fK_FeeStructureID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_FeeStructureID != value)

            {

                if (PartnerCommission != null && PartnerCommission.PK_PartnerCommID != value)

                {

                    PartnerCommission = null;

                }

                _fK_FeeStructureID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_FeeStructureID;


    public virtual Nullable<decimal> Commission
    {

        get;
        set;

    }


    public virtual Nullable<int> SpreadDiff
    {

        get;
        set;

    }


    public virtual Nullable<double> SwapBalance
    {

        get;
        set;

    }


    public virtual Nullable<decimal> StopOut
    {

        get;
        set;

    }


    public virtual Nullable<decimal> Margin
    {

        get;
        set;

    }


    public virtual Nullable<bool> IsBBook
    {

        get;
        set;

    }


    public virtual int FK_OrganizationID
    {

        get { return _fK_OrganizationID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_OrganizationID != value)

            {

                if (Organization != null && Organization.PK_OrganizationID != value)

                {

                    Organization = null;

                }

                _fK_OrganizationID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private int _fK_OrganizationID;

        #endregion

        #region Navigation Properties
    


    public virtual ICollection<AccountActivity> AccountActivities
    {
        get
        {
            if (_accountActivities == null)
            {

                var newCollection = new FixupCollection<AccountActivity>();
                newCollection.CollectionChanged += FixupAccountActivities;
                _accountActivities = newCollection;

            }
            return _accountActivities;
        }
        set
        {

            if (!ReferenceEquals(_accountActivities, value))
            {
                var previousValue = _accountActivities as FixupCollection<AccountActivity>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupAccountActivities;
                }
                _accountActivities = value;
                var newValue = value as FixupCollection<AccountActivity>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupAccountActivities;
                }
            }

        }
    }
    private ICollection<AccountActivity> _accountActivities;



    public virtual ICollection<BOMAMTrade> BOMAMTrades
    {
        get
        {
            if (_bOMAMTrades == null)
            {

                var newCollection = new FixupCollection<BOMAMTrade>();
                newCollection.CollectionChanged += FixupBOMAMTrades;
                _bOMAMTrades = newCollection;

            }
            return _bOMAMTrades;
        }
        set
        {

            if (!ReferenceEquals(_bOMAMTrades, value))
            {
                var previousValue = _bOMAMTrades as FixupCollection<BOMAMTrade>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupBOMAMTrades;
                }
                _bOMAMTrades = value;
                var newValue = value as FixupCollection<BOMAMTrade>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupBOMAMTrades;
                }
            }

        }
    }
    private ICollection<BOMAMTrade> _bOMAMTrades;



    public virtual Client Client
    {

        get { return _client; }
        set
        {
            if (!ReferenceEquals(_client, value))
            {
                var previousValue = _client;
                _client = value;
                FixupClient(previousValue);
            }
        }
    }
    private Client _client;



    public virtual PartnerCommission PartnerCommission
    {

        get { return _partnerCommission; }
        set
        {
            if (!ReferenceEquals(_partnerCommission, value))
            {
                var previousValue = _partnerCommission;
                _partnerCommission = value;
                FixupPartnerCommission(previousValue);
            }
        }
    }
    private PartnerCommission _partnerCommission;



    public virtual IntroducingBroker IntroducingBroker
    {

        get { return _introducingBroker; }
        set
        {
            if (!ReferenceEquals(_introducingBroker, value))
            {
                var previousValue = _introducingBroker;
                _introducingBroker = value;
                FixupIntroducingBroker(previousValue);
            }
        }
    }
    private IntroducingBroker _introducingBroker;



    public virtual ICollection<R_AssetManager_ClientAccount_Trader> R_AssetManager_ClientAccount_Trader
    {
        get
        {
            if (_r_AssetManager_ClientAccount_Trader == null)
            {

                var newCollection = new FixupCollection<R_AssetManager_ClientAccount_Trader>();
                newCollection.CollectionChanged += FixupR_AssetManager_ClientAccount_Trader;
                _r_AssetManager_ClientAccount_Trader = newCollection;

            }
            return _r_AssetManager_ClientAccount_Trader;
        }
        set
        {

            if (!ReferenceEquals(_r_AssetManager_ClientAccount_Trader, value))
            {
                var previousValue = _r_AssetManager_ClientAccount_Trader as FixupCollection<R_AssetManager_ClientAccount_Trader>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupR_AssetManager_ClientAccount_Trader;
                }
                _r_AssetManager_ClientAccount_Trader = value;
                var newValue = value as FixupCollection<R_AssetManager_ClientAccount_Trader>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupR_AssetManager_ClientAccount_Trader;
                }
            }

        }
    }
    private ICollection<R_AssetManager_ClientAccount_Trader> _r_AssetManager_ClientAccount_Trader;



    public virtual L_CurrencyValue L_CurrencyValue
    {

        get { return _l_CurrencyValue; }
        set
        {
            if (!ReferenceEquals(_l_CurrencyValue, value))
            {
                var previousValue = _l_CurrencyValue;
                _l_CurrencyValue = value;
                FixupL_CurrencyValue(previousValue);
            }
        }
    }
    private L_CurrencyValue _l_CurrencyValue;



    public virtual L_InitialInvestment L_InitialInvestment
    {

        get { return _l_InitialInvestment; }
        set
        {
            if (!ReferenceEquals(_l_InitialInvestment, value))
            {
                var previousValue = _l_InitialInvestment;
                _l_InitialInvestment = value;
                FixupL_InitialInvestment(previousValue);
            }
        }
    }
    private L_InitialInvestment _l_InitialInvestment;



    public virtual Organization Organization
    {

        get { return _organization; }
        set
        {
            if (!ReferenceEquals(_organization, value))
            {
                var previousValue = _organization;
                _organization = value;
                FixupOrganization(previousValue);
            }
        }
    }
    private Organization _organization;



    public virtual ICollection<R_AssetManager_IntroducingBroker_ClientAccount> R_AssetManager_IntroducingBroker_ClientAccount
    {
        get
        {
            if (_r_AssetManager_IntroducingBroker_ClientAccount == null)
            {

                var newCollection = new FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>();
                newCollection.CollectionChanged += FixupR_AssetManager_IntroducingBroker_ClientAccount;
                _r_AssetManager_IntroducingBroker_ClientAccount = newCollection;

            }
            return _r_AssetManager_IntroducingBroker_ClientAccount;
        }
        set
        {

            if (!ReferenceEquals(_r_AssetManager_IntroducingBroker_ClientAccount, value))
            {
                var previousValue = _r_AssetManager_IntroducingBroker_ClientAccount as FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupR_AssetManager_IntroducingBroker_ClientAccount;
                }
                _r_AssetManager_IntroducingBroker_ClientAccount = value;
                var newValue = value as FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupR_AssetManager_IntroducingBroker_ClientAccount;
                }
            }

        }
    }
    private ICollection<R_AssetManager_IntroducingBroker_ClientAccount> _r_AssetManager_IntroducingBroker_ClientAccount;



    public virtual ICollection<R_AssetManager_IntroducingBroker_ClientAccount> R_AssetManager_IntroducingBroker_ClientAccount1
    {
        get
        {
            if (_r_AssetManager_IntroducingBroker_ClientAccount1 == null)
            {

                var newCollection = new FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>();
                newCollection.CollectionChanged += FixupR_AssetManager_IntroducingBroker_ClientAccount1;
                _r_AssetManager_IntroducingBroker_ClientAccount1 = newCollection;

            }
            return _r_AssetManager_IntroducingBroker_ClientAccount1;
        }
        set
        {

            if (!ReferenceEquals(_r_AssetManager_IntroducingBroker_ClientAccount1, value))
            {
                var previousValue = _r_AssetManager_IntroducingBroker_ClientAccount1 as FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupR_AssetManager_IntroducingBroker_ClientAccount1;
                }
                _r_AssetManager_IntroducingBroker_ClientAccount1 = value;
                var newValue = value as FixupCollection<R_AssetManager_IntroducingBroker_ClientAccount>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupR_AssetManager_IntroducingBroker_ClientAccount1;
                }
            }

        }
    }
    private ICollection<R_AssetManager_IntroducingBroker_ClientAccount> _r_AssetManager_IntroducingBroker_ClientAccount1;



    public virtual ICollection<R_AssetManager_ClientAccount_Trader> R_AssetManager_ClientAccount_Trader1
    {
        get
        {
            if (_r_AssetManager_ClientAccount_Trader1 == null)
            {

                var newCollection = new FixupCollection<R_AssetManager_ClientAccount_Trader>();
                newCollection.CollectionChanged += FixupR_AssetManager_ClientAccount_Trader1;
                _r_AssetManager_ClientAccount_Trader1 = newCollection;

            }
            return _r_AssetManager_ClientAccount_Trader1;
        }
        set
        {

            if (!ReferenceEquals(_r_AssetManager_ClientAccount_Trader1, value))
            {
                var previousValue = _r_AssetManager_ClientAccount_Trader1 as FixupCollection<R_AssetManager_ClientAccount_Trader>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupR_AssetManager_ClientAccount_Trader1;
                }
                _r_AssetManager_ClientAccount_Trader1 = value;
                var newValue = value as FixupCollection<R_AssetManager_ClientAccount_Trader>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupR_AssetManager_ClientAccount_Trader1;
                }
            }

        }
    }
    private ICollection<R_AssetManager_ClientAccount_Trader> _r_AssetManager_ClientAccount_Trader1;

        #endregion

        #region Association Fixup
    

    private bool _settingFK = false;


    private void FixupClient(Client previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (Client != null)
        {
            if (!Client.Client_Account.Contains(this))
            {
                Client.Client_Account.Add(this);
            }

            if (FK_ClientID != Client.PK_ClientID)

            {
                FK_ClientID = Client.PK_ClientID;
            }

        }

        else if (!_settingFK)

        {

            FK_ClientID = null;

        }

    }


    private void FixupPartnerCommission(PartnerCommission previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (PartnerCommission != null)
        {
            if (!PartnerCommission.Client_Account.Contains(this))
            {
                PartnerCommission.Client_Account.Add(this);
            }

            if (FK_FeeStructureID != PartnerCommission.PK_PartnerCommID)

            {
                FK_FeeStructureID = PartnerCommission.PK_PartnerCommID;
            }

        }

        else if (!_settingFK)

        {

            FK_FeeStructureID = null;

        }

    }


    private void FixupIntroducingBroker(IntroducingBroker previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (IntroducingBroker != null)
        {
            if (!IntroducingBroker.Client_Account.Contains(this))
            {
                IntroducingBroker.Client_Account.Add(this);
            }

            if (FK_IntroducingBrokerID != IntroducingBroker.PK_IntroducingBrokerID)

            {
                FK_IntroducingBrokerID = IntroducingBroker.PK_IntroducingBrokerID;
            }

        }

        else if (!_settingFK)

        {

            FK_IntroducingBrokerID = null;

        }

    }


    private void FixupL_CurrencyValue(L_CurrencyValue previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (L_CurrencyValue != null)
        {
            if (!L_CurrencyValue.Client_Account.Contains(this))
            {
                L_CurrencyValue.Client_Account.Add(this);
            }

            if (FK_CurrencyID != L_CurrencyValue.PK_CurrencyValueID)

            {
                FK_CurrencyID = L_CurrencyValue.PK_CurrencyValueID;
            }

        }

        else if (!_settingFK)

        {

            FK_CurrencyID = null;

        }

    }


    private void FixupL_InitialInvestment(L_InitialInvestment previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (L_InitialInvestment != null)
        {
            if (!L_InitialInvestment.Client_Account.Contains(this))
            {
                L_InitialInvestment.Client_Account.Add(this);
            }

            if (FK_InitialInvestmentID != L_InitialInvestment.PK_InitialInvestmentID)

            {
                FK_InitialInvestmentID = L_InitialInvestment.PK_InitialInvestmentID;
            }

        }

        else if (!_settingFK)

        {

            FK_InitialInvestmentID = null;

        }

    }


    private void FixupOrganization(Organization previousValue)
    {

        if (previousValue != null && previousValue.Client_Account.Contains(this))
        {
            previousValue.Client_Account.Remove(this);
        }


        if (Organization != null)
        {
            if (!Organization.Client_Account.Contains(this))
            {
                Organization.Client_Account.Add(this);
            }

            if (FK_OrganizationID != Organization.PK_OrganizationID)

            {
                FK_OrganizationID = Organization.PK_OrganizationID;
            }

        }

    }


    private void FixupAccountActivities(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (AccountActivity item in e.NewItems)
            {

                item.Client_Account = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (AccountActivity item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account, this))
                {
                    item.Client_Account = null;
                }

            }
        }
    }


    private void FixupBOMAMTrades(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (BOMAMTrade item in e.NewItems)
            {

                item.Client_Account = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (BOMAMTrade item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account, this))
                {
                    item.Client_Account = null;
                }

            }
        }
    }


    private void FixupR_AssetManager_ClientAccount_Trader(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (R_AssetManager_ClientAccount_Trader item in e.NewItems)
            {

                item.Client_Account = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (R_AssetManager_ClientAccount_Trader item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account, this))
                {
                    item.Client_Account = null;
                }

            }
        }
    }


    private void FixupR_AssetManager_IntroducingBroker_ClientAccount(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (R_AssetManager_IntroducingBroker_ClientAccount item in e.NewItems)
            {

                item.Client_Account = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (R_AssetManager_IntroducingBroker_ClientAccount item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account, this))
                {
                    item.Client_Account = null;
                }

            }
        }
    }


    private void FixupR_AssetManager_IntroducingBroker_ClientAccount1(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (R_AssetManager_IntroducingBroker_ClientAccount item in e.NewItems)
            {

                item.Client_Account1 = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (R_AssetManager_IntroducingBroker_ClientAccount item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account1, this))
                {
                    item.Client_Account1 = null;
                }

            }
        }
    }


    private void FixupR_AssetManager_ClientAccount_Trader1(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (R_AssetManager_ClientAccount_Trader item in e.NewItems)
            {

                item.Client_Account1 = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (R_AssetManager_ClientAccount_Trader item in e.OldItems)
            {

                if (ReferenceEquals(item.Client_Account1, this))
                {
                    item.Client_Account1 = null;
                }

            }
        }
    }

        #endregion

    
}

}
