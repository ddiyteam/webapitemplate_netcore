using System.Threading.Tasks;

namespace $ext_safeprojectname$.BLL.Contracts
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken();
        Task<bool> ValidateToken(string token);
    }
}
