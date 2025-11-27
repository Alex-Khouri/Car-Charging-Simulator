----------------------------------------
REQUIREMENTS:

* .NET 8 or 9
----------------------------------------
RUNNING THE APPLICATION:

IOT DEVICE
1) Navigate to `car-charging-devices/abc123` in terminal
2) Run `run.sh` script
	* Framework is currently set to .NET 9, so you'll need to edit the `--framework` parameter if you want to use .NET 8

NB: This currently connects to Azure IoT Hub, and can be tested using the 'Message to Device' feature for the 'abc123' device

WEB INTERFACE
1) Navigate to `car-charging-client` in terminal
2) Run `run.sh` script
	* Framework is currently set to .NET 9, so you'll need to edit the `--framework` parameter if you want to use .NET 8
3) Navigate to application URL displayed within .NET terminal info messages
4) Navigate to 'Manage Car' on browser page

NB: Car charging info and controls are currently always visible, as they were being used for testing (there's a TODO for moving them into a conditional block once the licence retrieval system is working)
----------------------------------------
TESTING:

IOT DEVICE
Message Format:
device-id: <string>
	Supported Values (case-insensitive):
		* abc123
action: <string>
	Supported Values (case-insensitive):
		* charge_start
		* charge_stop
		* discharge_start
		* discharge_stop
charge-delay: <string>
	Supports a range of datetime string formats; see this documentation for options:
	https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse
----------------------------------------