using ProfessorCourse_BestFit.Helper;
using System.Web.Mvc;
using System.Web.Routing;
namespace ProfessorCourse_BestFit.CustomSecurity
{
    public class CustomAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public CustomAuthorization(string Role)
        {
            this.Role = Role;
        }

        public string Role { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var Roles = filterContext.HttpContext.User.Identity.Name;

                var dependent = Validator.IsDependent(Roles, Role);


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