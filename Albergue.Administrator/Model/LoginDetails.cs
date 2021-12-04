using System.ComponentModel.DataAnnotations;

namespace Albergue.Administrator.Model
{
    public class LoginDetails
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
