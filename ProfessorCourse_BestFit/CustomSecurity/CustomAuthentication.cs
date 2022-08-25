using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace ProfessorCourse_BestFit.CustomSecurity
{
    public class CustomAuthentication : FilterAttribute, IAuthenticationFilter
    {
        // executes before action method
        public void OnAuthentication(AuthenticationContext filterContext)
        {

            // check if user is authenticated or not
            // authenticated => true !true == false > if will not work
            // notAuthenticated => false !false == true > if will work

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // check if result is null or not authorized will redirect to login page
            // !authenticated => !authorized (always true)
            // !authorized => !authenticated (depends on permissions / not always true)

            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    }));
            }
        }
    }
}