
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XInputNET.Abstraction;

#endregion

namespace XInputTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var pads = Gamepad.GetConnectedDevices();
            if(pads.Count > 0)
            {
                Gamepad pad = pads.First();
                pad.StateChanged += Pad_StateChanged;
            }
            else
            {
                Console.WriteLine("No Gamepads connected");
            }

            Console.ReadLine();
        }

        private static void Pad_StateChanged(object sender, EventArgs args)
        {
            Console.WriteLine((sender as Gamepad)?.ToString());
        }
    }
}
