
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

public partial class L_CompanyTypeValue
{
    #region Primitive Properties
    

    public virtual int PK_CompanyTypeID
    {

        get;
        set;

    }


    public virtual string CompanyType
    {

        get;
        set;

    }

        #endregion

        #region Navigation Properties
    


    public virtual ICollection<CorporateAccountInformation> CorporateAccountInformations
    {
        get
        {
            if (_corporateAccountInformations == null)
            {

                var newCollection = new FixupCollection<CorporateAccountInformation>();
                newCollection.CollectionChanged += FixupCorporateAccountInformations;
                _corporateAccountInformations = newCollection;

            }
            return _corporateAccountInformations;
        }
        set
        {

            if (!ReferenceEquals(_corporateAccountInformations, value))
            {
                var previousValue = _corporateAccountInformations as FixupCollection<CorporateAccountInformation>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupCorporateAccountInformations;
                }
                _corporateAccountInformations = value;
                var newValue = value as FixupCollection<CorporateAccountInformation>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupCorporateAccountInformations;
                }
            }

        }
    }
    private ICollection<CorporateAccountInformation> _corporateAccountInformations;



    public virtual ICollection<TrustAccountInformation> TrustAccountInformations
    {
        get
        {
            if (_trustAccountInformations == null)
            {

                var newCollection = new FixupCollection<TrustAccountInformation>();
                newCollection.CollectionChanged += FixupTrustAccountInformations;
                _trustAccountInformations = newCollection;

            }
            return _trustAccountInformations;
        }
        set
        {

            if (!ReferenceEquals(_trustAccountInformations, value))
            {
                var previousValue = _trustAccountInformations as FixupCollection<TrustAccountInformation>;
                if (previousValue != null)
                {
                    previousValue.CollectionChanged -= FixupTrustAccountInformations;
                }
                _trustAccountInformations = value;
                var newValue = value as FixupCollection<TrustAccountInformation>;
                if (newValue != null)
                {
                    newValue.CollectionChanged += FixupTrustAccountInformations;
                }
            }

        }
    }
    private ICollection<TrustAccountInformation> _trustAccountInformations;

        #endregion

        #region Association Fixup
    

    private void FixupCorporateAccountInformations(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (CorporateAccountInformation item in e.NewItems)
            {

                item.L_CompanyTypeValue = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (CorporateAccountInformation item in e.OldItems)
            {

                if (ReferenceEquals(item.L_CompanyTypeValue, this))
                {
                    item.L_CompanyTypeValue = null;
                }

            }
        }
    }


    private void FixupTrustAccountInformations(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (TrustAccountInformation item in e.NewItems)
            {

                item.L_CompanyTypeValue = this;

            }
        }

        if (e.OldItems != null)
        {
            foreach (TrustAccountInformation item in e.OldItems)
            {

                if (ReferenceEquals(item.L_CompanyTypeValue, this))
                {
                    item.L_CompanyTypeValue = null;
                }

            }
        }
    }

        #endregion

    
}

}
