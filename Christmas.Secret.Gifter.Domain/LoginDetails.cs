using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.API.Model
{
    [ExportTsInterface]
    public class LoginDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}