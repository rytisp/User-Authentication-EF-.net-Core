using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IFW.Pages
{
    [Authorize(Policy = "HRpolicy")]
    public class HrModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
