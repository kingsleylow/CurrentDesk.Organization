//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace TestingDAL
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Client))]
    [KnownType(typeof(L_Country))]
    [KnownType(typeof(L_CompanyTypeValue))]
    [KnownType(typeof(L_IDInformationType))]
    public partial class CorporateAccountInformation
    {
        [DataMember]
        public int PK_CorporateAccountID { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public Nullable<int> FK_CompanyTypeID { get; set; }
        [DataMember]
        public string CompanyAddress { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public Nullable<int> FK_CompanyCountryID { get; set; }
        [DataMember]
        public string CompanyPostalCode { get; set; }
        [DataMember]
        public string AuthOfficerTitle { get; set; }
        [DataMember]
        public string AuthOfficerFirstName { get; set; }
        [DataMember]
        public string AuthOfficerLastName { get; set; }
        [DataMember]
        public string AuthOfficerMiddleName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> AuthOfficerBirthDate { get; set; }
        [DataMember]
        public Nullable<bool> AuthOfficerGender { get; set; }
        [DataMember]
        public Nullable<int> FK_AuthOfficerCitizenshipCountryID { get; set; }
        [DataMember]
        public Nullable<int> FK_AuthOfficerInformationTypeID { get; set; }
        [DataMember]
        public string AuthOfficerIDNumber { get; set; }
        [DataMember]
        public Nullable<int> FK_AuthOfficerResidenceCountryID { get; set; }
        [DataMember]
        public string AuthOfficerAddress { get; set; }
        [DataMember]
        public string AuthOfficerCity { get; set; }
        [DataMember]
        public Nullable<int> FK_AuthOfficerCountryID { get; set; }
        [DataMember]
        public string AuthOfficerPostalCode { get; set; }
        [DataMember]
        public string AuthOfficerTelephoneNumber { get; set; }
        [DataMember]
        public string AuthOfficerMobileNumber { get; set; }
        [DataMember]
        public string AuthOfficerEmailAddress { get; set; }
        [DataMember]
        public Nullable<int> FK_ClientID { get; set; }
    
        [DataMember]
        public virtual Client Client { get; set; }
        [DataMember]
        public virtual L_Country L_Country { get; set; }
        [DataMember]
        public virtual L_Country L_Country1 { get; set; }
        [DataMember]
        public virtual L_Country L_Country2 { get; set; }
        [DataMember]
        public virtual L_Country L_Country3 { get; set; }
        [DataMember]
        public virtual L_CompanyTypeValue L_CompanyTypeValue { get; set; }
        [DataMember]
        public virtual L_IDInformationType L_IDInformationType { get; set; }
    }
    
}