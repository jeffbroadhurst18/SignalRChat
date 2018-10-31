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
				var buffer = Encoding.UTF8.GetBytes(scoreString);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await client.PostAsync("http://localhost/Quick/api/values", byteContent);

				if (response.IsSuccessStatusCode)
				{
					Score createdScore = await response.Content.ReadAsAsync<Score>();
					return createdScore;
				}
			}
			else
			{
				var buffer = Encoding.UTF8.GetBytes(scoreString);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				HttpResponseMessage response = await client.PutAsync("http://localhost/Quick/api/values/" + deserializedScore.Id.ToString(), byteContent);

				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsAsync<Score>();
				}
			}
			return null;
		}

		public async void ClearScores()
		{
			HttpResponseMessage response = await client.DeleteAsync("http://localhost/Quick/api/values");
		}
	}
}