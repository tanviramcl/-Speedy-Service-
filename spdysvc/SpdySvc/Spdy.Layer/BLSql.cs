using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
/*
 * Call Simple Query From Here
 * */
namespace SpdySvc
{
    public class BLSql
    {
        /// <summary>
        /// Sql item user #rahul
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string Sql_User_Records()
        {
            String query = @"Select usr.*,ur.RoleID from users usr
                            Left Outer Join
                            UserRole ur
                            on usr.UserID=ur.UserID
                            ";

            return query;
           
            

        }

        public string Sql_Get_RootCategory()
        {

            String query = @"Select * from category where enabled=1 and status=1";
            return query;

        }

        public string Sql_Get_CategorybyRefKey(String RefKey)
        {

            String query = @"Select * from    Categorys where ReferenceKey='" + RefKey+"'";

            return query;          

        }
        public string Sql_Get_Category(String CatID)
        {

            String query = "Select * from    Categorys where CID=" + CatID;

            return query;

        }

        public string Sql_Get_Productby_CatId(int CatID)
        {

            String query = "Select * from    Items where CID=" + CatID;

            return query;

        }

        public string Sql_Get_All_Transection_Details_Redords(String TnType,String TnID)
        {

            String wherecondition = "";

            if(!TnType.Equals(""))
            {
                wherecondition += " WHERE tn.TnType="+TnType;
            }
            if (!TnID.Equals("")) 
            {
                if (!wherecondition.Equals(""))
                    wherecondition += " and ";
              else  wherecondition += " WHERE ";
                wherecondition+=" tn.IDN="+TnID;

            }
            String Query = @"  Select tnlt.*,tndt.ITEM_ID,tndt.OutQty,tndt.UnitQty,tndt.Unit,
                            p.Name,p.Cost,p.CID,c.CatName,u.Unit UnitName,tns.ApproveStatus,tns.ContractStatus,
                            tns.DeliverStatus,tns.OnHireStatus,CONVERT(VARCHAR(20), tns.EndOn, 101) EndOn,CONVERT(VARCHAR(20), tns.HireOn, 101) HireOn,
                            usr.FirstName+' '+usr.LastName RequesterName,rdt.LimitHireValue From
                            (Select tn.*,tntp.TypeName from Transection tn
                            Left Outer Join 
                            TransectionType tntp 
                            on tn.TnType=tntp.IDN  " + wherecondition +
                            @" ) tnlt 
                            Left Outer Join 
                            TransectionDetails tndt 
                            on tndt.TID=tnlt.IDN
                            Left Outer Join  
                            Items p
                            on tndt.ITEM_ID=p.IDN
                            Left Outer Join
                            Category c
                            on p.CID=c.IDN
                            Left Outer Join
                            Unit u
                            on tndt.Unit=u.IDN
                            Left Outer Join
                            TransectionStatus tns
                            on tns.TID=tnlt.IDN
							Left Outer Join
							Users usr
							on tnlt.REQUESTOR_ID=usr.UserID
	                        Left Outer Join
							RequestorDetails rdt
							on usr.UserID=rdt.UserID";
            return Query;

        }

        public string Sql_Get_All_Transection_Redords(String TnType, String TnID,String RequesterID)
        {

            String wherecondition = "";
            if (!TnType.Equals(""))
            {
                wherecondition += " WHERE tn.TnType="+TnType+" ";
            }
            if (!TnID.Equals(""))
            {
                if (!wherecondition.Equals(""))
                    wherecondition += " and ";
               wherecondition += " WHERE tn.IDN="+TnID+" ";

            }
            if(!RequesterID.Equals(""))
            {
                if (!wherecondition.Equals(""))
                    wherecondition += " and ";
                 wherecondition += " usr.UserID='" + RequesterID + "'";
            }

            String Query = @"Select tn.*,tdl.TOTAL_COST ,
                            tns.ApproveStatus,
                            tns.ContractStatus,tns.DeliverStatus,
                            tns.OnHireStatus,tns.EndOn DEndOn,
				            CONVERT(VARCHAR(20), tns.HireOn, 100) DHireOn,tntype.TypeName,tdl.TOTAL_ITEMS,
				            usr.FirstName+' '+usr.LastName Name,rdt.LimitHireValue 
                            from Transection tn
                            Left Outer Join
                            (Select SUM(CONVERT(int ,ISNULL(tndt.Cost,'0'))) TOTAL_COST,tndt.TID,COUNT(*) TOTAL_ITEMS  from TransectionDetails tndt
                            Group By tndt.TID) tdl
                            on tn.IDN=tdl.TID 
                            Left OUTER JOIN
                            TransectionStatus tns
                            on tn.IDN=tns.TID
				            Left Outer Join
				            Users usr
				            on usr.UserID=tn.REQUESTOR_ID
				            Left Outer Join
				            RequestorDetails rdt
				            on tn.REQUESTOR_ID=rdt.UserID
                            Left outer Join
                            TransectionType tntype
                            on tn.TnType=tntype.IDN" + wherecondition;
                            
            return Query;

        }

        public string Sql_Get_Productby_Id(int id)
        {
            String query = "Select * from    Items where IDN=" + id;

            return query;

        }
        //added by anis 
        //public string  Sql_Get_All_TransactionStatus_Record()
        //{
        //    String query = "SELECT [IDN],[TID],[ApproveStatus],[ContractStatus],[DeliverStatus],[OnHireStatus],[HireOn] ,[EndOn],[CreatedOn] FROM TransectionStatus";
            
        //    return query;
        //}
        //public string Sql_Get_All_USER_ROlES()
        //{
        //    string query = "SELECT * FROM Tbl_Role";

        //    return query;
        //}
    }
}
