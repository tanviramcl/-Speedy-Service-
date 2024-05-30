using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using DBManager;
namespace SpdySvc
{
    public class Repository : IRepository
    {
        public string MESSAGE { get; set; }

        public string CODE { get; set; }

        public String NEXT_ID { get; set; }
        public void Save<T>(List<T> svLst)
        {
            var context = new DBMContext();
            using (context) 
            {
                context.SaveRecords<T>(svLst);
            }
            MESSAGE = context.Message;
        }

        public void Edit<T>(List<T> edtLst)
        {
            var context = new DBMContext();
            using ( context )
            {
                context.UpdateRecords<T>(edtLst);
               
            }
            MESSAGE = context.Message;
        }

        public void Delete<T>(List<T> delLst)
        {
            var context = new DBMContext();
            using ( context)
            {
                context.DeleteRecords<T>(delLst);
               
            }
            MESSAGE = context.Message;
        }


        public List<T> RetriveRecords<T>(String SqlQuery)
        {            
            using (var context = new DBMContext())
            {
              return  context.RetriveRecords<T>(SqlQuery);
            }
        }

        public List<T> RetriveRecords<T>(IDBMCommand dbmCommand)
        {
            using (var context = new DBMContext())
            {
                context.ExecuteSpCommand(dbmCommand);
                return context.RetriveRecords<T>();
            }
            throw new NotImplementedException();
        }
        public List<T> RetriveRecords<T>()
        {
            using (var context = new DBMContext())
            {
                return context.RetriveRecords<T>();
            }
        }


        public void NextIncrementID<T>()
        {
            using (var context = new DBMContext())
            {                
                NEXT_ID= context.NextIncrementID<T>();
            }
        }


        public void Save<T, U>(List<T> FirstList, List<U> SecondList)
        {
            var context = new DBMContext();
            using (context)
            {
                context.SaveRecords<T>(FirstList);
                context.SaveRecords<U>(SecondList);
            }
            MESSAGE = context.Message;
        }

        public void Save<T, U, V>(List<T> FirstList, List<U> SecondList, List<V> ThirdList)
        {
            var context = new DBMContext();
            using (context)
            {
                context.SaveRecords<T>(FirstList);
                context.SaveRecords<U>(SecondList);
                context.SaveRecords<V>(ThirdList);
            }
            MESSAGE = context.Message;
        }

        public void Save<T, U, V, W>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList)
        {
            var context = new DBMContext();
            using (context)
            {
                context.SaveRecords<T>(FirstList);
                context.SaveRecords<U>(SecondList);
                context.SaveRecords<V>(ThirdList);
                context.SaveRecords<W>(ForthList);
            }
            MESSAGE = context.Message;
        }

        public void Save<T, U, V, W, X>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList, List<X> FifthList)
        {
            var context = new DBMContext();
            using (context)
            {
                context.SaveRecords<T>(FirstList);
                context.SaveRecords<U>(SecondList);
                context.SaveRecords<V>(ThirdList);
                context.SaveRecords<W>(ForthList);
                context.SaveRecords<X>(FifthList);
            }
            MESSAGE = context.Message;
        }




       
    }
}
