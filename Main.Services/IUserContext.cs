namespace Main.Services;

public interface IUserContext
{
    string UserId
    {
        get;
    }
    string IdentityId
    {
        get;
    }
    string Company
    {
        get;
    }
    string Currency
    {
        get;
    }
    string Country
    {
        get;
    }
    
    string AppEnvironment
    {
        get;
    }
    
    DateTime GetLocalNow ( );
}
