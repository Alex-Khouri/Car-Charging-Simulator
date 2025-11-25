using Microsoft.Azure.Devices.Client;
using System;
using System.Text;

using Device.Model;

namespace Device
{
	using static Globals;

	namespace Controller
	{
		public static class NetworkController
		{
			private static Car? DeviceModel;
			private static DeviceClient? DeviceClient;

			public static void Initialise(Car deviceModel)
			{
				DeviceModel = deviceModel;
				DeviceClient = DeviceClient.CreateFromConnectionString(CONNECTION_STRING, TransportType.Mqtt);

				DeviceClient.OpenAsync();

				Task.Run(() =>
				{
					_ = RunMessageReceiver();
				});
			}

			private static async Task RunMessageReceiver()
			{
				Console.WriteLine("Starting network message receiver...");
				while (DeviceClient != null)	// Run indefinitely
				{
					Thread.Sleep(NETWORK_RECEIVER_REFRESH_INTERVAL);
					Message receivedMessage = await DeviceClient.ReceiveAsync();
					if (receivedMessage == null) continue;

					var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
					Console.WriteLine($"Received message from IoT Hub: {messageData}");

					await DeviceClient.CompleteAsync(receivedMessage);
				}
			}

			public static void Shutdown()
			{
				if (DeviceClient != null)
				{
					DeviceClient.CloseAsync();
				}
			}
		}
	}
}