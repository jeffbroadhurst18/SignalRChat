using QuickAPI.Controllers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalRChat
{
	public class Caller
	{
		static HttpClient client = new HttpClient();

		public static async Task<Score[]> GetScoreAsync()
		{
			//client.BaseAddress = new Uri("http://localhost/Quick");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			Score[] scores = null;
			HttpResponseMessage response = await client.GetAsync("http://localhost/Quick/api/values");

			if (response.IsSuccessStatusCode)
			{
				scores = await response.Content.ReadAsAsync<Score[]>();
			}
			return scores;
		}

		
	}
}
