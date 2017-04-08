
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace XInputNET.Abstraction
{
    /// <summary>
    /// An abstraction for an xinput device.
    /// </summary>
    public class Gamepad
    {
        #region Private Constants

        private static short thumbMaxValue = System.Int16.MaxValue;

        private static byte triggerMaxValue = System.Byte.MaxValue;

        #endregion

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
            this.LeftThumbDeadzone = XInput.Gamepad.LeftThumbDeadzone / (double)thumbMaxValue;
            this.RightThumbDeadzone = XInput.Gamepad.RightThumbDeadzone / (double)thumbMaxValue;
            this.LeftTriggerThreshold = XInput.Gamepad.TriggerThreshold / (double)triggerMaxValue;
            this.RightTriggerThreshold = XInput.Gamepad.TriggerThreshold / (double)triggerMaxValue;

            this.UserIndex = (int)index;
            this.StartObserverThread();
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

        public bool L
        {
            get;
            private set;
        }

        public bool R
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

        public bool LeftThumbPress
        {
            get;
            private set;
        }

        public bool RightThumbPress
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

        public double LeftThumbX
        {
            get
            {
                return this.ApplyDeadzone(this.LeftThumbXUnfiltered, this.LeftThumbDeadzone);
            }
        }

        public double LeftThumbY
        {
            get
            {
                return this.ApplyDeadzone(this.LeftThumbYUnfiltered, this.LeftThumbDeadzone);
            }
        }

        public double LeftThumbYUnfiltered
        {
            get;
            private set;
        }

        public double LeftThumbXUnfiltered
        {
            get;
            private set;
        }

        public double RightThumbX
        {
            get
            {
                return this.ApplyDeadzone(this.RightThumbXUnfiltered, this.RightThumbDeadzone);
            }
        }

        public double RightThumbY
        {
            get
            {
                return this.ApplyDeadzone(this.RightThumbYUnfiltered, this.RightThumbDeadzone);
            }
        }

        public double RightThumbYUnfiltered
        {
            get;
            private set;
        }

        public double RightThumbXUnfiltered
        {
            get;
            private set;
        }

        public double LeftTriggerUnfiltered
        {
            get;
            private set;
        }

        public double RightTriggerUnfiltered
        {
            get;
            private set;
        }

        public double LeftTrigger
        {
            get
            {
                return this.ApplyDeadzone(this.LeftTriggerUnfiltered, this.LeftTriggerThreshold);
            }
        }

        public double RightTrigger
        {
            get
            {
                return this.ApplyDeadzone(this.RightTriggerUnfiltered, this.RightTriggerThreshold);
            }
            
        }

        public Boolean StopObserving
        {
            get;
            set;
        }

        #endregion

        #region Private Methods

        private void StartObserverThread()
        {
            Thread thread = new Thread(this.ObserveGamepad);
            thread.Start();
        }

        private void ObserveGamepad()
        {
            uint lastPacketNumber = 0;
            XInput.State state = new XInput.State();
            while (!this.StopObserving)
            {  
                XInput.Error result = XInput.GetState((XInput.UserIndex)this.UserIndex, out state);
                if (result == XInput.Error.Success && state.packetNumber > lastPacketNumber)
                {
                    lastPacketNumber = state.packetNumber;
                    this.SetStateFromXInputState(state);
                    this.OnStateChanged();
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Applies a deadzone to an axis value.
        /// </summary>
        /// <param name="axisValue">The axis value.</param>
        /// <param name="deadzone">The deadzone.</param>
        /// <returns>The filtered value from 0 to 1.</returns>
        private double ApplyDeadzone(double axisValue, double deadzone)
        {
            double rt;

            if (axisValue > 0.0)
            {
                rt = Math.Max((axisValue - deadzone) / (1.0 - deadzone), 0.0);
            }
            else
            {
                rt = Math.Min((axisValue + deadzone) / (1.0 - deadzone), 0.0);
            }

            return rt;
        }

        /// <summary>
        /// Sets the state of the <see cref="Gamepad"/> instance from an XInput.State object.
        /// </summary>
        /// <param name="state">The raw input state.</param>
        private void SetStateFromXInputState(XInput.State state)
        {
            this.A = state.gamepad.isButtonPressed(XInput.GamepadButtons.A);
            this.B = state.gamepad.isButtonPressed(XInput.GamepadButtons.B);
            this.X = state.gamepad.isButtonPressed(XInput.GamepadButtons.X);
            this.Y = state.gamepad.isButtonPressed(XInput.GamepadButtons.Y);
            this.L = state.gamepad.isButtonPressed(XInput.GamepadButtons.LeftShoulder);
            this.R = state.gamepad.isButtonPressed(XInput.GamepadButtons.RightShoulder);
            this.Start = state.gamepad.isButtonPressed(XInput.GamepadButtons.Start);
            this.Back = state.gamepad.isButtonPressed(XInput.GamepadButtons.Back);
            this.LeftThumbPress = state.gamepad.isButtonPressed(XInput.GamepadButtons.LeftThumb);
            this.RightThumbPress = state.gamepad.isButtonPressed(XInput.GamepadButtons.RightThumb);
            this.DPadUp = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadUp);
            this.DPadDown = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadDown);
            this.DPadLeft = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadLeft);
            this.DPadRight = state.gamepad.isButtonPressed(XInput.GamepadButtons.DPadRight);
            this.LeftThumbXUnfiltered = state.gamepad.leftThumbX / (double)thumbMaxValue;
            this.LeftThumbYUnfiltered = state.gamepad.leftThumbY / (double)thumbMaxValue;
            this.RightThumbXUnfiltered = state.gamepad.rightThumbX / (double)thumbMaxValue;
            this.RightThumbYUnfiltered = state.gamepad.rightThumbY / (double)thumbMaxValue;
            this.LeftTriggerUnfiltered = state.gamepad.leftTrigger / (double)triggerMaxValue;
            this.RightTriggerUnfiltered = state.gamepad.rightTrigger / (double)triggerMaxValue;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a textual representation of the gamepad state.
        /// </summary>
        /// <returns>The gamepad state string.</returns>
        public override string ToString()
        {
            string output = "";
            if (this.A) output += "A, ";
            if (this.B) output += "B, ";
            if (this.X) output += "X, ";
            if (this.Y) output += "Y, ";
            if (this.L) output += "L, ";
            if (this.R) output += "R, ";
            if (this.Start) output += "Start, ";
            if (this.Back) output += "Back, ";
            if (this.DPadUp) output += "DPadUp, ";
            if (this.DPadDown) output += "DPadDown, ";
            if (this.DPadLeft) output += "DPadLeft, ";
            if (this.DPadRight) output += "DPadRight, ";
            if (this.LeftThumbPress) output += "LeftThumbPress, ";
            if (this.RightThumbPress) output += "RightThumbPress, ";
            output += "lx: " + this.LeftThumbX + ",";
            output += "ly: " + this.LeftThumbY + ",";
            output += "rx: " + this.RightThumbX + ",";
            output += "ry: " + this.RightThumbY + ",";

            return output;
        }

        #endregion

        #region KeyEventArgs

        /// <summary>
        /// Holds additional information for key events.
        /// </summary>
        public class KeyEventArgs
        {

        }

        #endregion

        #region Events

        public delegate void StateChangedEventHandler(Object sender, EventArgs args);

        public event StateChangedEventHandler StateChanged;

        private void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged(this, null);
        }

        public delegate void KeyEventHandler(Object sender, KeyEventArgs args);

        public event KeyEventHandler KeyDown;

        private void OnKeyDown()
        {
            if (KeyDown != null)
                KeyDown(this, null);
        }

        private void OnKeyUp()
        {
            if (KeyUp != null)
                KeyUp(this, null);
        }

        public event KeyEventHandler KeyUp;



        #endregion
    }
}
