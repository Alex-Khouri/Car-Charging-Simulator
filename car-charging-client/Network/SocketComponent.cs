using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Client
{
	using static Globals;

	namespace Network
	{
		public class SocketComponent: ComponentBase
		{
			[Inject]
			public HttpClient HttpClient { get; set; }

			public SocketComponent()
			{
				HttpClient = new HttpClient();
			}

			public async Task<HttpResponseMessage> SendMessageToHub(string deviceId, string deviceValue)
			{
				var jsonContent = JsonSerializer.Serialize(deviceValue);
				var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

				string baseURI = $"{IOT_HUB_NAME}.azure-devices.net/devices/{deviceId}";
				string encodedURI = HttpUtility.UrlEncode(baseURI.ToLower());
				LOG($"encodedURI: {encodedURI}");
				long expiry = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + SAS_TOKEN_LIFETIME;

				string stringToSign = $"{baseURI.ToLower()}\n{expiry}";
				byte[] keyBytes = Convert.FromBase64String(SECURE_ACCESS_STRING);
				using var hmac = new HMACSHA256(keyBytes);
				byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
				string base64Signature = Convert.ToBase64String(signatureBytes);
    			string urlEncodedSignature = HttpUtility.UrlEncode(base64Signature);

				string sasToken = $"SharedAccessSignature sr={encodedURI}&sig={urlEncodedSignature}&se={expiry}";

				var request = new HttpRequestMessage(HttpMethod.Post, $"https://{baseURI}/messages/events?api-version=2021-04-12");
				request.Headers.Add("Authorization", sasToken);
				request.Content = content;

				LOG("Sending message...");
				HttpResponseMessage response = await HttpClient.SendAsync(request);

				if (response.IsSuccessStatusCode)
				{
					LOG("Telemetry sent to IoT Hub successfully.");
				}
				else
				{
					ERROR($"Error sending telemetry: {response.StatusCode}");
				}

				return response;
			}
		}
	}
}