using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SpdySvc.Models;
using SpdySvc.SingleTon;
using SpdySvc.Utility;
using SpdySvc.Models.SpdyMembershipUser;
namespace SpdySvc.Provider
{
    public class SqlServerMemberShipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();

        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (username == "")
                return null;
            User usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.Email==username);
            SingleTon.SingleTon.Instance().GetUserPermission(username);

            return new SpdyMembershipUser("SqlMemberShipProvider", usr.UserID.ToString(), null, usr.Email, "", "", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,(usr.Admin==true?"1":"3"),Convert.ToInt32(usr.UserID),SingleTon.SingleTon.Instance().UserAuthorization);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            foreach (User usr in SingleTon.SingleTon.Instance().USER_LIST)
            {
              String pwd = new Utility.Utility().EncryptPassword(password, usr.Token.ToString().Trim());
                if (SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Email ==username && itm.Password == pwd).Count > 0)
                {
                    //Audit Trail
                    List<Audit_trail> aLst = new List<Audit_trail>();
                    aLst.Add(new Audit_trail {action="Login Successfully User:"+usr.Email,script="../Acotion/Login",date_time=DateTime.Now,user=usr.Email});
                    FactoryPush fac = new FactoryPush();
                    fac.SaveList<Audit_trail>(aLst);

                    FormsAuthentication.SetAuthCookie(username, false);
                    
                    return true;
                }
            }

           
            return false;
         
        }

        public void LogOff() 
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            //Audit Trail
            List<Audit_trail> aLst = new List<Audit_trail>();
            aLst.Add(new Audit_trail { action = "Logout Successfully User:" + usr.Email, script = "../Acotion/Login", date_time = DateTime.Now,  user = usr.Email });
            FactoryPush fac = new FactoryPush();
            fac.SaveList<Audit_trail>(aLst);
            FormsAuthentication.SignOut();
        }

      
    }
}