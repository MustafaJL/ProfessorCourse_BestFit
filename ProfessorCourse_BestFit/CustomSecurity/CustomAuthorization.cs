using ProfessorCourse_BestFit.Helper;
using System.Web.Mvc;
using System.Web.Routing;
namespace ProfessorCourse_BestFit.CustomSecurity
{
    public class CustomAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public CustomAuthorization(string Permissions)
        {
            this.Permissions = Permissions;
        }

        public string Permissions { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var permissions = filterContext.HttpContext.User.Identity.Name;

                var dependent = Validator.IsDependent(permissions, Permissions);


                if (!(dependent))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "UnAuthorized"
                    }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
        }


    }
}