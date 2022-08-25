using System.Linq;

namespace ProfessorCourse_BestFit.Helper
{
    public class Validator
    {
        public static bool IsDependent(string Roles, string UserRoles)
        {

            string[] array1 = new[] { Roles };
            array1 = Roles.Split(',');

            string[] array2 = new[] { UserRoles };
            array2 = UserRoles.Split(',');


            var intersect = array1.Intersect(array2).Any();

            return intersect;

        }
    }
}