//namespace SpiritSpenderServer.HardwareControl.Axis;

//using SpiritSpenderServer.Persistence.DriveSettings;
//using System;
//using UnitsNet;

//public interface IAxis : IComponentWithStatus
//{
//    event Action<string, Length> PositionChanged;
//    public string Name { get; }
//    DriveSetting DriveSetting { get; }
//    Length CurrentPosition { get; }
//    Task InitAsync();
//    Task UpdateSettingsAsync(DriveSetting setting);
//    //void SetPosition(Length position);
//    Task DriveDistanceAsync(Length distance);
//    Task DriveToPositionAsync(Length position);
//    Task ReferenceDriveAsync();
//}
