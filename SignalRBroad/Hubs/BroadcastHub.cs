using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBroad.Hubs
{
	public class BroadcastHub : Hub
	{
		static HttpClient client = new HttpClient();

		public async Task<Score> SendScore(string scoreString)
		{
			Score deserializedScore = new Score();
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(scoreString));
			DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedScore.GetType());
			deserializedScore = ser.ReadObject(ms) as Score;
			ms.Close();

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));


			if (deserializedScore.Id == 0)
			{
				var buffer = System.Text.Encoding.UTF8.GetBytes(scoreString);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await client.PostAsync("http://localhost/Quick/api/values", byteContent);

				Score score = new Score();
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsAsync<Score>();

				}
			}
			else
			{
				var content = new FormUrlEncodedContent(new[]
				{
				 new KeyValuePair<string, string>("AwayName", deserializedScore.AwayName),
				 new KeyValuePair<string, string>("AwayScore", deserializedScore.AwayScore),
				 new KeyValuePair<string, string>("HomeName", deserializedScore.HomeName),
				 new KeyValuePair<string, string>("HomeScore", deserializedScore.HomeScore),
				 new KeyValuePair<string, string>("Id", deserializedScore.Id.ToString())
				});
				HttpResponseMessage response = await client.PutAsync("http://localhost/Quick/api/values/"  + deserializedScore.Id.ToString(), content);

				Score score = new Score();
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsAsync<Score>();

				}
			}
			return null;
		}
	}
}
