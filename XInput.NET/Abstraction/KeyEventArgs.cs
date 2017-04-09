
#region Using Directives

using System;

#endregion

namespace XInputNET.Abstraction
{
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
}
