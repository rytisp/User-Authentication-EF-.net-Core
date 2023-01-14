using IFW.SQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using IFW.Models;
using System.Security.Claims;

namespace IFW.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public required Credential Credential { get; set; }
        public void OnGet()
        {
            // this.Credential = new Credential { UserName = "rytis", Password = "" };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

                var userData = new MySQLDataQueries();
                var userObj = JObject.Parse(userData.GetUserDetails(Credential.UserName, Credential.Password));
                var sqlUserName = Convert.ToString(userObj["UserName"]);
                var sqlPassword = Convert.ToString(userObj["Password"]);


            TempData["mysql"] = sqlUserName + " " + sqlPassword;

            //Verify the credential
            if (Credential.UserName == sqlUserName && Credential.Password == sqlPassword)
                {
                var sqlEmail = Convert.ToString(userObj["Email"]);
                var sqlDepartmentName = Convert.ToString(userObj["DepartmentName"]);
                var sqlIsAdmin = Convert.ToString(userObj["IsAdmin"]);
                var sqlIsManager = Convert.ToString(userObj["IsManager"]);
                var sqlEmploymentDate = Convert.ToString(userObj["EmploymentDate"]);

                //Creating the security context
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, sqlUserName),
                new Claim(ClaimTypes.Email, sqlEmail),
                new Claim("Department", sqlDepartmentName),
                new Claim(sqlIsAdmin, "true"),
                new Claim(sqlIsManager, "true"),
                new Claim("EmploymentDate", sqlEmploymentDate)
                };

                var identity = new ClaimsIdentity(claims, "RytisCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                await HttpContext.SignInAsync("RytisCookie", claimsPrincipal, authProperties);

                return RedirectToPage("/Index");
                }

            return Page();
        }
    }


}
