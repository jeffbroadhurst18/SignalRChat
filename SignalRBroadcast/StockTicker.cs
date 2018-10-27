using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRBroadcast
{
	public class ScoreTicker
	{
		
		private readonly ConcurrentDictionary<string, Score> _stocks = new ConcurrentDictionary<string, Score>();

		private readonly object _updateScoresLock = new object();

		//stock can go up or down by a percentage of this factor on each change
		private readonly double _rangePercent = .002;

		private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
		private readonly Random _updateOrNotRandom = new Random();

		private readonly Timer _timer;
		private volatile bool _updatingScore = false;

		private ScoreTicker(IClientProxy clients)
		{
			

			_timer = new Timer(UpdateScores, null, _updateInterval, _updateInterval);

		}

		public static ScoreTicker Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		private IClientProxy Clients
		{
			get;
			set;
		}

		private void UpdateScores(object state)
		{
			lock (_updateScoresLock)
			{
				if (!_updatingScore)
				{
					_updatingScore = true;

					foreach (var stock in _stocks.Values)
					{
						if (TryUpdateStockPrice(score))
						{
							BroadcastStockPrice(score);
						}
					}

					_updatingScore = false;
				}
			}
		}

		private bool TryUpdateStockPrice(Score stock)
		{
			// Randomly choose whether to update this stock or not
			var r = _updateOrNotRandom.NextDouble();
			if (r > .1)
			{
				return false;
			}

			// Update the stock price by a random factor of the range percent
			var random = new Random((int)Math.Floor(stock.Price));
			var percentChange = random.NextDouble() * _rangePercent;
			var pos = random.NextDouble() > .51;
			var change = Math.Round(stock.Price * (decimal)percentChange, 2);
			change = pos ? change : -change;

			stock.Price += change;
			return true;
		}

		private void BroadcastStockPrice(Score score)
		{
			Clients.
		}

	}
}
	}
}
