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
    public interface IRepository
    {
        String MESSAGE { get; set; }
        String CODE { get; set; }
        String NEXT_ID { get; set; }
        void Save<T>(List<T> svLst);
        void Save<T,U>(List<T> FirstList,List<U> SecondList);
        void Save<T, U, V>(List<T> FirstList, List<U> SecondList, List<V> ThirdList);
        void Save<T, U, V, W>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList);
        void Save<T, U, V, W, X>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList, List<X> FifthList);
        void Edit<T>(List<T> edtLst);
        void Delete<T>(List<T> delLst);

        void NextIncrementID<T>(); 
        List<T> RetriveRecords<T>();
        List<T> RetriveRecords<T>(String SqlQuery);
        List<T> RetriveRecords<T>(IDBMCommand dbmCommand);
    }
}
