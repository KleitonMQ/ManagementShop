using DonaMenina.Entities;
using DonaMenina.Models;
using System.Text.Json;

namespace DonaMenina.Helper
{
    public class SaleSession : ISaleSession
    {
        private readonly IHttpContextAccessor _HttpContext;
        public SaleSession(IHttpContextAccessor httpContext)
        {
            _HttpContext = httpContext;
        }
        public void BuildSaleSession(WorkSpaceModel sale)
        {
            string value = JsonSerializer.Serialize(sale);
            _HttpContext.HttpContext.Session.SetString("sessionSale", value);
        }

        public void EndSaleSession()
        {
            _HttpContext.HttpContext.Session.Remove("sessionSale");
        }

        public WorkSpaceModel GetSaleSession()
        {
            string saleSession = _HttpContext.HttpContext.Session.GetString("sessionSale");
            if (string.IsNullOrEmpty(saleSession)) { return null; }
            return JsonSerializer.Deserialize<WorkSpaceModel>(saleSession);
        }
    }
}
