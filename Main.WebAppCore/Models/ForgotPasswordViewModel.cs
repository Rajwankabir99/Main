using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class ForgotPasswordViewModel : BaseViewModel
{
    public ForgotPasswordViewModel() {
        PageName = "Forgot Password";
    }

    [Required ( ErrorMessage = "Email address is required" )]
    [EmailAddress ( ErrorMessage = "Invalid email format" )]
    public string Email { get; set; } = string.Empty;
}

