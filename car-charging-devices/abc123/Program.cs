using Device;
using Device.Controller;
using Device.Model;

using static Device.Globals;

LOG($"Starting device: {DEVICE_LICENCE}");

Car deviceModel = new Car(DEVICE_LICENCE);

LOG("Initialising network connection...");
try
{
	NetworkController.Initialise(deviceModel);
}
catch (Exception e)
{
	ERROR($"Unable to initialise connection due to runtime error:\n{e}");
}

LOG("Press Escape to exit program...");
while (Console.ReadKey().Key != ConsoleKey.Escape)
{
	await Task.Delay(TERMINAL_INPUT_CAPTURE_INTERVAL);
}

LOG($"Stopping device: {DEVICE_LICENCE}");
NetworkController.Shutdown();