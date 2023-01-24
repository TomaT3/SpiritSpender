namespace SpiritSpenderServer.HardwareControl.SpiritSpenderControl;

using UnitsNet;

public interface ILinearMotor
{
    /// <summary>
    /// Drive motor backward for the given time
    /// </summary>
    /// <param name="drivingTime">The time to drive</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>true if driving was succesfull, otherwise false</returns>
    Task<bool> DriveBackwardAsync(Duration drivingTime, CancellationToken token);

    /// <summary>
    /// Drive motor forward for the given time
    /// </summary>
    /// <param name="drivingTime">The time to drive</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>true if driving was succesfull, otherwise false</returns>
    Task<bool> DriveForwardAsync(Duration drivingTime, CancellationToken token);
}
