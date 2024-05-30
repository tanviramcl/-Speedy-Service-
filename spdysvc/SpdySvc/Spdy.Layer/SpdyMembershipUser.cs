using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SpdySvc.Models;
namespace SpdySvc.Models.SpdyMembershipUser
{
    public class SpdyMembershipUser : MembershipUser
    {
        private bool _IsSubscriber;
        private string _CustomerID;

        private int _userID;
        private Authorization _authorization;

        public Authorization SpdyAuthorization 
        {
            get { return _authorization; }
            set { _authorization = value; }
        }

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }


        public string RoleID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public SpdyMembershipUser(string providername,
                                  string username,
                                  object providerUserKey,
                                  string email,
                                  string passwordQuestion,
                                  string comment,
                                  bool isApproved,
                                  bool isLockedOut,
                                  DateTime creationDate,
                                  DateTime lastLoginDate,
                                  DateTime lastActivityDate,
                                  DateTime lastPasswordChangedDate,
                                  DateTime lastLockedOutDate,
                                  string RoleID,
                                 int UserID,
                                    Authorization SpdyAuthorization) :
                                  base(providername,
                                       username,
                                       providerUserKey,
                                       email,
                                       passwordQuestion,
                                       comment,
                                       isApproved,
                                       isLockedOut,
                                       creationDate,
                                       lastLoginDate,
                                       lastActivityDate,
                                       lastPasswordChangedDate,
                                       lastLockedOutDate)
        {
            this.RoleID = RoleID;
            this.UserID = UserID;
            this.SpdyAuthorization = SpdyAuthorization;
        }
    }
}