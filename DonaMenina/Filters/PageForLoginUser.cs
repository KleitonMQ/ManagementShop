using DonaMenina.Entities;
using DonaMenina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace DonaMenina.Filters
{
    public class PageForLoginUser : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string userSession = context.HttpContext.Session.GetString("sessionUser");

            if (string.IsNullOrEmpty(userSession) ){
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "WorkSpace" }, { "action", "Index" } });
            }
            else
            {
                WorkSpaceModel model = new WorkSpaceModel();
                model.Worker = JsonSerializer.Deserialize<Worker>(userSession);
                if (model.Worker == null )
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "WorkSpace" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
