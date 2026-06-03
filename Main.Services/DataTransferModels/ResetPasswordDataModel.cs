using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DataTransferModel;

public class ResetPasswordDataModel
{
    public ResetPasswordDataModel ( )                          
    {
    }


    public ResetPasswordDataModel ( string email, string token )
    {
        Email = email;
        Token = token;
    }

    public string UserName
    {
        get; set;
    }

    public string Email { get; set; }


    public string Subject
    {
        get; set;
    }   


    public string Token { get; set; }


    public string Message { get; set; }


    public string LinkUrl
    {
        get;
        set;
    }
}
