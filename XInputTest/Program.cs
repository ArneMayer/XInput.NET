
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace XInputTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XInput.XINPUT_BATTERY_INFORMATION battery = new XInput.XINPUT_BATTERY_INFORMATION();
            XInput.XInputGetBatteryInformation(0, 0, out battery);
            Console.WriteLine("ijoo");
            Console.In.ReadLine();
        }
    }
}
