using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalRBroadcast.Hubs;

namespace MyBackgroundService
{
	public class JeffBackgroundService : BackgroundService
	{
		private readonly IHubContext<BroadcastHub> _hubContext;
		public JeffBackgroundService(IHubContext<BroadcastHub> hubContext)
		{
			_hubContext = hubContext;
		}
		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await _hubContext.Clients.All.SendAsync("BroadcastMessage", "server", "123");

				await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
			}
		}
	}
}