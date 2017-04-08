
#region Using Directives

using System.Runtime.InteropServices;
using dword = System.UInt32;
using word = System.UInt16;
using wchar = System.UInt16;

#endregion

namespace XInputNET
{
    /// <summary>
    /// XInput Game Controller API enables applications to receive input from the Xbox controller for Windows.
    /// </summary>
    public static class XInput
    {
        #region Enumerations

        /// <summary>
        /// Common error codes.
        /// </summary>
        public enum Error : dword
        {

            Success = 0,
            DeviceNotConnected = 1167,
            Empty = 4306
        }

        /// <summary>
        /// Device types available in <see cref="Capabilities"/>.
        /// </summary>
        public enum DeviceType : byte
        {
            Gamepad = 0x01
        }

        /// <summary>
        /// Device subtypes available in <see cref="Capabilities"/>.
        /// </summary>
        public enum DeviceSubtype : byte
        {
            Unknown = 0x00,
            Gamepad = 0x01,
            Wheel = 0x02,
            ArcadeStick = 0x03,
            FlightStick = 0x04,
            DancePad = 0x05,
            Guitar = 0x06,
            GuitarAlternate = 0x07,
            DrumKit = 0x08,
            GuitarBass = 0x0B,
            ArcadePad = 0x13
        }

        /// <summary>
        /// Flags for <see cref="Capabilities"/>.
        /// </summary>
        public enum CapabilityFlags : word
        {
            ForceFeedbackSupported = 0x0001,
            Wireless = 0x0002,
            VoiceSupported = 0x0004,
            PlugInModulesSupported = 0x0008,
            NoNavigation = 0x0010
        }

        /// <summary>
        /// Gamepad buttons.
        /// </summary>
        public enum GamepadButtons : word
        {
            DPadUp = 0x0001,
            DPadDown = 0x0002,
            DPadLeft = 0x0004,
            DPadRight = 0x0008,
            Start = 0x0010,
            Back = 0x0020,
            LeftThumb = 0x0040,
            RightThumb = 0x0080,
            LeftShoulder = 0x0100,
            RightShoulder = 0x0200,
            A = 0x1000,
            B = 0x2000,
            X = 0x4000,
            Y = 0x8000,
        }

        /// <summary>
        /// Flags to pass to <see cref="XInputGetCapabilities(UserIndex, GetCapabilitiesFlags, out Capabilities)"/>.
        /// </summary>
        public enum GetCapabilitiesFlags : dword
        {
            AllGamepads = 0,
            Gamepad = 0x00000001
        }

        /// <summary>
        /// Devices that support batteries.
        /// </summary>
        public enum BatteryDeviceType : byte
        {
            Gamepad = 0x00,
            Headset = 0x01
        }

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

        /// <summary>
        /// Battery status levels. These are only valid for wireless, connected devices, with known battery types.
        /// The amount of use time remaining depends on the type of device. 
        /// </summary>
        public enum BatteryLevel : byte
        {
            Empty = 0x00,
            Low = 0x01,
            Medium = 0x02,
            Full = 0x03
        }

        /// <summary>
        /// User index definitions.
        /// </summary>
        public enum UserIndex : dword
        {
            User0 = 0,
            User1 = 1,
            User2 = 2,
            User3 = 3,
            MaxCount = 4,
            Any = 0x000000FF
        }

        /// <summary>
        /// Codes returned for the gamepad keystroke.
        /// </summary>
        public enum VirtualKey : word
        {
            A = 0x5800,
            B = 0x5801,
            X = 0x5802,
            Y = 0x5803,
            RShoulder = 0x5804,
            LShoulder = 0x5805,
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

        /// <summary>
        /// Flags used in <see cref="KeyStroke"/>.
        /// </summary>
        public enum KeyStrokeFlags : word
        {
            KeyDown = 0x0001,
            KeyUp = 0x0002,
            Repeat = 0x0004
        }

        /// <summary>
        /// Flags used for <see cref="XInputGetKeystroke(UserIndex, dword, out KeyStroke)"/>.
        /// </summary>
        public enum GetKeyStrokeFlags : dword
        {
            Reserved = 0
        }

        #endregion

        #region Structs

        /// <summary>
        /// Contains information on battery type and charge state.
        /// </summary>
        public struct BatteryInformation
        {
            /// <summary>
            /// The type of battery.
            /// </summary>
            public BatteryType batteryType;

            /// <summary>
            /// The charge state of the battery. This value is only valid for wireless devices with a known battery type. 
            /// </summary>
            public BatteryLevel batteryLevel;
        }

        /// <summary>
        /// Describes the current state of the Xbox controller.
        /// </summary>
        public struct Gamepad
        {
            #region Constants

            /// <summary>
            /// Deadzone value of the left analog stick.
            /// </summary>
            public const short LeftThumbDeadzone = 7849;

            /// <summary>
            /// Deadzone value of the right analog stick.
            /// </summary>
            public const short RightThumbDeadzone = 8689;

            /// <summary>
            /// Threshold value of the triggers.
            /// </summary>
            public const byte TriggerThreshold = 30;

            #endregion

            #region Public Fields

            /// <summary>
            /// Bitmask of the device digital buttons. A set bit indicates that the corresponding button is pressed.
            /// </summary>
            public GamepadButtons buttonsBitmask;

            /// <summary>
            /// The current value of the left trigger analog control. The value is between 0 and 255.
            /// </summary>
            public byte leftTrigger;

            /// <summary>
            /// The current value of the right trigger analog control. The value is between 0 and 255.
            /// </summary>
            public byte rightTrigger;

            /// <summary>
            /// Left thumbstick x-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick. 
            /// A value of 0 is centered. Negative values signify to the left. Positive values signify to the right. 
            /// The constant <see cref="LeftThumbDeadzone"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            public short leftThumbX;

            /// <summary>
            /// Left thumbstick y-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify down. Positive values signify up.
            /// The constant <see cref="LeftThumbDeadzone"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            public short leftThumbY;

            /// <summary>
            /// Right thumbstick x-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify to the left. Positive values signify to the right.
            /// The constant <see cref="RightThumbDeadzone"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            public short rightThumbX;

            /// <summary>
            /// Right thumbstick y-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify down. Positive values signify ups.
            /// The constant <see cref="RightThumbDeadzone"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            public short rightThumbY;

            #endregion

            #region Public Methods

            /// <summary>
            /// Gets the pressed state of a gamepad button.
            /// </summary>
            /// <param name="button">The button flag.</param>
            /// <returns>The pressed state of the buton./returns>
            public bool isButtonPressed(GamepadButtons button)
            {
                return (this.buttonsBitmask & button) != 0;
            }

            /// <summary>
            /// Returns a string representation of the gamepad state.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return
                    "Left trigger: " + this.leftTrigger + "\n" +
                    "Right trigger: " + this.rightTrigger + "\n" +
                    "Left thumb X: " + this.leftThumbX + "\n" +
                    "Left thumb Y: " + this.leftThumbY + "\n" +
                    "Right thumb X: " + this.rightThumbX + "\n" +
                    "Right thumb Y: " + this.rightThumbY + "\n" +
                    "Buttons: " + this.buttonsBitmask;
            }

            #endregion
        }

        /// <summary>
        /// Specifies keystroke data returned by <see cref="XInputGetKeystroke(UserIndex, GetKeyStrokeFlags, out KeyStroke)"/>.
        /// </summary>
        public struct KeyStroke
        {
            /// <summary>
            /// Virtual-key code of the key, button, or stick movement. See <see cref="VirtualKey"/> for a list of valid virtual-keys.
            /// </summary>
            public VirtualKey virtualKey;

            /// <summary>
            /// This member is unused and the value is zero.
            /// </summary>
            public wchar unicode;

            /// <summary>
            /// Flags that indicate the keyboard state at the time of the input event.
            /// </summary>
            public KeyStrokeFlags flags;

            /// <summary>
            /// Index of the signed-in gamer associated with the device. Can be a value in the range 0–3.
            /// </summary>
            public UserIndex userIndex;

            /// <summary>
            /// Human Interface Device (HID) code corresponding to the input. If there is no corresponding HID code, this value is zero.
            /// </summary>
            public byte hidCode;
        }

        /// <summary>
        /// Represents the state of a controller.
        /// </summary>
        public struct State
        {
            /// <summary>
            /// State packet number. The packet number indicates whether there have been any changes in the state of the controller. 
            /// If the packet number member is the same in sequentially returned XINPUT_STATE structures, the controller state has not changed.
            /// The packet number member is incremented only if the status of the controller has changed since the controller was last polled.
            /// </summary>
            public dword packetNumber;

            /// <summary>
            /// <see cref="Gamepad"/> structure containing the current state of an Xbox Controller.
            /// </summary>
            public Gamepad gamepad;
        }

        /// <summary>
        /// Specifies motor speed levels for the vibration function of a controller.
        /// </summary>
        public struct Vibration
        {
            /// <summary>
            /// Speed of the left motor. Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies 100 percent motor use.
            /// </summary>
            public word leftMotorSpeed;

            /// <summary>
            /// Speed of the right motor. Valid values are in the range 0 to 65,535. Zero signifies no motor use; 65,535 signifies 100 percent motor use.
            /// </summary>
            public word rightMotorSpeed;
        }

        /// <summary>
        /// Describes the capabilities of a connected controller.
        /// </summary>
        public struct Capabilities
        {
            /// <summary>
            /// The controller type.
            /// </summary>
            public DeviceType type;

            /// <summary>
            /// Subtype of the game controller.
            /// </summary>
            public DeviceSubtype subType;

            /// <summary>
            /// Features of the controller.
            /// </summary>
            public CapabilityFlags flags;

            /// <summary>
            /// <see cref="Gamepad"/> structure that describes available controller features and control resolutions.
            /// </summary>
            public Gamepad gamepad;

            /// <summary>
            /// <see cref="Vibration"/> structure that describes available vibration functionality and resolutions.
            /// </summary>
            public Vibration vibration;
        }

        #endregion

        #region Function Prototypes

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="state">An <see cref="State"/> structure that receives the current state of the controller.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>. 
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in Winerror.h.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputGetState(UserIndex userIndex, out State state);

        /// <summary>
        /// Sends data to a connected controller. This function is used to activate the vibration function of a controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="vibration">An <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputSetState(UserIndex userIndex, ref Vibration vibration);

        /// <summary>
        /// Retrieves the capabilities and features of a connected controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3.</param>
        /// <param name="flags">Input flags that identify the controller type. 
        /// If this value is 0, then the capabilities of all controllers connected to the system are returned. 
        /// Currently, only one value is supported: <see cref="GetCapabilitiesFlags.Gamepad"/> - Limit the query to devices of Xbox controller type.</param>
        /// <param name="capabilities">An <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputGetCapabilities(UserIndex userIndex, GetCapabilitiesFlags flags, out Capabilities capabilities);

        /// <summary>
        /// Sets the reporting state of XInput.
        /// </summary>
        /// <param name="enable">If enable is FALSE, XInput will only send neutral data in response to <see cref="XInputGetState(UserIndex, out State)"/> (all buttons up, axes centered, and triggers at 0). 
        /// <see cref="XInputSetState(UserIndex, ref Vibration)"/> calls will be registered but not sent to the device. Sending any value other than FALSE will restore reading and writing functionality to normal.</param>
        [DllImport("xinput1_4.DLL")]
        private static extern void XInputEnable(bool enable);

        /// <summary>
        /// Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the gamer associated with the device.</param>
        /// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
        /// <param name="renderCount">Size, in wide-chars, of the render device ID string buffer.</param>
        /// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
        /// <param name="captureCount">Size, in wide-chars, of capture device ID string buffer.</param>
        /// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="Error.Success"/>.
        /// If there is no headset connected to the controller, the function will also retrieve <see cref="Error.Success"/> with null as the values for renderDeviceId and captureDeviceId.
        /// If the controller port device is not physically connected, the function will return <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, it will return a valid Win32 error code.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputGetAudioDeviceIds(UserIndex userIndex, [MarshalAs(UnmanagedType.LPWStr)] out string renderDeviceId,
            ref uint renderCount, [MarshalAs(UnmanagedType.LPWStr)] out string captureDeviceId, ref uint captureCount);

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–<see cref="UserIndex.MaxCount"/> − 1.</param>
        /// <param name="devType">Specifies which device associated with this user index should be queried. Must be <see cref="BatteryDeviceType.Gamepad"/> or <see cref="BatteryDeviceType.Headset"/>.</param>
        /// <param name="batteryInformation">An <see cref="BatteryInformation"/> structure that receives the battery information.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputGetBatteryInformation(UserIndex userIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation);

        /// <summary>
        /// Retrieves a gamepad input event.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device. 
        /// Can be a value in the range 0–<see cref="UserIndex.MaxCount"/> − 1, or <see cref="UserIndex.Any"/> to fetch the next available input event from any user.</param>
        /// <param name="reserved">Reserved. Must be value <see cref="GetKeyStrokeFlags.Reserved"/>, which is 0.</param>
        /// <param name="keystroke">An <see cref="KeyStroke"/> structure that receives an input event.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If no new keys have been pressed, the return value is <see cref="Error.Empty"/>.
        /// If the controller is not connected or the user has not activated it, the return value is <see cref="Error.DeviceNotConnected"/>.See the Remarks section below.
        /// If the function fails, the return value is an error code defined in Winerror.h.</returns>
        [DllImport("xinput1_4.DLL")]
        private static extern Error XInputGetKeystroke(UserIndex userIndex, GetKeyStrokeFlags reserved, out KeyStroke keystroke);

        #endregion

        #region Public Functions

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="state">An <see cref="State"/> structure that receives the current state of the controller.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>. 
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in Winerror.h.</returns>
        public static Error GetState(UserIndex userIndex, out State state)
        {
            return XInputGetState(userIndex, out state);
        }

        /// <summary>
        /// Sends data to a connected controller. This function is used to activate the vibration function of a controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="vibration">An <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        public static Error SetState(UserIndex userIndex, ref Vibration vibration)
        {
            return XInputSetState(userIndex, ref vibration);
        }

        /// <summary>
        /// Retrieves the capabilities and features of a connected controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3.</param>
        /// <param name="flags">Input flags that identify the controller type. 
        /// If this value is 0, then the capabilities of all controllers connected to the system are returned. 
        /// Currently, only one value is supported: <see cref="GetCapabilitiesFlags.Gamepad"/> - Limit the query to devices of Xbox controller type.</param>
        /// <param name="capabilities">An <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If the controller is not connected, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        public static Error GetCapabilities(UserIndex userIndex, GetCapabilitiesFlags flags, out Capabilities capabilities)
        {
            return XInputGetCapabilities(userIndex, flags, out capabilities);
        }

        /// <summary>
        /// Sets the reporting state of XInput.
        /// </summary>
        /// <param name="enable">If enable is FALSE, XInput will only send neutral data in response to <see cref="GetState(UserIndex, out State)"/> (all buttons up, axes centered, and triggers at 0). 
        /// <see cref="SetState(UserIndex, ref Vibration)"/> calls will be registered but not sent to the device. Sending any value other than FALSE will restore reading and writing functionality to normal.</param>
        public static void Enable(bool enable)
        {
            XInputEnable(enable);
        }

        /// <summary>
        /// Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the gamer associated with the device.</param>
        /// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
        /// <param name="renderCount">Size, in wide-chars, of the render device ID string buffer.</param>
        /// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
        /// <param name="captureCount">Size, in wide-chars, of capture device ID string buffer.</param>
        /// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="Error.Success"/>.
        /// If there is no headset connected to the controller, the function will also retrieve <see cref="Error.Success"/> with null as the values for renderDeviceId and captureDeviceId.
        /// If the controller port device is not physically connected, the function will return <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, it will return a valid Win32 error code.</returns>
        public static Error GetAudioDeviceIds(UserIndex userIndex, out string renderDeviceId,
            ref uint renderCount, out string captureDeviceId, ref uint captureCount)
        {
            return XInputGetAudioDeviceIds(userIndex, out renderDeviceId, ref renderCount, out captureDeviceId, ref captureCount);    
        }

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–<see cref="UserIndex.MaxCount"/> − 1.</param>
        /// <param name="devType">Specifies which device associated with this user index should be queried. Must be <see cref="BatteryDeviceType.Gamepad"/> or <see cref="BatteryDeviceType.Headset"/>.</param>
        /// <param name="batteryInformation">An <see cref="BatteryInformation"/> structure that receives the battery information.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.</returns>
        public static Error GetBatteryInformation(UserIndex userIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation)
        {
            return XInputGetBatteryInformation(userIndex, devType, out batteryInformation);
        }

        /// <summary>
        /// Retrieves a gamepad input event.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device.
        /// Can be a value in the range 0–<see cref="UserIndex.MaxCount"/> − 1, or <see cref="UserIndex.Any"/> to fetch the next available input event from any user.</param>
        /// <param name="keystroke">An <see cref="KeyStroke"/> structure that receives an input event.</param>
        /// <returns>If the function succeeds, the return value is <see cref="Error.Success"/>.
        /// If no new keys have been pressed, the return value is <see cref="Error.Empty"/>.
        /// If the controller is not connected or the user has not activated it, the return value is <see cref="Error.DeviceNotConnected"/>.
        /// If the function fails, the return value is an error code defined in Winerror.h.</returns>
        public static Error GetKeystroke(UserIndex userIndex, out KeyStroke keystroke)
        {
            return XInputGetKeystroke(userIndex, GetKeyStrokeFlags.Reserved, out keystroke);
        }

        #endregion
    }
}
