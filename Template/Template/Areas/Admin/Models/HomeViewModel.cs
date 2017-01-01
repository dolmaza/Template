namespace Template.Admin.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        public string LoginUrl { get; set; }
        public string RedirectUrl { get; set; }

    }
}