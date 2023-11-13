using IdentityModel.Client;

namespace Client.Services
{
	public interface ITokenService
	{
		Task<TokenResponse> GetTokenAsync(string scope);
	}
}
