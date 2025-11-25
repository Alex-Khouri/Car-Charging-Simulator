namespace Device
{
	public static class Globals
	{
		public static readonly string CONNECTION_STRING				= "HostName=Car-Charging-Simulator.azure-devices.net;DeviceId=abc123;SharedAccessKey=iztQ1MbijzZU0COCjsB/MkxFa2UndFqn6/dXaQ2wVLU=";
		public static readonly string DEVICE_LICENCE				= "abc123";
		// All intervals are measured in milliseconds
		public static readonly int CHARGE_INCREMENT_INTERVAL			= 1000;
		public static readonly int CHARGE_TIMER_REFRESH_INTERVAL		= 1000;
		public static readonly int NETWORK_RECEIVER_REFRESH_INTERVAL	= 10;
		public static readonly int TERMINAL_INPUT_CAPTURE_INTERVAL		= 1;
		public static readonly double MIN_CHARGE					= 0.0;
		public static readonly double MAX_CHARGE					= 100.0;
		public static readonly double CHARGE_INCREMENT				= 1.0;
	}
}