using System;
using System.Threading;

namespace Device
{
	using static Globals;

	namespace Model
	{
		enum STATE
		{
			CHARGING,
			IDLE,
			DISCHARGING,
			NULL
		}

		public class Car
		{
			private string Licence;
			private double ChargeLevel = 0.0;
			private STATE State = STATE.IDLE;

			public Car(string licence)
			{
				Licence = licence;

				StartDischarge();
			}

			public string GetLicence()
			{
				return Licence;
			}

			public double GetChargeLevel()
			{
				return ChargeLevel;
			}

			public void StartCharge(DateTime? startTime = null)
			{
				Task.Run(async () =>
				{
					State = STATE.CHARGING;
					while (State.Equals(STATE.CHARGING) &&
						   startTime != null && startTime > DateTime.Now)
					{
						await Task.Delay(CHARGE_TIMER_REFRESH_INTERVAL);
					}

					while (State.Equals(STATE.CHARGING) && ChargeLevel < MAX_CHARGE)
					{
						ChargeLevel = Math.Min(ChargeLevel + CHARGE_INCREMENT, MAX_CHARGE);
						await Task.Delay(CHARGE_INTERVAL);
					}

					StopCharge();
				});
			}

			public void StopCharge()
			{
				if (State.Equals(STATE.CHARGING))
				{
					State = STATE.IDLE;
				}
			}

			public void StartDischarge()
			{
				State = STATE.DISCHARGING;
				Task.Run(async() =>
				{
					while (State.Equals(STATE.DISCHARGING))
					{
						ChargeLevel = Math.Max(MIN_CHARGE, ChargeLevel + DISCHARGE_DECREMENT);
						await Task.Delay(DISCHARGE_INTERVAL);
					}
				});
			}

			public void StopDischarge()
			{
				if (State.Equals(STATE.DISCHARGING))
				{
					State = STATE.IDLE;
				}
			}
		}
	}
}