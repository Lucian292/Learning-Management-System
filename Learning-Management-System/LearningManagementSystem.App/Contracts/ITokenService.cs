namespace LearningManagementSystem.App.Contracts
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
        Task SetTokenAsync(string token);
    }
}
