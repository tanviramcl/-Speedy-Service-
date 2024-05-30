using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{

    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "TransectionDetails")]
    public class TransectionsDetails
    {
    public int IDN {get;set;}
    public String REQUESTOR_ID {get;set;}
    public String Contract {get;set;}
    public String Reference {get;set;}
    public String USER_ID {get;set;}
    public String DeliveryAddress {get;set;}
    public String CreatedOn {get;set;}
    public String TnType {get;set;}
    public String TypeName {get;set;}
    public String ITEM_ID {get;set;}
    public String OutQty {get;set;}
    public String UnitQty {get;set;}
    public String Unit {get;set;}
    public String Name {get;set;}
    public String Cost {get;set;}
    public String CID {get;set;}
    public String CatName {get;set;}
    public String UnitName { get; set; }
    public int TID {get;set;}
    public String ApproveStatus {get;set;}
    public String ContractStatus {get;set;}
    public String DeliverStatus {get;set;}
    public String OnHireStatus {get;set;}
    public String RequesterName { get; set; }
    public DateTime? HireOn { get; set; }
    public DateTime? EndOn { get; set; }
    public String LimitHireValue { get; set; }


    }
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Transection")]
    public class Transections
    { 
    public String IDN {get;set;}
    public String REQUESTOR_ID {get;set;}
    public String Name { get; set; }
    public String Contract {get;set;}
    public String Reference {get;set;}
    public String USER_ID {get;set;}
    public String DeliveryAddress {get;set;}
    public DateTime CreatedOn {get;set;}
    public String TnType {get;set;}
    public String TOTAL_COST {get;set;}
    public String ApproveStatus {get;set;}
    public String ContractStatus {get;set;}
    public String DeliverStatus {get;set;}
    public String OnHireStatus {get;set;}
    //public String DEndOn {get;set;}
    //public String DHireOn {get;set;}
    public DateTime EndOn { get; set; }
    public DateTime HireOn { get; set; }
    public String TypeName { get; set; }
    public String LimitHirevalue { get; set; }
    public String TOTAL_ITEMS { get; set; }
    }

    //Added by anis
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "TransectionStatus")]
    public class TransectionStatus
    {
        public String IDN { get; set; }
        public string TID { get; set; }
        public string ApproveStatus { get; set; }
        public string ContractStatus { get; set; }
        public string DeliverStatus { get; set; }
        public string OnHireStatus { get; set; }
        public DateTime HireOn { get; set; }
        public DateTime EndOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }


}