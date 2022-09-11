using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Messages
{
    public class Messages
    {
        public string message_null = "null";

        public string message_not_null = "notnull";

        public string message_success_submit_title = "Done...";

        public string message_success_submit_body = "The data has been saved successfully.";

        public string message_failed_submit_title = "Failed!";

        public string message_failed_submit_body = "A problem occurred, the data was not saved.";

        public string no_managers = "No manager has been appointed yet.";

        public string no_programs = "No programs has been appointed yet.";

        public string name_exist = "The name you entered is already exist please chose another name.";

        public string code_exist = "The code you entered is already exist please chose another one.";

        public string data_not_saved = "The data is not saved.";

        public string name_Required = "Please you should write name first.";
    }
}