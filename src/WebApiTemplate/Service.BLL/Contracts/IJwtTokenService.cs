using System.Threading.Tasks;

namespace Service.BLL.Contracts
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken();
        Task<bool> ValidateToken(string token);
    }
}
