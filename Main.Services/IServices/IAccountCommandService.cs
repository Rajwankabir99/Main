namespace Main.Services;

public interface IAccountCommandService
{
    Task<bool> CreateUserAccount ( 
        UserAccountDataModel userAccountDM )

    Task<int> GetSingleUser ( string id );
}
