namespace Client
{
	public static class Globals
	{
		public static readonly string IOT_HUB_NAME			= "car-charging-simulator";
		// TODO: Move access string to secure file
		public static readonly string SECURE_ACCESS_STRING	= "oLlL0U349GN1dS5z3DN/JuLFy9A0Q8L77KCHoH0ZlTk=";
		public static readonly string EMPTY_STRING			= "";
		public static readonly int SAS_TOKEN_LIFETIME		= 3600;	// Seconds
		public static readonly double NULL_CHARGE			= -1.0;
		public static readonly double MIN_CHARGE			= 0.0;
		public static readonly double MAX_CHARGE			= 100.0;

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