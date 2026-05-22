namespace Main.Services
{
    public interface IAccountCommandService
    {
        Task<bool> CreateUserAccount(string email);
    }
}
