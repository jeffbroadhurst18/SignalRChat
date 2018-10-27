using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalRBroad.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRBroad
{

	public class JeffBackgroundService : BackgroundService
	{
		private readonly IHubContext<BroadcastHub> _hubContext;
		static HttpClient client = new HttpClient();

		public JeffBackgroundService(IHubContext<BroadcastHub> hubContext)
		{
			_hubContext = hubContext;
		}
		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var scores = await GetScoreAsync();
				var score = scores[0];
				var scoreString = string.Format("{0} {1} - {2} {3}", score.HomeName, score.HomeScore, score.AwayName, score.AwayScore);

				await _hubContext.Clients.All.SendAsync("BroadcastMessage", scoreString);

				await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
			}
		}

		private static async Task<Score[]> GetScoreAsync()
		{
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
