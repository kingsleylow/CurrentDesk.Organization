
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

public partial class L_InitialInvestment
{
    #region Primitive Properties
    

    public virtual int PK_InitialInvestmentID
    {

        get;
        set;

    }


    public virtual string InvestmentValue
    {

        get;
        set;

    }

        #endregion

        #region Navigation Properties
    


    public virtual ICollection<DemoLead> DemoLeads
    {
        get
        {
            if (_demoLeads == null)
            {

                var newCollection = new FixupCollection<DemoLead>();
                newCollection.CollectionChanged += FixupDemoLeads;
                _demoLeads = newCollection;

            }
            return _demoLeads;
        }
        set
        {

            if (!ReferenceEquals(_demoLeads, value))
            {
                var previousValue = _demoLeads as FixupCollection<DemoLead>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupDemoLeads;
                }
                _demoLeads = value;
                var newValue = value as FixupCollection<DemoLead>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupDemoLeads;
                }
            }

        }
    }
    private ICollection<DemoLead> _demoLeads;



    public virtual ICollection<Client_Account> Client_Account
    {
        get
        {
            if (_client_Account == null)
            {

                var newCollection = new FixupCollection<Client_Account>();
                newCollection.CollectionChanged += FixupClient_Account;
                _client_Account = newCollection;

            }
            return _client_Account;
        }
        set
        {

            if (!ReferenceEquals(_client_Account, value))
            {
                var previousValue = _client_Account as FixupCollection<Client_Account>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupClient_Account;
                }
                _client_Account = value;
                var newValue = value as FixupCollection<Client_Account>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupClient_Account;
                }
            }

        }
    }
    private ICollection<Client_Account> _client_Account;

        #endregion

        #region Association Fixup
    

    private void FixupDemoLeads(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (DemoLead item in e.NewItems)
            {

                item.L_InitialInvestment = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (DemoLead item in e.OldItems)
            {

                if (ReferenceEquals(item.L_InitialInvestment, this))
                {
                    item.L_InitialInvestment = null;
                }

            }
        }
    }


    private void FixupClient_Account(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Client_Account item in e.NewItems)
            {

                item.L_InitialInvestment = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (Client_Account item in e.OldItems)
            {

                if (ReferenceEquals(item.L_InitialInvestment, this))
                {
                    item.L_InitialInvestment = null;
                }

            }
        }
    }

        #endregion

    
}

}