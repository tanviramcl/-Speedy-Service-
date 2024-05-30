using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpdySvc.Service.PriceService;
using SpdySvc.Provider;
using SpdySvc.Models;
using SpdySvc.Models.SpdyMembershipUser;
using System.Web.Security;
namespace ServiceReference
{
    public static class InjectedPriceService
    {
        public static PriceEnquiryDetailResponse GetRatesDetail(List<PriceHelper_Product> prods, bool bDelivery)
        {
           // String SiteStatus = Config.getString("SiteStatus");
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            if (usr != null)
            {
              
            }

            string accountId = "";

            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                //accountId = "";
            }
           


            List<RequestProductDto> prodList = new List<RequestProductDto>();

            foreach (PriceHelper_Product h in prods)
            {
                prodList.Add(new RequestProductDto() { ProductId = h.product_Code, Quantity = h.qty, FromDate = h.from_date, ToDate = h.to_date });
            }

            int? delPref;

            if (bDelivery)
            {
                delPref = 0;
            }
            else
            {
                delPref = null;
            }

            var request = new PriceEnquiryRequest
            {
                AccountId = accountId,
                AxaptaCompany = AxaptaCompanyType.SAS,
                Currency = CurrencyType.GBP,
                FromDate = DateTime.Today,
                Products = prodList.ToArray(),
                DeliveryPreference = delPref
            };

            try
            {
                using (var client = new PriceServiceClient())
                {
                    return client.GetHireRatesDetail(request);
                }
            }
            catch (Exception ex)
            {
                //Faker Result
                //Because
                Random rnd = new Random();
                PriceEnquiryDetailResponse mdl = new PriceEnquiryDetailResponse();
                mdl.AccountId = "2";
                mdl.AccountType = AccountType.Credit;
                mdl.Currency = CurrencyType.EUR;
                mdl.TotalBuyValue = rnd.Next(10, 1000);
                mdl.TotalBuyVat = rnd.Next(10, 1000);
                mdl.TotalHireCost = rnd.Next(10, 1000);
                mdl.TotalHireDeposit = rnd.Next(10,1000);
                mdl.TotalHireValue = rnd.Next(10, 1000);
                mdl.TotalHireVat = rnd.Next(10, 1000);
                mdl.TotalNetValue = rnd.Next(10, 1000);
                mdl.TotalPayable = rnd.Next(10, 1000);
                mdl.TotalVAT = rnd.Next(10, 1000);
                mdl.DeliveryCharge = rnd.Next(10, 1000);
                mdl.TotalSpeedyShield = rnd.Next(10, 1000);

                return mdl;
            }
        }

        public static PriceEnquiryDetailResponse GetRatesDetail(List<PriceHelper_Product> prods)
        {


            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            if (usr != null)
            {

            }

            string accountId = "";

            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                //accountId = "";
            }
           


            List<RequestProductDto> prodList = new List<RequestProductDto>();

            foreach (PriceHelper_Product h in prods)
            {
                prodList.Add(new RequestProductDto() { ProductId = h.product_Code, Quantity = h.qty, FromDate = h.from_date, ToDate = h.to_date });
            }

            var request = new PriceEnquiryRequest
            {
                AccountId = accountId,
                AxaptaCompany = AxaptaCompanyType.SAS,
                Currency = CurrencyType.GBP,
                FromDate = DateTime.Today,
                Products = prodList.ToArray()
            };

            try
            {
                using (var client = new PriceServiceClient())
                {
                    var response = client.GetHireRatesDetail(request);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ParsedPriceSummaryResponse> GetRatesSummary(List<PriceHelper_Product> prods)
        {
            List<ParsedPriceSummaryResponse> ret = new List<ParsedPriceSummaryResponse>();
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            if (usr != null)
            {

            }

            string accountId = "";

            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                //accountId = "";
            }
           

            

            List<RequestProductDto> prodList = new List<RequestProductDto>();

            foreach (PriceHelper_Product h in prods)
            {
                prodList.Add(new RequestProductDto() { ProductId = h.product_Code, Quantity = h.qty, FromDate = h.from_date, ToDate = h.to_date });
            }

            var request = new PriceEnquiryRequest
            {
                AccountId = accountId,
                AxaptaCompany = AxaptaCompanyType.SAS,
                Currency = CurrencyType.GBP,
                FromDate = DateTime.Today,
                Products = prodList.ToArray()
            };

            try
            {
                using (var client = new PriceServiceClient())
                {
                    return parseSummaryResponse(client.GetHireRatesSummary(request));
                }
            }
            catch (Exception ex)
            {
                ParsedPriceSummaryResponse ppsr = new ParsedPriceSummaryResponse();
                ret.Add(ppsr);
            }

            return ret;
        }

        public static List<ParsedPriceSummaryResponse> parseSummaryResponse(PriceEnquirySummaryResponse myResponse)
        {
            List<ParsedPriceSummaryResponse> response = new List<ParsedPriceSummaryResponse>();

            Dictionary<String, ParsedPriceSummaryResponse> responses = new Dictionary<String, ParsedPriceSummaryResponse>();

            try
            {
                foreach (SummaryProductDto respo in myResponse.Products)
                {
                    String productId = respo.ProductId.ToString();
                    if (!responses.ContainsKey(productId))
                    {
                        responses[productId] = new ParsedPriceSummaryResponse();
                        responses[productId].productId = productId;
                    }

                    if (respo.BuyPrice != null)
                    {
                        responses[productId].priceType = respo.BuyPrice.PriceType.ToString();
                        responses[productId].buyRate = respo.BuyPrice.Amount.ToString();
                    }
                    if (respo.DayRate != null)
                    {
                        responses[productId].dayType = respo.DayRate.PriceType.ToString();
                        responses[productId].priceType = respo.DayRate.PriceType.ToString();
                        responses[productId].dayRate = respo.DayRate.Amount.ToString();
                    }
                    if (respo.ExtraDay != null)
                    {
                        responses[productId].extraDayType = respo.ExtraDay.PriceType.ToString();
                        responses[productId].priceType = respo.ExtraDay.PriceType.ToString();
                        responses[productId].extraDayRate = respo.ExtraDay.Amount.ToString();
                    }
                    if (respo.WeekRate != null)
                    {
                        responses[productId].weekType = respo.WeekRate.PriceType.ToString();
                        responses[productId].priceType = respo.WeekRate.PriceType.ToString();
                        responses[productId].weekRate = respo.WeekRate.Amount.ToString();
                    }
                    if (respo.MinimumWeek != null)
                    {
                        responses[productId].minWeekType = respo.MinimumWeek.PriceType.ToString();
                        responses[productId].priceType = respo.MinimumWeek.PriceType.ToString();
                        responses[productId].minWeekRate = respo.MinimumWeek.Amount.ToString();
                    }

                }
            }
            catch
            {
            }

            foreach (KeyValuePair<String, ParsedPriceSummaryResponse> resp in responses)
            {
                response.Add(resp.Value);
            }

            return response;
        }

        public static Nullable<DateTime> DefaultOnHireDate()
        {
            return CalculateNextMonday();
        }

        public static Nullable<DateTime> DefaultOffHireDateFromDate(Nullable<DateTime> baseDate)
        {
            return ((DateTime)baseDate).AddDays(HireWeekDays());
        }

        public static Nullable<DateTime> DefaultOffHireDate()
        {
            return CalculateNextMonday().AddDays(HireWeekDays());
        }

        public static DateTime CalculateNextMonday()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            return today.AddDays(daysUntilMonday);
        }

        public static int HireWeekDays()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultHireDaysToAdd"]);
        }
    }
}