using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class User : PresentationModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool UserAgreement { get; set; }
    }
}