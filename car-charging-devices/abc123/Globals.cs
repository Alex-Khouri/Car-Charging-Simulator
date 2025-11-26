namespace Device
{
	public enum ACTION
	{
		CHARGE_START,
		CHARGE_STOP,
		DISCHARGE_START,
		DISCHARGE_STOP
	}

	public static class Globals
	{
		public static readonly string EMPTY_STRING					= "";
		public static readonly string CONNECTION_STRING				= "HostName=car-charging-simulator.azure-devices.net;DeviceId=abc123;SharedAccessKey=oLlL0U349GN1dS5z3DN/JuLFy9A0Q8L77KCHoH0ZlTk=";
		public static readonly string DEVICE_LICENCE				= "abc123";
		// Message Property Keys
		public static readonly string DEVICE_ID_KEY			= "device-id";
		public static readonly string ACTION_KEY			= "action";
		public static readonly string CHARGE_DELAY_KEY		= "charge-delay"; // e.g. 2009-06-15T13:45:30
		// All intervals are measured in milliseconds
		public static readonly int CHARGE_INTERVAL							= 1000;
		public static readonly int CHARGE_TIMER_REFRESH_INTERVAL			= 1000;
		public static readonly int DISCHARGE_INTERVAL						= 3000;
		public static readonly int NETWORK_RECEIVER_REFRESH_INTERVAL		= 10;
		public static readonly int NETWORK_SENDER_TRANSMISSION_INTERVAL		= 1000;
		public static readonly int TERMINAL_INPUT_CAPTURE_INTERVAL			= 1;
		public static readonly double MIN_CHARGE					= 0.0;
		public static readonly double MAX_CHARGE					= 100.0;
		public static readonly double CHARGE_INCREMENT				= 1.0;
		public static readonly double DISCHARGE_DECREMENT			= -1.0;
		public static readonly bool IGNORE_CASE						= true;

		public static void WARNING(string message)
		{
			Console.WriteLine($"[{DateTime.Now}] WARNING: {message}");
		}

		public static void ERROR(string message)
		{
			Console.WriteLine($"[{DateTime.Now}] ERROR: {message}");
		}

		public static void LOG(string message)
		{
			Console.WriteLine($"[{DateTime.Now}] {message}");
		}
	}
}