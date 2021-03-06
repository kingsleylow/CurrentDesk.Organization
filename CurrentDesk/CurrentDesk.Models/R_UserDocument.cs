
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

public partial class R_UserDocument
{
    #region Primitive Properties
    

    public virtual int PK_ID
    {

        get;
        set;

    }


    public virtual Nullable<int> FK_AccountTypeID
    {

        get { return _fK_AccountTypeID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_AccountTypeID != value)

            {

                if (AccountType != null && AccountType.PK_AccountType != value)

                {

                    AccountType = null;

                }

                _fK_AccountTypeID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_AccountTypeID;


    public virtual Nullable<int> FK_DocumentID
    {

        get { return _fK_DocumentID; }
        set
        {

            try
            {
                _settingFK = true;

            if (_fK_DocumentID != value)

            {

                if (Document != null && Document.PK_DocumentID != value)

                {

                    Document = null;

                }

                _fK_DocumentID = value;
            }

            }
            finally
            {
                _settingFK = false;
            }

        }

    }

    private Nullable<int> _fK_DocumentID;


    public virtual Nullable<bool> IsBrokerForm
    {

        get;
        set;

    }

        #endregion

        #region Navigation Properties
    


    public virtual AccountType AccountType
    {

        get { return _accountType; }
        set
        {
            if (!ReferenceEquals(_accountType, value))
            {
                var previousValue = _accountType;
                _accountType = value;
                FixupAccountType(previousValue);
            }
        }
    }
    private AccountType _accountType;



    public virtual Document Document
    {

        get { return _document; }
        set
        {
            if (!ReferenceEquals(_document, value))
            {
                var previousValue = _document;
                _document = value;
                FixupDocument(previousValue);
            }
        }
    }
    private Document _document;

        #endregion

        #region Association Fixup
    

    private bool _settingFK = false;


    private void FixupAccountType(AccountType previousValue)
    {

        if (previousValue != null && previousValue.R_UserDocument.Contains(this))
        {
            previousValue.R_UserDocument.Remove(this);
        }


        if (AccountType != null)
        {
            if (!AccountType.R_UserDocument.Contains(this))
            {
                AccountType.R_UserDocument.Add(this);
            }

            if (FK_AccountTypeID != AccountType.PK_AccountType)

            {
                FK_AccountTypeID = AccountType.PK_AccountType;
            }

        }

        else if (!_settingFK)

        {

            FK_AccountTypeID = null;

        }

    }


    private void FixupDocument(Document previousValue)
    {

        if (previousValue != null && previousValue.R_UserDocument.Contains(this))
        {
            previousValue.R_UserDocument.Remove(this);
        }


        if (Document != null)
        {
            if (!Document.R_UserDocument.Contains(this))
            {
                Document.R_UserDocument.Add(this);
            }

            if (FK_DocumentID != Document.PK_DocumentID)

            {
                FK_DocumentID = Document.PK_DocumentID;
            }

        }

        else if (!_settingFK)

        {

            FK_DocumentID = null;

        }

    }

        #endregion

    
}

}
