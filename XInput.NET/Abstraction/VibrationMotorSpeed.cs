
#region Using Directives

using System;

#endregion

namespace XInputNET.Abstraction
{
    /// <summary>
    /// The <see cref="VibrationMotorSpeed"/> class is used to specify vibration configuration values for the gamepad.
    /// </summary>
    public class VibrationMotorSpeed
    {
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="VibrationMotorSpeed"/> instance with motor speed values for both drivers.
        /// </summary>
        /// <param name="motorSpeedLeft">The left vibration motor speed value. Can be in the range of 0.0 to 1.0.</param>
        /// <param name="motorSpeedRight">The right vibration motor speed value. Can be in the range of 0.0 to 1.0.</param>
        public VibrationMotorSpeed(double motorSpeedLeft, double motorSpeedRight)
        {
            this.LeftMotorSpeed = motorSpeedLeft;
            this.RightMotorSpeed = motorSpeedRight;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Contains the backing value for the <see cref="LeftMotorSpeed"/> property.
        /// </summary>
        private double leftMotorSpeed;

        /// <summary>
        /// Gets the value left vibration motor speed.
        /// Can be in the range of 0.0 to 1.0.
        /// </summary>
        public double LeftMotorSpeed
        {
            get
            {
                return this.leftMotorSpeed;
            }
            private set
            {
                this.leftMotorSpeed = Math.Max(Math.Min(value, 1.0), 0.0);
            }
        }

        /// <summary>
        /// Contains the backing value for the <see cref="RightMotorSpeed"/> property.
        /// </summary>
        private double rightMotorSpeed;

        /// <summary>
        /// Gets the value right vibration motor speed.
        /// Can be in the range of 0.0 to 1.0.
        /// </summary>
        public double RightMotorSpeed
        {
            get
            {
                return this.rightMotorSpeed;
            }
            private set
            {
                this.rightMotorSpeed = Math.Max(Math.Min(value, 1.0), 0.0);
            }
        }

        #endregion
    }
}
