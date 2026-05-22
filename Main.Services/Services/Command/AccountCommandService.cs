namespace Main.Services;

public class AccountCommandService
{
    private readonly IUserContext _userContext;

    public AccountCommandService ( IUserContext userContext )
    {
        _userContext = userContext;
    }

    public async Task<bool> CreateUserAccount ( string email )
    {
    
    }
}
