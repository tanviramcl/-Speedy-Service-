using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
  [DataMember(ID_FIELD = "MailQueueID", TABLE_NAME = "MailQueue")]
 public class MailQueue
    {
        public int MailQueueID{get;set;}	
        public String Sender{get;set;}		
        public String Recipients{get;set;}		
        public String CC{get;set;}		
        public String BCC{get;set;}		
        public String Subject{get;set;}		
        public String BodyText{get;set;}		
        public String BodyHtml	{get;set;}	
        public String MailStatusID{get;set;}		
        public String AccountID	{get;set;}	
        public DateTime DateCreated	{get;set;}
        public DateTime DateModified { get; set; }	
    }
      [DataMember(ID_FIELD = "MailStatusID", TABLE_NAME = "MailStatusID")]
     public class MailStatus
     {
        public String MailStatusID	{get;set;}
        public String Name { get; set; }	
     }

     [DataMember(ID_FIELD = "MailStatusID", TABLE_NAME = "MailStatusID")]
    public class Attachment
    {
      public int  AttachmentID{get;set;}	
      public int  MailQueueID{get;set;}
      public String FilePath { get; set; }	
    }
}