using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IFW.Models
{
    public class Credential
    {
        [Display(Name = "User Name")]
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
