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
    [KnownType(typeof(L_AccountFormType))]
    [KnownType(typeof(ClientInformation))]
    [KnownType(typeof(L_CurrencyValue))]
    [KnownType(typeof(DemoLead))]
    [KnownType(typeof(LiveLead))]
    public partial class AccountCurrency
    {
        public AccountCurrency()
        {
            this.DemoLeads = new HashSet<DemoLead>();
            this.LiveLeads = new HashSet<LiveLead>();
        }
    
        [DataMember]
        public int PK_AccountCurrencyID { get; set; }
        [DataMember]
        public int FK_ClientID { get; set; }
        [DataMember]
        public int FK_AccountFormTypeID { get; set; }
        [DataMember]
        public int FK_CurrencyValueID { get; set; }
    
        [DataMember]
        public virtual L_AccountFormType L_AccountFormType { get; set; }
        [DataMember]
        public virtual ClientInformation ClientInformation { get; set; }
        [DataMember]
        public virtual L_CurrencyValue L_CurrencyValue { get; set; }
        [DataMember]
        public virtual ICollection<DemoLead> DemoLeads { get; set; }
        [DataMember]
        public virtual ICollection<LiveLead> LiveLeads { get; set; }
    }
    
}
