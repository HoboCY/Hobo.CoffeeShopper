using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Client.Services
{
	public class TokenService : ITokenService
	{
		private readonly IdentityServerSettings _settings;
		private readonly DiscoveryDocumentResponse _discoveryDocument;
		private readonly HttpClient _httpClient;

		public TokenService(IOptions<IdentityServerSettings> settings)
		{
			_settings = settings.Value;
			_httpClient = new HttpClient();
			_discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_settings.DiscoveryUrl).Result;

			if (_discoveryDocument.IsError)
			{
				throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
			}
		}

		public async Task<TokenResponse> GetTokenAsync(string scope)
		{
			var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = _discoveryDocument.TokenEndpoint,
				ClientId = _settings.ClientName,
				ClientSecret = _settings.ClientPassword,
				Scope = scope
			});

			if (tokenResponse.IsError)
			{
				throw new Exception("Unable to get token", tokenResponse.Exception);
			}

			return tokenResponse;
		}
	}
}
