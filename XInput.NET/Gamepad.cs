
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

        /// <summary>
        /// The maximum raw value for anolog stick axes.
        /// </summary>
        private static short thumbMaxValue = System.Int16.MaxValue;

        /// <summary>
        /// The maximum raw value for triggers.
        /// </summary>
        private static byte triggerMaxValue = System.Byte.MaxValue;

        #endregion

        #region Static Methods

        /// <summary>
        /// Collects all the connected xinput gamepads.
        /// </summary>
        /// <returns>A list of all connected devices.</returns>
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

        /// <summary>
        /// Gets the user id of the gamepad.
        /// </summary>
        public int UserIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the charge level of the battery in the gamepad.
        /// </summary>
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

        /// <summary>
        /// Gets the type of the battery in the gamepad.
        /// </summary>
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

        /// <summary>
        /// Gets the state of the A button.
        /// </summary>
        public bool A
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the B button.
        /// </summary>
        public bool B
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the X button.
        /// </summary>
        public bool X
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the Y button.
        /// </summary>
        public bool Y
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the L button.
        /// </summary>
        public bool L
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the R button.
        /// </summary>
        public bool R
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the start button.
        /// </summary>
        public bool Start
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the back button.
        /// </summary>
        public bool Back
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the d-pad up button.
        /// </summary>
        public bool DPadUp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the d-pad down button.
        /// </summary>
        public bool DPadDown
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the d-pad left button.
        /// </summary>
        public bool DPadLeft
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the d-pad right button.
        /// </summary>
        public bool DPadRight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the left thumb press.
        /// </summary>
        public bool LeftThumbPress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the right thumb press.
        /// </summary>
        public bool RightThumbPress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the deadzone for the left analog control stick.
        /// The value should be in the range of 0.0 to 1.0.
        /// </summary>
        public double LeftThumbDeadzone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the deadzone for the right analog control stick.
        /// The value should be in the range of 0.0 to 1.0.
        /// </summary>
        public double RightThumbDeadzone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the threshold value for the left trigger.
        /// The value should be in the range of 0.0 to 1.0.
        /// </summary>
        public double LeftTriggerThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the threshold value for the right trigger.
        /// The value should be in the range of 0.0 to 1.0.
        /// </summary>
        public double RightTriggerThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the x-axis value of the left analog control stick.
        /// The deadzone is already applied.
        /// </summary>
        public double LeftThumbX
        {
            get
            {
                return this.ApplyDeadzone(this.LeftThumbXUnfiltered, this.LeftThumbDeadzone);
            }
        }

        /// <summary>
        /// Gets the y-axis value of the left analog control stick.
        /// The deadzone is already applied.
        /// </summary>
        public double LeftThumbY
        {
            get
            {
                return this.ApplyDeadzone(this.LeftThumbYUnfiltered, this.LeftThumbDeadzone);
            }
        }

        /// <summary>
        /// Gets the unfiltered y-axis value of the left anolog control stick.
        /// The deadzone is not applied.
        /// </summary>
        public double LeftThumbYUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the unfiltered x-axis value of the left anolog control stick.
        /// The deadzone is not applied.
        /// </summary>
        public double LeftThumbXUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the x-axis value of the right analog control stick.
        /// The deadzone is already applied.
        /// </summary>
        public double RightThumbX
        {
            get
            {
                return this.ApplyDeadzone(this.RightThumbXUnfiltered, this.RightThumbDeadzone);
            }
        }

        /// <summary>
        /// Gets the y-axis value of the right analog control stick.
        /// The deadzone is already applied.
        /// </summary>
        public double RightThumbY
        {
            get
            {
                return this.ApplyDeadzone(this.RightThumbYUnfiltered, this.RightThumbDeadzone);
            }
        }

        /// <summary>
        /// Gets the unfiltered y-axis value of the right anolog control stick.
        /// The deadzone is not applied.
        /// </summary>
        public double RightThumbYUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the unfiltered x-axis value of the right anolog control stick.
        /// The deadzone is not applied.
        /// </summary>
        public double RightThumbXUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the unfiltered value of the left trigger.
        /// The threshold is not applied.
        /// </summary>
        public double LeftTriggerUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the unfiltered value of the right trigger.
        /// The threshold is not applied.
        /// </summary>
        public double RightTriggerUnfiltered
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the left trigger.
        /// The threshold is already applied.
        /// </summary>
        public double LeftTrigger
        {
            get
            {
                return this.ApplyDeadzone(this.LeftTriggerUnfiltered, this.LeftTriggerThreshold);
            }
        }

        /// <summary>
        /// Gets the value of the right trigger.
        /// The threshold is already applied.
        /// </summary>
        public double RightTrigger
        {
            get
            {
                return this.ApplyDeadzone(this.RightTriggerUnfiltered, this.RightTriggerThreshold);
            }
            
        }

        /// <summary>
        /// Gets or sets whether the gamepad should stop being observed for status changes.
        /// </summary>
        public Boolean StopObserving
        {
            get;
            set;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Starts observing the gamepad state in a new thread.
        /// </summary>
        private void StartObserverThread()
        {
            Thread thread = new Thread(this.ObserveGamepad);
            thread.Start();
        }

        /// <summary>
        /// Observes the gamepad for status changes.
        /// </summary>
        private void ObserveGamepad()
        {
            uint lastPacketNumber = 0;
            XInput.State state = new XInput.State();
            XInput.KeyStroke keystroke = new XInput.KeyStroke();
            while (!this.StopObserving)
            {  
                XInput.Error result = XInput.GetState((XInput.UserIndex)this.UserIndex, out state);
                if (result == XInput.Error.Success && state.packetNumber > lastPacketNumber)
                {
                    lastPacketNumber = state.packetNumber;
                    this.SetStateFromXInputState(state);
                    this.RaiseStateChanged();
                }

                result = XInput.GetKeystroke((XInput.UserIndex)this.UserIndex, out keystroke);
                if(result == XInput.Error.Success)
                {
                    if(keystroke.flags == XInput.KeyStrokeFlags.KeyUp)
                    {
                        this.RaiseKeyUp((KeyEventArgs.KeyCode)keystroke.virtualKey);
                    }

                    if (keystroke.flags == XInput.KeyStrokeFlags.KeyDown)
                    {
                        this.RaiseKeyDown((KeyEventArgs.KeyCode)keystroke.virtualKey);
                    }
                }

                Thread.Sleep(2);
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
            output += "lx: " + this.LeftThumbX + ", ";
            output += "ly: " + this.LeftThumbY + ", ";
            output += "rx: " + this.RightThumbX + ", ";
            output += "ry: " + this.RightThumbY + ", ";
            output += "lt: " + this.LeftTrigger + ", ";
            output += "rt: " + this.RightTrigger;

            return output;
        }

        #endregion

        #region KeyEventArgs

        /// <summary>
        /// Holds additional information for key events.
        /// </summary>
        public class KeyEventArgs
        {
            #region Constructors

            /// <summary>
            /// Creates a new <see cref="KeyEventArgs"/> instance.
            /// </summary>
            /// <param name="key">Which key state changed.</param>
            /// <param name="change">Whether a key got pressed or released.</param>
            public KeyEventArgs(KeyCode key, KeyChange change)
            {
                this.Key = key;
                this.Change = change;
            }

            #endregion

            #region Enumerations

            /// <summary>
            /// Enumerates the key change possibilities.
            /// </summary>
            public enum KeyChange
            {
                Up,
                Down
            }

            /// <summary>
            /// Enumerates all pressable keys.
            /// </summary>
            public enum KeyCode : UInt16
            {
                A = 0x5800,
                B = 0x5801,
                X = 0x5802,
                Y = 0x5803,
                R = 0x5804,
                L = 0x5805,
                LTrigger = 0x5806,
                RTrigger = 0x5807,

                DPadUp = 0x5810,
                DPadDown = 0x5811,
                DPadLeft = 0x5812,
                DPadRight = 0x5813,
                Start = 0x5814,
                Back = 0x5815,
                LThumbPress = 0x5816,
                RThumbPress = 0x5817,

                LThumbUp = 0x5820,
                LThumbDown = 0x5821,
                LThumbRight = 0x5822,
                LThumbLeft = 0x5823,
                LThumbUpLeft = 0x5824,
                LThumbUpRight = 0x5825,
                LThumbDownRight = 0x5826,
                LThumbDownLeft = 0x5827,

                RThumbUp = 0x5830,
                RThumbDown = 0x5831,
                RThumbRight = 0x5832,
                RThumbLeft = 0x5833,
                RThumbUpLeft = 0x5834,
                RThumbUpRight = 0x5835,
                RThumbDownRight = 0x5836,
                RThumbDownLeft = 0x5837
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// How the key changed its state.
            /// </summary>
            public KeyChange Change
            {
                get;
                private set;
            }

            /// <summary>
            /// The key that changed its state.
            /// </summary>
            public KeyCode Key
            {
                get;
                private set;
            }

            #endregion
        }

        #endregion

        #region Events

        /// <summary>
        /// The state changed event delegate type.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The event arguments.</param>
        public delegate void StateChangedEventHandler(Object sender, EventArgs args);

        /// <summary>
        /// The state changed event fires when the gamepad changes its state in any way.
        /// </summary>
        public event StateChangedEventHandler StateChanged;

        /// <summary>
        /// Raises a state changed event.
        /// </summary>
        private void RaiseStateChanged()
        {
            if (StateChanged != null)
                StateChanged(this, null);
        }

        /// <summary>
        /// The key event delegate type.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The event arguments.</param>
        public delegate void KeyEventHandler(Object sender, KeyEventArgs args);

        /// <summary>
        /// The key down event is fired when a button is pressed down on the gamepad.
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Raises a key down event.
        /// </summary>
        /// <param name="key">The key that was pressed down.</param>
        private void RaiseKeyDown(KeyEventArgs.KeyCode key)
        {
            if (KeyDown != null)
                KeyDown(this, new KeyEventArgs(key, KeyEventArgs.KeyChange.Down));
        }

        /// <summary>
        /// The key up event
        /// </summary>
        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Raises a key up event.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        private void RaiseKeyUp(KeyEventArgs.KeyCode key)
        {
            if (KeyUp != null)
                KeyUp(this, new KeyEventArgs(key, KeyEventArgs.KeyChange.Up));
        }

        #endregion
    }
}
