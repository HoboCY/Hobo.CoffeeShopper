using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Services;
using IdentityModel.Client;


namespace Client.Pages
{
	public partial class CoffeeShops
	{
		private List<CoffeeShopDto> Shops = new();

		[Inject] private HttpClient HttpClient { get; set; }

		[Inject] private IConfiguration Config { get; set; }

		[Inject] private ITokenService TokenService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var tokenResponse = await TokenService.GetTokenAsync("CoffeeAPI.read");

			HttpClient.SetBearerToken(tokenResponse.AccessToken);
			var result = await HttpClient.GetAsync(Config["ApiUrl"] + "/api/CoffeeShop");

			if (result.IsSuccessStatusCode)
			{
				Shops = await result.Content.ReadFromJsonAsync<List<CoffeeShopDto>>();
			}
		}
	}
}
