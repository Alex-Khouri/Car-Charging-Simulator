using System;
using System.Threading;

namespace Device
{
	using static Globals;

	namespace Model
	{
		public class Car
		{
			private string Licence;
			private double Charge = 0.0;
			private bool Charging = false;

			public Car(string licence)
			{
				Licence = licence;
			}

			public string GetLicence()
			{
				return Licence;
			}

			public double GetCharge()
			{
				return Charge;
			}

			public void StartCharging(DateTime? startTime = null)
			{
				if (startTime != null)
				{
					Task.Run(() =>
					{
						while (startTime > DateTime.Now)
						{
							Thread.Sleep(CHARGE_TIMER_REFRESH_INTERVAL);
						}
					});
				}

				Charging = true;

				Task.Run(() =>
				{
					while (Charging && Charge < MAX_CHARGE)
					{
						Charge = Math.Min(Charge + CHARGE_INCREMENT, MAX_CHARGE);
						Thread.Sleep(CHARGE_INCREMENT_INTERVAL);
					}
					Charging = false;
				});
			}

			public void StopCharging()
			{
				Charging = false;
			}
		}
	}
}