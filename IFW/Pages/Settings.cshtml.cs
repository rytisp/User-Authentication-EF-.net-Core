using IFW.Models;
using IFW.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IFW.Pages
{
    [Authorize(Policy = "AdminPolicy")]
    public class SettingsModel : PageModel
    {
        [BindProperty]
        public required NewUserObject NewUser { get; set; }
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            var insertUser = new MySQLDataQueries();
            insertUser.CreateNewUser(NewUser.UserName, NewUser.Password, NewUser.FirstName, NewUser.Surname,
                 NewUser.Email, NewUser.DepartmentName, NewUser.IsManager, NewUser.IsAdmin, NewUser.EmploymentDate);
            TempData["UserAdded"] = "User " + NewUser.UserName + " has been created";
        }
    }
}
