
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

public partial class BankAccountInformation
{
    #region Primitive Properties
    

    public virtual int PK_BankAccountInformationID
    {

        get;
        set;

    }


    public virtual string BankName
    {

        get;
        set;

    }


    public virtual string AccountNumber
    {

        get;
        set;

    }


    public virtual string BicNumberOrSwiftCode
    {

        get;
        set;

    }


    public virtual string BankingAddress
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_ReceivingBankInformationID
    {

        get { return _fK_ReceivingBankInformationID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_ReceivingBankInformationID != value)

            {

                if (L_RecievingBank != null && L_RecievingBank.PK_RecievingBankID != value)

                {

                    L_RecievingBank = null;

                }

                _fK_ReceivingBankInformationID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_ReceivingBankInformationID;


    public virtual string City
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_CountryID
    {

        get { return _fK_CountryID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_CountryID != value)

            {

                if (L_Country != null && L_Country.PK_CountryID != value)

                {

                    L_Country = null;

                }

                _fK_CountryID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_CountryID;


    public virtual string PostalCode
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


    public virtual string ReceivingBankInfo
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

        #endregion

        #region Navigation Properties
    


    public virtual L_Country L_Country
    {

        get { return _l_Country; }
        set
        {
            if (!ReferenceEquals(_l_Country, value))
            {
                var previousValue = _l_Country;
                _l_Country = value;
                FixupL_Country(previousValue);
            }
        }
    }
    private L_Country _l_Country;



    public virtual L_RecievingBank L_RecievingBank
    {

        get { return _l_RecievingBank; }
        set
        {
            if (!ReferenceEquals(_l_RecievingBank, value))
            {
                var previousValue = _l_RecievingBank;
                _l_RecievingBank = value;
                FixupL_RecievingBank(previousValue);
            }
        }
    }
    private L_RecievingBank _l_RecievingBank;



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



    public virtual ICollection<DepositOrWithdrawActivity> DepositOrWithdrawActivities
    {
        get
        {
            if (_depositOrWithdrawActivities == null)
            {

                var newCollection = new FixupCollection<DepositOrWithdrawActivity>();
                newCollection.CollectionChanged += FixupDepositOrWithdrawActivities;
                _depositOrWithdrawActivities = newCollection;

            }
            return _depositOrWithdrawActivities;
        }
        set
        {

            if (!ReferenceEquals(_depositOrWithdrawActivities, value))
            {
                var previousValue = _depositOrWithdrawActivities as FixupCollection<DepositOrWithdrawActivity>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupDepositOrWithdrawActivities;
                }
                _depositOrWithdrawActivities = value;
                var newValue = value as FixupCollection<DepositOrWithdrawActivity>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupDepositOrWithdrawActivities;
                }
            }

        }
    }
    private ICollection<DepositOrWithdrawActivity> _depositOrWithdrawActivities;



    public virtual ICollection<AdminTransaction> AdminTransactions
    {
        get
        {
            if (_adminTransactions == null)
            {

                var newCollection = new FixupCollection<AdminTransaction>();
                newCollection.CollectionChanged += FixupAdminTransactions;
                _adminTransactions = newCollection;

            }
            return _adminTransactions;
        }
        set
        {

            if (!ReferenceEquals(_adminTransactions, value))
            {
                var previousValue = _adminTransactions as FixupCollection<AdminTransaction>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupAdminTransactions;
                }
                _adminTransactions = value;
                var newValue = value as FixupCollection<AdminTransaction>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupAdminTransactions;
                }
            }

        }
    }
    private ICollection<AdminTransaction> _adminTransactions;

        #endregion

        #region Association Fixup
    

    private bool _settingFK = false;


    private void FixupL_Country(L_Country previousValue)
    {

        if (previousValue != null && previousValue.BankAccountInformations.Contains(this))
        {
            previousValue.BankAccountInformations.Remove(this);
        }


        if (L_Country != null)
        {
            if (!L_Country.BankAccountInformations.Contains(this))
            {
                L_Country.BankAccountInformations.Add(this);
            }

            if (FK_CountryID != L_Country.PK_CountryID)

            {
                FK_CountryID = L_Country.PK_CountryID;
            }

        }

        else if (!_settingFK)

        {

            FK_CountryID = null;

        }

    }


    private void FixupL_RecievingBank(L_RecievingBank previousValue)
    {

        if (previousValue != null && previousValue.BankAccountInformations.Contains(this))
        {
            previousValue.BankAccountInformations.Remove(this);
        }


        if (L_RecievingBank != null)
        {
            if (!L_RecievingBank.BankAccountInformations.Contains(this))
            {
                L_RecievingBank.BankAccountInformations.Add(this);
            }

            if (FK_ReceivingBankInformationID != L_RecievingBank.PK_RecievingBankID)

            {
                FK_ReceivingBankInformationID = L_RecievingBank.PK_RecievingBankID;
            }

        }

        else if (!_settingFK)

        {

            FK_ReceivingBankInformationID = null;

        }

    }


    private void FixupClient(Client previousValue)
    {

        if (previousValue != null && previousValue.BankAccountInformations.Contains(this))
        {
            previousValue.BankAccountInformations.Remove(this);
        }


        if (Client != null)
        {
            if (!Client.BankAccountInformations.Contains(this))
            {
                Client.BankAccountInformations.Add(this);
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


    private void FixupIntroducingBroker(IntroducingBroker previousValue)
    {

        if (previousValue != null && previousValue.BankAccountInformations.Contains(this))
        {
            previousValue.BankAccountInformations.Remove(this);
        }


        if (IntroducingBroker != null)
        {
            if (!IntroducingBroker.BankAccountInformations.Contains(this))
            {
                IntroducingBroker.BankAccountInformations.Add(this);
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


    private void FixupDepositOrWithdrawActivities(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (DepositOrWithdrawActivity item in e.NewItems)
            {

                item.BankAccountInformation = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (DepositOrWithdrawActivity item in e.OldItems)
            {

                if (ReferenceEquals(item.BankAccountInformation, this))
                {
                    item.BankAccountInformation = null;
                }

            }
        }
    }


    private void FixupAdminTransactions(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (AdminTransaction item in e.NewItems)
            {

                item.BankAccountInformation = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (AdminTransaction item in e.OldItems)
            {

                if (ReferenceEquals(item.BankAccountInformation, this))
                {
                    item.BankAccountInformation = null;
                }

            }
        }
    }

        #endregion

    
}

}