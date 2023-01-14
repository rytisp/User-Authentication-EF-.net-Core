using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IFW.Pages
{
    [Authorize(Policy = "HRmanagerPolicy")]
    public class HRmanagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
