using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpdySvc.Service.HireService;
using SpdySvc.Models;
using SpdySvc.Provider;
using SpdySvc.Models.SpdyMembershipUser;
using System.Web.Security;
using SpdySvc.SingleTon;
namespace ServiceReference
{
    public class InjectedHireReference
    {
        public bool CommitToServiceLayer( List<Order> orderLst,List<OrderItem> OrderItems, List<OrderItemExtra> extras)
        {
            var client = new HireServiceClient();

            //create list of products
            List<RequestProductDto> listOfProducts = new List<RequestProductDto>();
            HireLineType productIndicator = HireLineType.SingleLine;

            foreach (OrderItem o in OrderItems)
            {
                bool isHireItem = ((o.HireBuy != null) && (o.HireBuy.ToLower() == "hire"));
                // Find the order item extras for this product


                productIndicator = (extras.Count() > 0) ? HireLineType.MainKitLine : HireLineType.SingleLine;
                RequestProductDto prod = new RequestProductDto
                {
                    ProductId = o.ProductCode,
                    ProductIndicator = productIndicator,
                    FromDate = isHireItem ? o.FromDate : DateTime.Now,
                    ToDate = isHireItem ? o.ToDate : DateTime.Now,
                    Quantity = o.Quantity
                };
                listOfProducts.Add(prod);

                // Add the order item extras for this product.
                foreach (OrderItemExtra ex in extras)
                {
                    if (ex.Quantity > 0)
                    {
                        RequestProductDto prodex = new RequestProductDto
                        {
                            ProductId = ex.ProductCode,
                            ProductIndicator = (HireLineType)ex.ProductIndicator,
                            FromDate = isHireItem ? ex.FromDate : DateTime.Now,
                            ToDate = isHireItem ? ex.ToDate : DateTime.Now,
                            Quantity = ex.Quantity
                        };
                        listOfProducts.Add(prodex);
                    }
                }
            }

            PreHireRequest request = new PreHireRequest();
            request.WebOrderNumber = orderLst[0].OrderId.ToString();
            request.CustomerOrderNumber = orderLst[0].CustomerOrderNumber;
            request.Notes = orderLst[0].Note;
            if (orderLst[0].OnHireDate != null)
            {
                request.OnHireDate = orderLst[0].OnHireDate;
            }
            if (orderLst[0].OffHireDate != null)
            {
                request.ExpectedOffHireDate = orderLst[0].OffHireDate;
            }
            request.Products = listOfProducts.ToArray();

            //pageObj.writefile("OrderObject : Setting Card Charges");
            //this needs finalizing once we've got the payment layer installed
            //CardChargesDto charges = new CardChargesDto
            //{
            //    AmountCharged = this.AmountCharged,
            //    ApprovalCode = this.ApprovalCode,
            //    ApprovalDate = DateTime.Now
            //};

            //request.CardCharges = charges;



            //pageObj.writefile("OrderObject : Setting Delivery Info");
            //recipient
            PersonDto person_Deliver = new PersonDto
            {
                Address = new AddressDto
                {
                    AddressLine1 = orderLst[0].Address1,
                    AddressLine2 = orderLst[0].Address2,
                    City = orderLst[0].City,
                    County = orderLst[0].County,
                    Postcode = orderLst[0].Postcode,
                    //Country = this.CountryID.ToString()
                    Country = "UK" //hardcoded to UK.
                },

                ContactDetails = new ContactDetailsDto
                {
                    EmailAddress = orderLst[0].Email,
                    FaxNumber = orderLst[0].FaxNumber,
                    MobileNumber = orderLst[0].MobileNumber,
                    TelephoneNumber = orderLst[0].ContactPhoneNumber
                },

                Title = "",
                FirstName = orderLst[0].ContactName
            };

            ///GET CURRENT USER RECORDS
            SpdyMembershipUser crntUsr=(SpdyMembershipUser)Membership.GetUser();
            User uo=SingleTon.Instance().USER_LIST.Find(itm=>itm.Email.Equals(crntUsr.Email));

            string user_title = (String.IsNullOrEmpty(uo.Title) ? "" : uo.Title.ToString());
            string firstname_order = (uo.FirstName ?? "").ToString();
            string surname_order = (uo.LastName ?? "").ToString();

            //orderedby
            PersonDto person_order = new PersonDto
            {
                Address = new AddressDto
                {
                    AddressLine1 = uo.Address1,
                    AddressLine2 = uo.Address2,
                    City = uo.City,
                    County = uo.County,
                    Postcode = "",
                    //Country = uo.country_id.ToString()
                    Country = "UK" //hardcoded to UK
                },

                ContactDetails = new ContactDetailsDto
                {
                    EmailAddress = uo.Email,
                    FaxNumber = uo.FaxNumber,
                    MobileNumber = uo.MobileNumber,
                    TelephoneNumber = uo.PhoneNumber.ToString()
                },

                Title = user_title,
                FirstName = firstname_order,
                LastName = surname_order,
            };

            //pageObj.writefile("OrderObject : Setting Request Details");
            request.OrderedBy = person_order;
            request.DeliverTo = person_Deliver;

           // request.AccountId = uo.getAccountNumFromAccountID(orderLst[0].CustomerAccountID);

            //pageObj.writefile("OrderObject : Setting Delivery Charge");
            request.DepotDeliveryCharge = "0"; // this.DeliveryCharge.ToString();
            request.RetailDeliveryCharge = orderLst[0].DeliveryCharge.ToString();

            //pageObj.writefile("OrderObject : Making Service Layer Request");
            BaseResponse result = client.PlacePrehireRequest(request);

            //pageObj.writefile("OrderObject : Service Layer ErrorCode " + result.Error.ErrorCode);
            if (result.Error.ErrorCode == 0)
            {
                return true;
            }
            else
            {
                //log.ErrorFormat("Fabric Response : {0} - {1}", result.Error.ErrorCode, result.Error.ErrorText);
                return false;
            }
        }
    }
}