using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.SqlClient;
using DBManager;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SpdySvc.Models;
using SpdySvc.Utility;
using System.Configuration;
/*
 * Call Any Stored Prosedure From Here
 * */
namespace SpdySvc
{
    public class BLSp : IDisposable
    {
        private IDataManager dbManager;
        private IDBMCommand dbmCommand;
        public String UserID { get; set; }
        public String OrderID { get; set; }

        public BLSp()
        {
            dbmCommand = new DBMCommand();
        }

        public IDBMCommand SP_Get_All_Transection_Details_Redords(String TypeID, String Idn)
        {
            try
            {

                dbmCommand.CommandName = StoredProcedure.SP_Get_All_Transection_Details_Redords;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@TnType";
                prm.Value = TypeID;

                SqlParameter prm2 = new SqlParameter();
                prm2.DbType = DbType.String;
                prm2.ParameterName = "@TnID";
                prm2.Value = Idn;


                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
                dbmCommand.paramLst.Add(prm2);

            }
            catch (Exception ex)
            {


            }
            return dbmCommand;

        }
        //added by anis SP_Get_All_Order_Details_Records
        //public IDBMCommand SP_Get_All_Transaction_Record()
        //{
        //    try
        //    {

        //        dbmCommand.CommandName = StoredProcedure.SP_Get_All_Transaction_Record;          

        //        dbmCommand.paramLst = new List<SqlParameter>();




        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return dbmCommand;

        //}
        //16/04/2015
        public IDBMCommand SP_Get_All_Order_Records(String OrderId = null, String UserId = null)
        {
            try
            {

                dbmCommand.CommandName = StoredProcedure.SP_Get_All_Order_Records;

                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@OrderId";
                if (OrderId != null)
                    prm.Value = OrderId;
                else
                    prm.Value = "";

                SqlParameter prm1 = new SqlParameter();
                prm1.DbType = DbType.String;
                prm1.ParameterName = "@UserId";
                if (UserId != null)
                    prm1.Value = UserId;
                else
                    prm1.Value = "";

                dbmCommand.paramLst = new List<SqlParameter>();

                dbmCommand.paramLst.Add(prm);
                dbmCommand.paramLst.Add(prm1);


            }
            catch (Exception ex)
            {


            }
            return dbmCommand;

        }

        public IDBMCommand SP_Get_All_Order_Details_Records(String OrderID = null, String CustomerID = null, String OrderStatusID = null, String UserId = null)
        {
            try
            {

                dbmCommand.CommandName = StoredProcedure.SP_Get_All_Order_Details_Records;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@OrderID";
                if (OrderID != null)
                    prm.Value = OrderID;
                else
                    prm.Value = "";

                SqlParameter prm2 = new SqlParameter();
                prm2.DbType = DbType.String;
                prm2.ParameterName = "@CustomerID";
                if (CustomerID != null)
                    prm2.Value = CustomerID;
                else
                    prm2.Value = null;


                SqlParameter prm3 = new SqlParameter();
                prm3.DbType = DbType.String;
                prm3.ParameterName = "@OrderStatusID";
                if (UserId != null)
                    prm3.Value = OrderStatusID;
                else
                    prm3.Value = "";

                SqlParameter prm4 = new SqlParameter();
                prm4.DbType = DbType.String;
                prm4.ParameterName = "@UserId";
                if (UserId != null)
                    prm4.Value = UserId;
                else
                    prm4.Value = "";

                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
                dbmCommand.paramLst.Add(prm2);
                dbmCommand.paramLst.Add(prm3);
                dbmCommand.paramLst.Add(prm4);

            }
            catch (Exception ex)
            {


            }
            return dbmCommand;

        }

        public IDBMCommand SP_Get_OffHire_Order_Details_Records(DateTime offHireDate)
        {
            try
            {

                dbmCommand.CommandName = StoredProcedure.SP_Get_OffHire_Order_Details_Records;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@offHireDate";
                prm.Value = offHireDate.ToShortDateString();
                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
            }
            catch (Exception ex)
            {


            }
            return dbmCommand;

        }
        public void Dispose()
        {
            dbManager.Dispose();
        }

        //added by anis 21/04/2015 SP_GetUserPermissionDetails
        public IDBMCommand SP_GetUserPermissionDetails(String UserID)
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_GetUserPermissionDetails;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@UserID";
                prm.Value = UserID;





                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);



            }
            catch
            {


            }

            return dbmCommand;
        }

        public IDBMCommand SP_AssignUserRole(String UserID, String RoleID)
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_AssignUserRole;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@userid";
                prm.Value = UserID;

                SqlParameter prm1 = new SqlParameter();
                prm1.DbType = DbType.String;
                prm1.ParameterName = "@roleid";
                prm1.Value = RoleID;



                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
                dbmCommand.paramLst.Add(prm1);


            }
            catch
            {


            }

            return dbmCommand;
        }

        public IDBMCommand SP_GetAllUserRecords()
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_GetAllUserRecords;

                dbmCommand.paramLst = new List<SqlParameter>();
            }
            catch
            {


            }

            return dbmCommand;
        }


        //end

        public IDBMCommand SP_Get_Token_Records(String TokenID = null)
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_Get_Token_Records;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@TokenID";
                if (TokenID != null)
                    prm.Value = TokenID;
                else
                    prm.Value = "";



                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);



            }
            catch (Exception ex)
            {


            }
            return dbmCommand;

   }
        public IDBMCommand SP_Get_Order_Records_byCustomerId(String CustomerOrderNumber = null)
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_Get_Order_Records_byCustomerId;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@CustomerOrderNumber";
                if (CustomerOrderNumber != null)
                    prm.Value = CustomerOrderNumber;
                else
                    prm.Value = "";



                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);



            }
            catch (Exception ex)
            {
            }

            return dbmCommand;
        }

        public IDBMCommand SP_Get_All_Categorys(String CatId="", String ParentId="")
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_Get_All_Categorys;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@catId";
                prm.Value = CatId;

                SqlParameter prm2 = new SqlParameter();
                prm2.DbType = DbType.String;
                prm2.ParameterName = "@catParentId";
                prm2.Value = ParentId;

                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
                dbmCommand.paramLst.Add(prm2);

            }
            catch (Exception ex)
            {
            }
            return dbmCommand;
        }

        public IDBMCommand SP_Get_Category_Products(String CatId="")
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_Get_Category_Products;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@catId";
                prm.Value = CatId;

                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
            }
            catch (Exception ex)
            {
            }
            return dbmCommand;
        }
        public IDBMCommand SP_Get_ProductDetails(String ProductCode = "")
        {
            try
            {
                dbmCommand.CommandName = StoredProcedure.SP_Get_ProductDetails;
                SqlParameter prm = new SqlParameter();
                prm.DbType = DbType.String;
                prm.ParameterName = "@productCode";
                prm.Value = ProductCode;

                dbmCommand.paramLst = new List<SqlParameter>();
                dbmCommand.paramLst.Add(prm);
            }
            catch (Exception ex)
            {
            }
            return dbmCommand;
        }

        
        


    }
}
