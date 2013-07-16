
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

public partial class L_RecievingBank
{
    #region Primitive Properties
    

    public virtual int PK_RecievingBankID
    {

        get;
        set;

    }


    public virtual string RecievingBankName
    {

        get;
        set;

    }


    public virtual int FK_OrganizationID
    {

        get { return _fK_OrganizationID; }
        set
        {

            if (_fK_OrganizationID != value)

            {

                if (Organization != null && Organization.PK_OrganizationID != value)

                {

                    Organization = null;

                }

                _fK_OrganizationID = value;
            }

        }

    }

    private int _fK_OrganizationID;

        #endregion

        #region Navigation Properties
    


    public virtual ICollection<AssetManager> AssetManagers
    {
        get
        {
            if (_assetManagers == null)
            {

                var newCollection = new FixupCollection<AssetManager>();
                newCollection.CollectionChanged += FixupAssetManagers;
                _assetManagers = newCollection;

            }
            return _assetManagers;
        }
        set
        {

            if (!ReferenceEquals(_assetManagers, value))
            {
                var previousValue = _assetManagers as FixupCollection<AssetManager>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupAssetManagers;
                }
                _assetManagers = value;
                var newValue = value as FixupCollection<AssetManager>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupAssetManagers;
                }
            }

        }
    }
    private ICollection<AssetManager> _assetManagers;



    public virtual ICollection<IntroducingBroker> IntroducingBrokers
    {
        get
        {
            if (_introducingBrokers == null)
            {

                var newCollection = new FixupCollection<IntroducingBroker>();
                newCollection.CollectionChanged += FixupIntroducingBrokers;
                _introducingBrokers = newCollection;

            }
            return _introducingBrokers;
        }
        set
        {

            if (!ReferenceEquals(_introducingBrokers, value))
            {
                var previousValue = _introducingBrokers as FixupCollection<IntroducingBroker>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupIntroducingBrokers;
                }
                _introducingBrokers = value;
                var newValue = value as FixupCollection<IntroducingBroker>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupIntroducingBrokers;
                }
            }

        }
    }
    private ICollection<IntroducingBroker> _introducingBrokers;



    public virtual ICollection<Client> Clients
    {
        get
        {
            if (_clients == null)
            {

                var newCollection = new FixupCollection<Client>();
                newCollection.CollectionChanged += FixupClients;
                _clients = newCollection;

            }
            return _clients;
        }
        set
        {

            if (!ReferenceEquals(_clients, value))
            {
                var previousValue = _clients as FixupCollection<Client>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupClients;
                }
                _clients = value;
                var newValue = value as FixupCollection<Client>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupClients;
                }
            }

        }
    }
    private ICollection<Client> _clients;



    public virtual ICollection<BankAccountInformation> BankAccountInformations
    {
        get
        {
            if (_bankAccountInformations == null)
            {

                var newCollection = new FixupCollection<BankAccountInformation>();
                newCollection.CollectionChanged += FixupBankAccountInformations;
                _bankAccountInformations = newCollection;

            }
            return _bankAccountInformations;
        }
        set
        {

            if (!ReferenceEquals(_bankAccountInformations, value))
            {
                var previousValue = _bankAccountInformations as FixupCollection<BankAccountInformation>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupBankAccountInformations;
                }
                _bankAccountInformations = value;
                var newValue = value as FixupCollection<BankAccountInformation>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupBankAccountInformations;
                }
            }

        }
    }
    private ICollection<BankAccountInformation> _bankAccountInformations;



    public virtual ICollection<Agent> Agents
    {
        get
        {
            if (_agents == null)
            {

                var newCollection = new FixupCollection<Agent>();
                newCollection.CollectionChanged += FixupAgents;
                _agents = newCollection;

            }
            return _agents;
        }
        set
        {

            if (!ReferenceEquals(_agents, value))
            {
                var previousValue = _agents as FixupCollection<Agent>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupAgents;
                }
                _agents = value;
                var newValue = value as FixupCollection<Agent>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupAgents;
                }
            }

        }
    }
    private ICollection<Agent> _agents;



    public virtual ICollection<FundingSource> FundingSources
    {
        get
        {
            if (_fundingSources == null)
            {

                var newCollection = new FixupCollection<FundingSource>();
                newCollection.CollectionChanged += FixupFundingSources;
                _fundingSources = newCollection;

            }
            return _fundingSources;
        }
        set
        {

            if (!ReferenceEquals(_fundingSources, value))
            {
                var previousValue = _fundingSources as FixupCollection<FundingSource>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupFundingSources;
                }
                _fundingSources = value;
                var newValue = value as FixupCollection<FundingSource>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupFundingSources;
                }
            }

        }
    }
    private ICollection<FundingSource> _fundingSources;



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

        #endregion

        #region Association Fixup
    

    private void FixupOrganization(Organization previousValue)
    {

        if (previousValue != null && previousValue.L_RecievingBank.Contains(this))
        {
            previousValue.L_RecievingBank.Remove(this);
        }


        if (Organization != null)
        {
            if (!Organization.L_RecievingBank.Contains(this))
            {
                Organization.L_RecievingBank.Add(this);
            }

            if (FK_OrganizationID != Organization.PK_OrganizationID)

            {
                FK_OrganizationID = Organization.PK_OrganizationID;
            }

        }

    }


    private void FixupAssetManagers(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (AssetManager item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (AssetManager item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }


    private void FixupIntroducingBrokers(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (IntroducingBroker item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (IntroducingBroker item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }


    private void FixupClients(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Client item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (Client item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }


    private void FixupBankAccountInformations(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (BankAccountInformation item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (BankAccountInformation item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }


    private void FixupAgents(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Agent item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (Agent item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }


    private void FixupFundingSources(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (FundingSource item in e.NewItems)
            {

                item.L_RecievingBank = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (FundingSource item in e.OldItems)
            {

                if (ReferenceEquals(item.L_RecievingBank, this))
                {
                    item.L_RecievingBank = null;
                }

            }
        }
    }

        #endregion

    
}

}
