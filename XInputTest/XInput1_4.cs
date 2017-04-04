
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using dword = System.UInt32;
using word = System.UInt16;
using wchar = System.UInt16;

#endregion

namespace XInputTest
{
    public class XInput1_4
    {
        #region Constants

        //
        // Error Codes
        //
        public static dword ERROR_SUCCESS = 0;
        public static dword ERROR_DEVICE_NOT_CONNECTED = 1167;
        public static dword ERROR_EMPTY = 4306;

        //
        // Device types available in XINPUT_CAPABILITIES
        //
        public static byte XINPUT_DEVTYPE_GAMEPAD = 0x01;

        //
        // Device subtypes available in XINPUT_CAPABILITIES
        //
        // #if (_WIN32_WINNT >= _WIN32_WINNT_WIN8)
        public static byte XINPUT_DEVSUBTYPE_UNKNOWN = 0x00;
        public static byte XINPUT_DEVSUBTYPE_WHEEL = 0x02;
        public static byte XINPUT_DEVSUBTYPE_ARCADE_STICK = 0x03;
        public static byte XINPUT_DEVSUBTYPE_FLIGHT_STICK = 0x04;
        public static byte XINPUT_DEVSUBTYPE_DANCE_PAD = 0x05;
        public static byte XINPUT_DEVSUBTYPE_GUITAR = 0x06;
        public static byte XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = 0x07;
        public static byte XINPUT_DEVSUBTYPE_DRUM_KIT = 0x08;
        public static byte XINPUT_DEVSUBTYPE_GUITAR_BASS = 0x0B;
        public static byte XINPUT_DEVSUBTYPE_ARCADE_PAD = 0x13;
        // #endif //(_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        //
        // Flags for XINPUT_CAPABILITIES
        //
        public static word XINPUT_CAPS_VOICE_SUPPORTED = 0x0004;

        // #if (_WIN32_WINNT >= _WIN32_WINNT_WIN8)
        public static word XINPUT_CAPS_FFB_SUPPORTED = 0x0001;
        public static word XINPUT_CAPS_WIRELESS = 0x0002;
        public static word XINPUT_CAPS_PMD_SUPPORTED = 0x0008;
        public static word XINPUT_CAPS_NO_NAVIGATION = 0x0010;
        // #endif //(_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        //
        // Constants for gamepad buttons
        //
        public static word XINPUT_GAMEPAD_DPAD_UP = 0x0001;
        public static word XINPUT_GAMEPAD_DPAD_DOWN = 0x0002;
        public static word XINPUT_GAMEPAD_DPAD_LEFT = 0x0004;
        public static word XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008;
        public static word XINPUT_GAMEPAD_START = 0x0010;
        public static word XINPUT_GAMEPAD_BACK = 0x0020;
        public static word XINPUT_GAMEPAD_LEFT_THUMB = 0x0040;
        public static word XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080;
        public static word XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100;
        public static word XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200;
        public static word XINPUT_GAMEPAD_A = 0x1000;
        public static word XINPUT_GAMEPAD_B = 0x2000;
        public static word XINPUT_GAMEPAD_X = 0x4000;
        public static word XINPUT_GAMEPAD_Y = 0x8000;

        //
        // Gamepad thresholds
        //
        public static short XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;
        public static short XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;
        public static byte XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;

        //
        // Flags to pass to XInputGetCapabilities
        //
        public static dword XINPUT_FLAG_GAMEPAD = 0x00000001;

        //#if (_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        //
        // Devices that support batteries
        //
        public static byte BATTERY_DEVTYPE_GAMEPAD = 0x00;
        public static byte BATTERY_DEVTYPE_HEADSET = 0x01;

        //
        // Flags for battery status level
        //
        public static byte BATTERY_TYPE_DISCONNECTED = 0x00;    // This device is not connected
        public static byte BATTERY_TYPE_WIRED = 0x01;    // Wired device, no battery
        public static byte BATTERY_TYPE_ALKALINE = 0x02;    // Alkaline battery source
        public static byte BATTERY_TYPE_NIMH = 0x03;    // Nickel Metal Hydride battery source
        public static byte BATTERY_TYPE_UNKNOWN = 0xFF;    // Cannot determine the battery type

        // These are only valid for wireless, connected devices, with known battery types
        // The amount of use time remaining depends on the type of device.
        public static byte BATTERY_LEVEL_EMPTY = 0x00;
        public static byte BATTERY_LEVEL_LOW = 0x01;
        public static byte BATTERY_LEVEL_MEDIUM = 0x02;
        public static byte BATTERY_LEVEL_FULL = 0x03;

        //#endif //(_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        //
        // User index definitions
        //
        public static dword XUSER_MAX_COUNT = 4;
        public static dword XUSER_INDEX_ANY = 0x000000FF;

        //#if (_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        //
        // Codes returned for the gamepad keystroke
        //

        public static word VK_PAD_A = 0x5800;
        public static word VK_PAD_B = 0x5801;
        public static word VK_PAD_X = 0x5802;
        public static word VK_PAD_Y = 0x5803;
        public static word VK_PAD_RSHOULDER = 0x5804;
        public static word VK_PAD_LSHOULDER = 0x5805;
        public static word VK_PAD_LTRIGGER = 0x5806;
        public static word VK_PAD_RTRIGGER = 0x5807;

        public static word VK_PAD_DPAD_UP = 0x5810;
        public static word VK_PAD_DPAD_DOWN = 0x5811;
        public static word VK_PAD_DPAD_LEFT = 0x5812;
        public static word VK_PAD_DPAD_RIGHT = 0x5813;
        public static word VK_PAD_START = 0x5814;
        public static word VK_PAD_BACK = 0x5815;
        public static word VK_PAD_LTHUMB_PRESS = 0x5816;
        public static word VK_PAD_RTHUMB_PRESS = 0x5817;

        public static word VK_PAD_LTHUMB_UP = 0x5820;
        public static word VK_PAD_LTHUMB_DOWN = 0x5821;
        public static word VK_PAD_LTHUMB_RIGHT = 0x5822;
        public static word VK_PAD_LTHUMB_LEFT = 0x5823;
        public static word VK_PAD_LTHUMB_UPLEFT = 0x5824;
        public static word VK_PAD_LTHUMB_UPRIGHT = 0x5825;
        public static word VK_PAD_LTHUMB_DOWNRIGHT = 0x5826;
        public static word VK_PAD_LTHUMB_DOWNLEFT = 0x5827;

        public static word VK_PAD_RTHUMB_UP = 0x5830;
        public static word VK_PAD_RTHUMB_DOWN = 0x5831;
        public static word VK_PAD_RTHUMB_RIGHT = 0x5832;
        public static word VK_PAD_RTHUMB_LEFT = 0x5833;
        public static word VK_PAD_RTHUMB_UPLEFT = 0x5834;
        public static word VK_PAD_RTHUMB_UPRIGHT = 0x5835;
        public static word VK_PAD_RTHUMB_DOWNRIGHT = 0x5836;
        public static word VK_PAD_RTHUMB_DOWNLEFT = 0x5837;

        //
        // Flags used in XINPUT_KEYSTROKE
        //
        public static word XINPUT_KEYSTROKE_KEYDOWN = 0x0001;
        public static word XINPUT_KEYSTROKE_KEYUP = 0x0002;
        public static word XINPUT_KEYSTROKE_REPEAT = 0x0004;

//#endif //(_WIN32_WINNT >= _WIN32_WINNT_WIN8)

        #endregion

        #region Structs

        public struct XINPUT_BATTERY_INFORMATION
        {
            byte BatteryType;
            byte BatteryLevel;
        }

        public struct XINPUT_GAMEPAD
        {
            word wButtons;
            byte bLeftTrigger;
            byte bRightTrigger;
            short sThumbLX;
            short sThumbLY;
            short sThumbRX;
            short sThumbRY;
        }

        public struct XINPUT_KEYSTROKE
        {
            word VirtualKey;
            wchar Unicode;
            word Flags;
            byte UserIndex;
            byte HidCode;
        }

        public struct XINPUT_STATE
        {
            dword dwPacketNumber;
            XINPUT_GAMEPAD Gamepad;
        }

        public struct XINPUT_VIBRATION
        {
            word wLeftMotorSpeed;
            word wRightMotorSpeed;
        }

        public struct XINPUT_CAPABILITIES
        {
            byte Type;
            byte SubType;
            word Flags;
            XINPUT_GAMEPAD Gamepad;
            XINPUT_VIBRATION Vibration;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="dwUserIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="pState">Pointer to an <see cref="XINPUT_STATE"/> structure that receives the current state of the controller.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS. 
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in Winerror.h. The function does not use SetLastError to set the calling thread's last-error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetState(dword dwUserIndex, out XINPUT_STATE pState);

        /// <summary>
        /// Sends data to a connected controller. This function is used to activate the vibration function of a controller.
        /// </summary>
        /// <param name="dwUserIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="pVibration">Pointer to an <see cref="XINPUT_VIBRATION"/> structure containing the vibration information to send to the controller.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in WinError.h.The function does not use SetLastError to set the calling thread's last-error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputSetState(dword dwUserIndex, ref XINPUT_VIBRATION pVibration);

        /// <summary>
        /// Retrieves the capabilities and features of a connected controller.
        /// </summary>
        /// <param name="dwUserIndex">Index of the user's controller. Can be a value in the range 0–3</param>
        /// <param name="dwFlags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: <see cref="XINPUT_FLAG_GAMEPAD"/> - Limit query to devices of Xbox 360 Controller type.</param>
        /// <param name="pCapabilities">Pointer to an <see cref="XINPUT_CAPABILITIES"/> structure that receives the controller capabilities.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If the controller is not connected, the return value is ERROR_DEVICE_NOT_CONNECTED.
        /// If the function fails, the return value is an error code defined in WinError.h.The function does not use SetLastError to set the calling thread's last-error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetCapabilities(dword dwUserIndex, dword dwFlags, out XINPUT_CAPABILITIES pCapabilities);

        /// <summary>
        /// Sets the reporting state of XInput.
        /// </summary>
        /// <param name="enable">If enable is FALSE, XInput will only send neutral data in response to XInputGetState (all buttons up, axes centered, and triggers at 0). XInputSetState calls will be registered but not sent to the device. Sending any value other than FALSE will restore reading and writing functionality to normal.</param>
        [DllImport("xinput1_4.DLL")]
        public static extern void XInputEnable(System.Boolean enable);

        /// <summary>
        /// Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
        /// </summary>
        /// <param name="dwUserIndex">Index of the gamer associated with the device.</param>
        /// <param name="pRenderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
        /// <param name="pRenderCount">Size, in wide-chars, of the render device ID string buffer.</param>
        /// <param name="pCaptureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
        /// <param name="pCaptureCount">Size, in wide-chars, of capture device ID string buffer.</param>
        /// <returns></returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetAudioDeviceIds(dword dwUserIndex, [MarshalAs(UnmanagedType.LPWStr)] ref string pRenderDeviceId,
            ref uint pRenderCount, [MarshalAs(UnmanagedType.LPWStr)] ref string pCaptureDeviceId, ref uint pCaptureCount);

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="dwUserIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–XUSER_MAX_COUNT − 1.</param>
        /// <param name="devType">Specifies which device associated with this user index should be queried. Must be BATTERY_DEVTYPE_GAMEPAD or BATTERY_DEVTYPE_HEADSET.</param>
        /// <param name="pBatteryInformation">Pointer to an <see cref="XINPUT_BATTERY_INFORMATION"/> structure that receives the battery information.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetBatteryInformation(dword dwUserIndex, byte devType, out XINPUT_BATTERY_INFORMATION pBatteryInformation);

        /// <summary>
        /// Retrieves a gamepad input event.
        /// </summary>
        /// <param name="dwUserIndex">Index of the signed-in gamer associated with the device. Can be a value in the range 0–XUSER_MAX_COUNT − 1, or XUSER_INDEX_ANY to fetch the next available input event from any user.</param>
        /// <param name="dwReserved">Reserved</param>
        /// <param name="pKeystroke">Pointer to an <see cref="XINPUT_KEYSTROKE"/> structure that receives an input event.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
        /// If no new keys have been pressed, the return value is ERROR_EMPTY.
        /// If the controller is not connected or the user has not activated it, the return value is ERROR_DEVICE_NOT_CONNECTED.See the Remarks section below.
        /// If the function fails, the return value is an error code defined in Winerror.h.The function does not use SetLastError to set the calling thread's last-error code.</returns>
        [DllImport("xinput1_4.DLL")]
        public static extern dword XInputGetKeystroke(dword dwUserIndex, dword dwReserved, out XINPUT_KEYSTROKE pKeystroke);

        #endregion
    }
}
