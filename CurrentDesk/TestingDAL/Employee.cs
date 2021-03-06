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
    [KnownType(typeof(LeadNote))]
    public partial class Employee
    {
        public Employee()
        {
            this.LeadNotes = new HashSet<LeadNote>();
        }
    
        [DataMember]
        public int PK_EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string OfficeLocation { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string StateOrProvince { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string CountryOrRegion { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string WorkPhone { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BirthDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DateHired { get; set; }
        [DataMember]
        public Nullable<decimal> Salary { get; set; }
        [DataMember]
        public string TaskNumber { get; set; }
        [DataMember]
        public Nullable<decimal> BillingRate { get; set; }
        [DataMember]
        public Nullable<short> Deductions { get; set; }
        [DataMember]
        public string SpouseName { get; set; }
        [DataMember]
        public string EmergencyContactName { get; set; }
        [DataMember]
        public string EmergencyContactPhone { get; set; }
        [DataMember]
        public byte[] Photograph { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public Nullable<int> DefaultComOnIBVol { get; set; }
        [DataMember]
        public Nullable<int> DefaultComOnACCVol { get; set; }
        [DataMember]
        public byte[] upsite_ts { get; set; }
    
        [DataMember]
        public virtual ICollection<LeadNote> LeadNotes { get; set; }
    }
    
}
