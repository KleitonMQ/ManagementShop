using DonaMenina.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DonaMenina.Helper
{
    public class Session : IUserSession
    {
        private readonly IHttpContextAccessor _HttpContext;
        public Session(IHttpContextAccessor httpContext)
        {
            _HttpContext= httpContext;
        }
        public void BuildUserSession(Worker worker)
        {
            string value = JsonSerializer.Serialize(worker);
            _HttpContext.HttpContext.Session.SetString("sessionUser", value);
        }

        public void EndUserSession()
        {
            _HttpContext.HttpContext.Session.Remove("sessionUser");
        }

        public Worker GetWorkerSession()
        {
            string userSession = _HttpContext.HttpContext.Session.GetString("sessionUser");
            if (string.IsNullOrEmpty(userSession)) { return null; }
            return JsonSerializer.Deserialize<Worker>(userSession);
        }


    }
}
