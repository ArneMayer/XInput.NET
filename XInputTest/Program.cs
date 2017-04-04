
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XInputNET;
#endregion

namespace XInputTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XInput.Enable(true);
            /*
            // Get battery information
            XInput.BatteryInformation battery = new XInput.BatteryInformation();
            var result = XInput.XInputGetBatteryInformation(XInput.UserIndex.User0, XInput.BatteryDeviceType.Gamepad, out battery);
            if(result == XInput.Error.DeviceNotConnected)
            {
                Console.WriteLine("Error: Device not connected");
            }
            else
            {
                Console.WriteLine("Battery information:");
                Console.WriteLine(battery.batteryLevel.ToString());
                Console.WriteLine(battery.batteryType.ToString());
                Console.WriteLine();
            }

            // Get capability information
            XInput.Capabilities caps = new XInput.Capabilities();
            result = XInput.XInputGetCapabilities(XInput.UserIndex.User0, XInput.GetCapabilitiesFlags.Gamepad, out caps);
            if(result == XInput.Error.Success)
            {
                Console.WriteLine("Capabilities information:");
                Console.WriteLine(caps.type.ToString());
                Console.WriteLine(caps.subType.ToString());
                foreach(XInput.CapabilityFlags cap in Enum.GetValues(typeof(XInput.CapabilityFlags)))
                {
                    if((caps.flags & cap) != 0)
                    {
                        Console.WriteLine(cap.ToString());
                    }
                }
            }*/

            // Get state information
            XInput.State state = new XInput.State();
            uint packetNumber;
            XInput.Error result;
            while(true)
            {
                packetNumber = state.packetNumber;
                result = XInput.GetState(XInput.UserIndex.User0, out state);
                
                if(result != XInput.Error.Success)
                {
                    Console.WriteLine("An error occurred while retrieving the gamepad state");
                }
                else if(state.packetNumber > packetNumber)
                {
                    Console.Clear();
                    //Console.SetCursorPosition(0, 0);
                    //Console.CursorVisible = false;
                    Console.WriteLine(state.gamepad);
                    //Console.WriteLine("---------------");
                }

                Thread.Sleep(10);
            }
            
            Console.ReadLine();
        }
    }
}
