using Device;
using Device.Controller;
using Device.Model;

using static Device.Globals;

Console.WriteLine($"Starting device: {DEVICE_LICENCE}");

Car deviceModel = new Car(DEVICE_LICENCE);

Console.WriteLine("Initialising network connection...");
try
{
	NetworkController.Initialise(deviceModel);
}
catch (Exception e)
{
	Console.WriteLine($"Unable to initialise connection due to runtime error:\n{e}");
}

Console.WriteLine("Connection established!");
Console.WriteLine("Press Ctrl+C to exit program...");
while (Console.ReadKey().Key != ConsoleKey.C || Console.ReadKey().Modifiers != ConsoleModifiers.Control)
{
	Thread.Sleep(TERMINAL_INPUT_CAPTURE_INTERVAL);
}

Console.WriteLine($"Stopping device: {DEVICE_LICENCE}");
NetworkController.Shutdown();
Console.WriteLine($"Device stopped: {DEVICE_LICENCE}");