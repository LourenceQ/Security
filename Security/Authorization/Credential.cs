using System.ComponentModel.DataAnnotations;

namespace Security.Authorization
{
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Rmember Me")]
        public bool RememberMe { get; set; }
    }
}
