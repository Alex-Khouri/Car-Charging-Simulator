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

			private static bool Receiving;
			private static bool Transmitting;

			public static void Initialise(Car deviceModel)
			{
				DeviceModel = deviceModel;
				DeviceClient = DeviceClient.CreateFromConnectionString(CONNECTION_STRING, TransportType.Mqtt);
				Receiving = true;
				Transmitting = true;

				DeviceClient.OpenAsync();

				Task.Run(() =>
				{
					_ = ReceiveCloudToDeviceMessagesAsync();
				});

				_ = SendDeviceToCloudMessagesAsync();
			}

			public static void StartReceiving()
			{
				Receiving = true;
			}

			public static void StopReceiving()
			{
				Receiving = false;
			}

			public static void StartTransmitting()
			{
				Transmitting = true;
			}

			public static void StopTransmitting()
			{
				Transmitting = false;
			}

			private static async Task ReceiveCloudToDeviceMessagesAsync()
			{
				while (Receiving && DeviceClient != null)
				{
					await Task.Delay(NETWORK_RECEIVER_REFRESH_INTERVAL);
					Message receivedMessage = await DeviceClient.ReceiveAsync();
					if (receivedMessage == null) continue;

					var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
					LOG($"Received message from IoT Hub: {messageData}");
					string? deviceID;
					string? actionString;
					ACTION actionType;
					if (DeviceModel != null &&
						receivedMessage.Properties.TryGetValue(DEVICE_ID_KEY, out deviceID) &&
						deviceID.Equals(DeviceModel.GetLicence(), StringComparison.OrdinalIgnoreCase) &&
						receivedMessage.Properties.TryGetValue(ACTION_KEY, out actionString) &&
						Enum.TryParse(actionString, IGNORE_CASE, out actionType))
					{
						switch (actionType)
						{
							case ACTION.CHARGE_START:
								string? startTimeString;
								receivedMessage.Properties.TryGetValue(CHARGE_START_TIME_KEY, out startTimeString);
								DateTime startTime;
								if (DateTime.TryParse(startTimeString, out startTime))
								{
									DeviceModel.StartCharge(startTime);
								}
								DeviceModel.StartCharge();
								break;
							case ACTION.CHARGE_STOP:
								DeviceModel.StopCharge();
								break;
							case ACTION.DISCHARGE_START:
								DeviceModel.StartDischarge();
								break;
							case ACTION.DISCHARGE_STOP:
								DeviceModel.StopDischarge();
								break;
							default:
								break;
						}
					}

					await DeviceClient.CompleteAsync(receivedMessage);
				}
			}

			private static async Task SendDeviceToCloudMessagesAsync()
			{
				while (Transmitting && DeviceClient != null)
				{
					var deviceChargeLevel = new { chargeLevel = DeviceModel?.GetChargeLevel() };
					var messageString = Newtonsoft.Json.JsonConvert.SerializeObject(deviceChargeLevel);
					var message = new Message(Encoding.ASCII.GetBytes(messageString));

					LOG($"Current charge level: {DeviceModel?.GetChargeLevel()}");

					await DeviceClient.SendEventAsync(message);

					await Task.Delay(NETWORK_SENDER_TRANSMISSION_INTERVAL);
				}
			}

			public static void Shutdown()
			{
				DeviceClient?.CloseAsync();
				StopReceiving();
				StopTransmitting();
			}
		}
	}
}