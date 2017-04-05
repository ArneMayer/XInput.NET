
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace XInputNET.Abstraction
{
    /// <summary>
    /// An abstraction for an xinput device.
    /// </summary>
    class Gamepad
    {
        #region Static Methods

        public static List<Gamepad> GetConnectedDevices()
        {
            List<Gamepad> gamepads = new List<Gamepad>();

            for(XInput.UserIndex userIndex = 0; userIndex < XInput.UserIndex.MaxCount; userIndex++)
            {
                XInput.State state = new XInput.State();
                XInput.Error result = XInput.GetState(userIndex, out state);
                if (result == XInput.Error.Success)
                {
                    gamepads.Add(new Gamepad(state, userIndex));
                }
            }

            return gamepads;
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// Types of batteries.
        /// </summary>
        public enum BatteryType : byte
        {
            /// <summary>
            /// This device is not connected.
            /// </summary>
            Disconnected = 0x00,

            /// <summary>
            /// Wired device, no battery.
            /// </summary>
            Wired = 0x01,

            /// <summary>
            /// Alkaline battery source.
            /// </summary>
            Alkaline = 0x02,

            /// <summary>
            /// Nickel Metal Hydride battery source.
            /// </summary>
            NiMH = 0x03,

            /// <summary>
            /// Cannot determine the battery type.
            /// </summary>
            Unknown = 0xFF
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Gamepad"/> instance from an xinput state structure.
        /// </summary>
        /// <param name="state"></param>
        private Gamepad(XInput.State state, XInput.UserIndex index)
        {
            this.LeftThumbDeadzone = XInput.Gamepad.LeftThumbDeadzone / (double)System.Int16.MaxValue;
            this.RightThumbDeadzone = XInput.Gamepad.RightThumbDeadzone / (double)System.Int16.MaxValue;
            this.LeftTriggerThreshold = XInput.Gamepad.TriggerThreshold / (double)System.Byte.MaxValue;
            this.RightTriggerThreshold = XInput.Gamepad.TriggerThreshold / (double)System.Byte.MaxValue;

            this.UserIndex = (int)index;
            this.A = state.gamepad.isButtonPressed(XInput.GamepadButtons.A);
            this.B = state.gamepad.isButtonPressed(XInput.GamepadButtons.B);
            this.X = state.gamepad.isButtonPressed(XInput.GamepadButtons.X);
            this.Y = state.gamepad.isButtonPressed(XInput.GamepadButtons.Y);
            this.LeftShoulder = state.gamepad.isButtonPressed(XInput.GamepadButtons.LeftShoulder);
            this.RightShoulder = state.gamepad.isButtonPressed(XInput.GamepadButtons.RightShoulder);
            this.Start = state.gamepad.isButtonPressed(XInput.GamepadButtons.Start);
            this.Back = state.gamepad.isButtonPressed(XInput.GamepadButtons.Back);
            this.LeftThumb = state.gamepad.isButtonPressed(XInput.GamepadButtons.LeftThumb);
            this.RightThumb = state.gamepad.isButtonPressed(XInput.GamepadButtons.RightThumb);
            this.DPadUp = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadUp);
            this.DPadDown = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadDown);
            this.DPadLeft = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadLeft);
            this.DPadRight = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadRight);
        }

        #endregion

        #region Public Properties

        public int UserIndex
        {
            get;
            private set;
        }

        public double BatteryChargeLevel
        {
            get
            {
                XInput.BatteryInformation batteryInfo = new XInput.BatteryInformation();
                var result = XInput.GetBatteryInformation((XInput.UserIndex)this.UserIndex, XInput.BatteryDeviceType.Gamepad, out batteryInfo);

                if(result != XInput.Error.Success)
                {
                    return 0.0;
                }
                else if (batteryInfo.batteryLevel == XInput.BatteryLevel.Empty)
                {
                    return 0.0;
                }
                else if (batteryInfo.batteryLevel == XInput.BatteryLevel.Full)
                {
                    return 1.0;
                }
                else if (batteryInfo.batteryLevel == XInput.BatteryLevel.Low)
                {
                    return 0.33;
                }
                else if (batteryInfo.batteryLevel == XInput.BatteryLevel.Medium)
                {
                    return 0.66;
                }
                else
                {
                    throw new Exception("Unknown battery level code: " + batteryInfo.batteryLevel);
                }
            }
        }

        public BatteryType TypeOfBattery
        {
            get
            {
                XInput.BatteryInformation batteryInfo = new XInput.BatteryInformation();
                var result = XInput.GetBatteryInformation((XInput.UserIndex)this.UserIndex, XInput.BatteryDeviceType.Gamepad, out batteryInfo);

                if (result != XInput.Error.Success)
                {
                    return 0.0;
                }
                else
                {
                    return (BatteryType)batteryInfo.batteryType;
                }
            }
        }

        public bool A
        {
            get;
            private set;
        }


        public bool B
        {
            get;
            private set;
        }

        public bool X
        {
            get;
            private set;
        }

        public bool Y
        {
            get;
            private set;
        }

        public bool LeftShoulder
        {
            get;
            private set;
        }

        public bool RightShoulder
        {
            get;
            private set;
        }

        public bool Start
        {
            get;
            private set;
        }

        public bool Back
        {
            get;
            private set;
        }

        public bool DPadUp
        {
            get;
            private set;
        }

        public bool DPadDown
        {
            get;
            private set;
        }

        public bool DPadLeft
        {
            get;
            private set;
        }

        public bool DPadRight
        {
            get;
            private set;
        }

        public bool LeftThumb
        {
            get;
            private set;
        }

        public bool RightThumb
        {
            get;
            private set;
        }

        public double LeftThumbDeadzone
        {
            get;
            set;
        }

        public double RightThumbDeadzone
        {
            get;
            set;
        }

        public double LeftTriggerThreshold
        {
            get;
            set;
        }

        public double RightTriggerThreshold
        {
            get;
            set;
        }

        #endregion
    }
}
