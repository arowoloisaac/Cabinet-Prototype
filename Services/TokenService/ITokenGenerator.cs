using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.TokenService
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
