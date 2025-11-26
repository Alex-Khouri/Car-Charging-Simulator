using Microsoft.Azure.Devices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	using static Globals;

	using Network;

	namespace Controller
	{
		public static class NetworkController
		{
			private static SocketComponent? Socket;

			public static void Initialise()
			{
				Socket = new SocketComponent();
			}

			public static HttpResponseMessage? SendMessageToHub(string deviceId, string deviceValue)
			{
				if (Socket == null)
				{
					ERROR($"Unable to send data to IoT hub due to uninitialised socket");
					return null;
				}

				return Socket.SendMessageToHub(deviceId, deviceValue).Result;
			}

			public static void Shutdown()
			{

			}
		}
	}
}