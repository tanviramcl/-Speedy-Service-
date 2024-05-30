using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using DBManager;
using SpdySvc.Models;
using SpdySvc.Service.PriceService;
namespace SpdySvc
{
    public interface IFactory
    {
        String MESSAGE { get; set; }
        String CODE { get; set; }
        String NEXT_ID { get; set; }
        PriceEnquiryDetailResponse PRICE_ENQUIRY_DETAIL_RESPONSE { get; set; }
        IRepository repoSave<T>(List<T> svLst);
        IRepository repoEdit<T>(List<T> svLst);
        IRepository repoDelete<T>(List<T> svLst);
        IRepository repoSave<T, U>(List<T> FirstLst, List<U> SecondList);
        IRepository repoSave<T, U, V>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList);
        IRepository repoSave<T, U, V, W>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList, List<W> ForthList);
        IRepository repoSave<T, U, V, W, X>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList, List<W> ForthList, List<X> FifthList);
        void CreateNewTransection<T, U>(List<T> FirstList, List<U> SecondList);
        void UpdateTransection<T, U>(List<T> FirstList, List<U> SecondList);
        IRepository NextIncrementID<T>();
        List<T> repoGetRecords<T>();
        List<T> repoGetRecords<T>(String SqlQuery);
        List<T> repoGetRecords<T>(IDBMCommand dbmCommand);
        List<Users> SPItemUser(String UserID);
        List<Users> SQlItemUser(String UserID);
        void ExecuteNonQuery(String Query); 
        void Newuser(List<User> uList);
        void ExecuteCommand(IDBMCommand command);
        void SendEmail(EMAIL emailMdl);
        void AssignUserPermission(User aUser, Authorization.UserRoles aUserRole);
        void GetRateDetails(List<PriceHelper_Product> prods, bool bDelivery);
        bool CommitToServiceLayer(List<Order> orderLst, List<OrderItem> orderItem, List<OrderItemExtra> extra);
        void CreateNewOrder(List<Order> order, List<OrderItem> orderItem, List<OrderItemExtra> extra);
        
    }
}
