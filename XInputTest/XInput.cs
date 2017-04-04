
#region Using Directives

using System.Runtime.InteropServices;
using dword = System.UInt32;
using word = System.UInt16;
using wchar = System.UInt16;

#endregion

namespace XInputTest
{
    public class XInput
    {
        #region Constants

        /// <summary>
        /// The error code that signals success.
        /// </summary>
        public static dword ERROR_SUCCESS = 0;

        /// <summary>
        /// The error code that signals a not connected device.
        /// </summary>
        public static dword ERROR_DEVICE_NOT_CONNECTED = 1167;

        /// <summary>
        /// The error code that signals that no new buttons have been pressed.
        /// </summary>
        public static dword ERROR_EMPTY = 4306;

        /// <summary>
        /// Deadzone value of the left analog stick.
        /// </summary>
        public static short GAMEPAD_LEFT_THUMB_DEADZONE = 7849;

        /// <summary>
        /// Deadzone value of the right analog stick.
        /// </summary>
        public static short GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;

        /// <summary>
        /// Threshold value of the triggers.
        /// </summary>
        public static byte GAMEPAD_TRIGGER_THRESHOLD = 30;

        #endregion

        #region Enumerations

        /// <summary>
        /// Device types available in <see cref="XINPUT_CAPABILITIES"/>.
        /// </summary>
        public enum DEVTYPE : byte
        {
            GAMEPAD = 0x01
        }

        /// <summary>
        /// Device subtypes available in <see cref="XINPUT_CAPABILITIES"/>.
        /// </summary>
        public enum DEVSUBTYPE : byte
        {
            UNKNOWN = 0x00,
            WHEEL = 0x02,
            ARCADE_STICK = 0x03,
            FLIGHT_STICK = 0x04,
            DANCE_PAD = 0x05,
            GUITAR = 0x06,
            GUITAR_ALTERNATE = 0x07,
            DRUM_KIT = 0x08,
            GUITAR_BASS = 0x0B,
            ARCADE_PAD = 0x13
        }

        /// <summary>
        /// Flags for <see cref="XINPUT_CAPABILITIES"/>.
        /// </summary>
        public enum CAPS : word
        {
            VOICE_SUPPORTED = 0x0004,
            FFB_SUPPORTED = 0x0001,
            WIRELESS = 0x0002,
            PMD_SUPPORTED = 0x0008,
            NO_NAVIGATION = 0x0010
        }

        /// <summary>
        /// Gamepad buttons.
        /// </summary>
        public enum GAMEPAD_BUTTONS : word
        {
            DPAD_UP = 0x0001,
            DPAD_DOWN = 0x0002,
            DPAD_LEFT = 0x0004,
            DPAD_RIGHT = 0x0008,
            START = 0x0010,
            BACK = 0x0020,
            LEFT_THUMB = 0x0040,
            RIGHT_THUMB = 0x0080,
            LEFT_SHOULDER = 0x0100,
            RIGHT_SHOULDER = 0x0200,
            GAMEPAD_A = 0x1000,
            GAMEPAD_B = 0x2000,
            GAMEPAD_X = 0x4000,
            GAMEPAD_Y = 0x8000,
        }

        /// <summary>
        /// Flags to pass to <see cref="XInputGetCapabilities(XUSER_INDEX, GET_CAPABILITIES_FLAGS, out XINPUT_CAPABILITIES)"/>.
        /// </summary>
        public enum GET_CAPABILITIES_FLAGS : dword
        {
            ALL_GAMEPADS = 0,
            GAMEPAD = 0x00000001
        }

        /// <summary>
        /// Devices that support batteries.
        /// </summary>
        public enum BATTERY_DEVTYPE : byte
        {
            BATTERY_DEVTYPE_GAMEPAD = 0x00,
            BATTERY_DEVTYPE_HEADSET = 0x01
        }

        /// <summary>
        /// Types of batteries.
        /// </summary>
        public enum BATTERY_TYPE : byte
        {
            /// <summary>
            /// This device is not connected.
            /// </summary>
            BATTERY_TYPE_DISCONNECTED = 0x00,
            /// <summary>
            /// Wired device, no battery.
            /// </summary>
            BATTERY_TYPE_WIRED = 0x01,
            /// <summary>
            /// Alkaline battery source.
            /// </summary>
            BATTERY_TYPE_ALKALINE = 0x02,
            /// <summary>
            /// Nickel Metal Hydride battery source.
            /// </summary>
            BATTERY_TYPE_NIMH = 0x03,
            /// <summary>
            /// Cannot determine the battery type.
            /// </summary>
            BATTERY_TYPE_UNKNOWN = 0xFF
        }

        /// <summary>
        /// Battery status levels. These are only valid for wireless, connected devices, with known battery types.
        /// The amount of use time remaining depends on the type of device. 
        /// </summary>
        public enum BATTERY_LEVEL : byte
        {
            BATTERY_LEVEL_EMPTY = 0x00,
            BATTERY_LEVEL_LOW = 0x01,
            BATTERY_LEVEL_MEDIUM = 0x02,
            BATTERY_LEVEL_FULL = 0x03
        }

        /// <summary>
        /// User index definitions.
        /// </summary>
        public enum XUSER_INDEX : dword
        {
            USER_0 = 0,
            USER_1 = 1,
            USER_2 = 2,
            USER_3 = 3,
            MAX_COUNT = 4,
            ANY = 0x000000FF
        }

        /// <summary>
        /// Codes returned for the gamepad keystroke.
        /// </summary>
        public enum KEYSTROKE_VK : word
        {
            PAD_A = 0x5800,
            PAD_B = 0x5801,
            PAD_X = 0x5802,
            PAD_Y = 0x5803,
            PAD_RSHOULDER = 0x5804,
            PAD_LSHOULDER = 0x5805,
            PAD_LTRIGGER = 0x5806,
            PAD_RTRIGGER = 0x5807,

            PAD_DPAD_UP = 0x5810,
            PAD_DPAD_DOWN = 0x5811,
            PAD_DPAD_LEFT = 0x5812,
            PAD_DPAD_RIGHT = 0x5813,
            PAD_START = 0x5814,
            PAD_BACK = 0x5815,
            PAD_LTHUMB_PRESS = 0x5816,
            PAD_RTHUMB_PRESS = 0x5817,

            PAD_LTHUMB_UP = 0x5820,
            PAD_LTHUMB_DOWN = 0x5821,
            PAD_LTHUMB_RIGHT = 0x5822,
            VK_PAD_LTHUMB_LEFT = 0x5823,
            PAD_LTHUMB_UPLEFT = 0x5824,
            PAD_LTHUMB_UPRIGHT = 0x5825,
            PAD_LTHUMB_DOWNRIGHT = 0x5826,
            PAD_LTHUMB_DOWNLEFT = 0x5827,

            PAD_RTHUMB_UP = 0x5830,
            PAD_RTHUMB_DOWN = 0x5831,
            PAD_RTHUMB_RIGHT = 0x5832,
            PAD_RTHUMB_LEFT = 0x5833,
            PAD_RTHUMB_UPLEFT = 0x5834,
            PAD_RTHUMB_UPRIGHT = 0x5835,
            PAD_RTHUMB_DOWNRIGHT = 0x5836,
            PAD_RTHUMB_DOWNLEFT = 0x5837
        }

        /// <summary>
        /// Flags used in <see cref="XINPUT_KEYSTROKE"/>.
        /// </summary>
        public enum KEYSTROKE_FLAGS : word
        {
            KEYDOWN = 0x0001,
            KEYUP = 0x0002,
            REPEAT = 0x0004
        }

        /// <summary>
        /// Flags used for <see cref="XInputGetKeystroke(XUSER_INDEX, dword, out XINPUT_KEYSTROKE)"/>.
        /// </summary>
        public enum GET_KEYSTROKE_FLAGS : dword
        {
            RESERVED = 0
        }

        #endregion

        #region Structs

        /// <summary>
        /// Contains information on battery type and charge state.
        /// </summary>
        public struct XINPUT_BATTERY_INFORMATION
        {
            /// <summary>
            /// The type of battery.
            /// </summary>
            BATTERY_TYPE BatteryType;

            /// <summary>
            /// The charge state of the battery. This value is only valid for wireless devices with a known battery type. 
            /// </summary>
            BATTERY_LEVEL BatteryLevel;
        }

        /// <summary>
        /// Describes the current state of the Xbox controller.
        /// </summary>
        public struct XINPUT_GAMEPAD
        {
            /// <summary>
            /// Bitmask of the device digital buttons. A set bit indicates that the corresponding button is pressed.
            /// </summary>
            GAMEPAD_BUTTONS buttons;

            /// <summary>
            /// The current value of the left trigger analog control. The value is between 0 and 255.
            /// </summary>
            byte leftTrigger;

            /// <summary>
            /// The current value of the right trigger analog control. The value is between 0 and 255.
            /// </summary>
            byte rightTrigger;

            /// <summary>
            /// Left thumbstick x-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick. 
            /// A value of 0 is centered. Negative values signify to the left. Positive values signify to the right. 
            /// The constant <see cref="GAMEPAD_LEFT_THUMB_DEADZONE"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            short thumbLX;

            /// <summary>
            /// Left thumbstick y-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify down. Positive values signify up.
            /// The constant <see cref="GAMEPAD_LEFT_THUMB_DEADZONE"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            short thumbLY;

            /// <summary>
            /// Right thumbstick x-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify to the left. Positive values signify to the right.
            /// The constant <see cref="GAMEPAD_RIGHT_THUMB_DEADZONE"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            short thumbRX;

            /// <summary>
            /// Right thumbstick y-axis value. 
            /// The value is between -32768 and 32767 describing the position of the thumbstick.
            /// A value of 0 is centered. Negative values signify down. Positive values signify ups.
            /// The constant <see cref="GAMEPAD_RIGHT_THUMB_DEADZONE"/> can be used as a positive and negative value to filter a thumbstick input.
            /// </summary>
            short thumbRY;
        }

        public struct XINPUT_KEYSTROKE
        {
            KEYSTROKE_VK virtualKey;
            wchar unicode;
            KEYSTROKE_FLAGS flags;
            XUSER_INDEX userIndex;
            byte hidCode;
        }

        public struct XINPUT_STATE
        {
            dword packetNumber;
            XINPUT_GAMEPAD gamepad;
        }

        public struct XINPUT_VIBRATION
        {
            word leftMotorSpeed;
            word rightMotorSpeed;
        }

        public struct XINPUT_CAPABILITIES
        {
            DEVTYPE type;
            DEVSUBTYPE subType;
            CAPS flags;
            XINPUT_GAMEPAD gamepad;
            XINPUT_VIBRATION vibration;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="state">An <see cref="XINPUT_STATE"/> structure that receives the current state of the controller.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. 
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in Winerror.h.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetState(XUSER_INDEX userIndex, out XINPUT_STATE state);

        /// <summary>
        /// Sends data to a connected controller. This function is used to activate the vibration function of a controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="vibration">An <see cref="XINPUT_VIBRATION"/> structure containing the vibration information to send to the controller.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputSetState(XUSER_INDEX userIndex, ref XINPUT_VIBRATION vibration);

        /// <summary>
        /// Retrieves the capabilities and features of a connected controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3.</param>
        /// <param name="flags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: <see cref="XINPUT_FLAG_GAMEPAD"/> - Limit query to devices of Xbox 360 Controller type.</param>
        /// <param name="capabilities">An <see cref="XINPUT_CAPABILITIES"/> structure that receives the controller capabilities.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in WinError.h.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetCapabilities(XUSER_INDEX userIndex, GET_CAPABILITIES_FLAGS flags, out XINPUT_CAPABILITIES capabilities);

        /// <summary>
        /// Sets the reporting state of XInput.
        /// </summary>
        /// <param name="enable">If enable is FALSE, XInput will only send neutral data in response to XInputGetState (all buttons up, axes centered, and triggers at 0). XInputSetState calls will be registered but not sent to the device. Sending any value other than FALSE will restore reading and writing functionality to normal.</param>
        [DllImport("xinput1_4.DLL")]
        public static extern void XInputEnable(bool enable);

        /// <summary>
        /// Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the gamer associated with the device.</param>
        /// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
        /// <param name="renderCount">Size, in wide-chars, of the render device ID string buffer.</param>
        /// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
        /// <param name="captureCount">Size, in wide-chars, of capture device ID string buffer.</param>
        /// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is ERROR_SUCCESS.
        /// If there is no headset connected to the controller, the function will also retrieve ERROR_SUCCESS with NULL as the values for pRenderDeviceId and pCaptureDeviceId.
        /// If the controller port device is not physically connected, the function will return ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, it will return a valid Win32 error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetAudioDeviceIds(XUSER_INDEX userIndex, [MarshalAs(UnmanagedType.LPWStr)] ref string renderDeviceId,
            ref uint renderCount, [MarshalAs(UnmanagedType.LPWStr)] ref string captureDeviceId, ref uint captureCount);

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–XUSER_INDEX.MAX_COUNT − 1.</param>
        /// <param name="devType">Specifies which device associated with this user index should be queried. Must be BATTERY_DEVTYPE.GAMEPAD or BATTERY_DEVTYPE.HEADSET.</param>
        /// <param name="batteryInformation">An <see cref="XINPUT_BATTERY_INFORMATION"/> structure that receives the battery information.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetBatteryInformation(XUSER_INDEX userIndex, BATTERY_DEVTYPE devType, out XINPUT_BATTERY_INFORMATION batteryInformation);

        /// <summary>
        /// Retrieves a gamepad input event.
        /// </summary>
        /// <param name="userIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–XUSER_MAX_COUNT − 1, or XUSER_INDEX_ANY to fetch the next available input event from any user.</param>
        /// <param name="reserved">Reserved. Must be value RESERVED, which is 0.</param>
        /// <param name="keystroke">An <see cref="XINPUT_KEYSTROKE"/> structure that receives an input event.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If no new keys have been pressed, the return value is ERROR_EMPTY.
        /// If the controller is not connected or the user has not activated it, the return value is ERROR_DEVICE_NOT_CONNECTED.See the Remarks section below.
        /// If the function fails, the return value is an error code defined in Winerror.h.The function does not use SetLastError to set the calling thread's last-error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetKeystroke(XUSER_INDEX userIndex, GET_KEYSTROKE_FLAGS reserved, out XINPUT_KEYSTROKE pKeystroke);

        #endregion
    }
}
