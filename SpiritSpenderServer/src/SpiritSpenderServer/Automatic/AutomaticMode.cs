﻿namespace SpiritSpenderServer.Automatic;

using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.Axis;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderControl;
using SpiritSpenderServer.Persistence.Positions;
using System.Reactive.Linq;
using System.Reactive.Subjects;

public class AutomaticMode : IAutomaticMode
{
    private readonly BehaviorSubject<Status> _currentStatus;
    private readonly IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;
    private readonly IEmergencyStop _emergencyStop;
    private readonly ISpiritDispenserControl _spiritDispenserControl;
    private readonly IAxis _X_Axis;
    private readonly IAxis _Y_Axis;
    private bool _areComponentsReady;

    public AutomaticMode(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository,
                         IXAxis xAxis,
                         IYAxis yAxis,
                         ISpiritDispenserControl spiritDispenserControl,
                         IEmergencyStop emergencyStop)
    {
        _currentStatus = new BehaviorSubject<Status>(Status.NotReady);
        _shotGlassPositionSettingRepository = shotGlassPositionSettingRepository;
        _X_Axis = xAxis;
        _Y_Axis = yAxis;
        _spiritDispenserControl = spiritDispenserControl;
        _emergencyStop = emergencyStop;
        _emergencyStop.EmergencyStopPressedChanged += (estop) => CalculateStatuts();

        var components = new List<IObservable<Status>>
        {
            _X_Axis.GetStatusObservable(),
            _Y_Axis.GetStatusObservable(),
            _spiritDispenserControl.GetStatusObservable()
        };


        components.CombineLatest(lastStates => lastStates.All(state => state == Status.Ready))
            .Subscribe(areComponentsReady =>
            {
                _areComponentsReady = areComponentsReady;
                CalculateStatuts();
            });
    }

    public event Action? OneShotPoured;

    public IObservable<Status> GetStatusObservable() => _currentStatus.AsObservable();

    public async Task ReferenceAllAxis()
    {
        var taskRefX = _X_Axis.ReferenceDriveAsync();
        var taskRefY = _Y_Axis.ReferenceDriveAsync();
        var taskRefDispenser = _spiritDispenserControl.ReferenceDriveAsync();

        await taskRefX;
        await taskRefY;
        await taskRefDispenser;
    }

    public async Task GoToBottleChange()
    {
        const int BOTTLE_CHANGE_POSITION = 6;
        var bottleChangePosition = await _shotGlassPositionSettingRepository.GetSettingAsync(BOTTLE_CHANGE_POSITION);
        await DriveToPositionAsync(bottleChangePosition.Position);
        await _spiritDispenserControl.GoToBottleChangePosition();
    }

    public async Task ReleaseTheSpiritAsync()
    {
        if (_currentStatus.Value != Status.Ready)
        {
            return;
        }

        _currentStatus.OnNext(Status.Running);

        var positionSettings = await _shotGlassPositionSettingRepository.GetAllSettingsAsync();
        var positionsWithGlasses = positionSettings.Where(p => p.Quantity != Quantity.Empty).ToList();
        var optimizedRoute = GetOptimizedRoute(positionsWithGlasses);

        foreach (var position in optimizedRoute)
        {
            var positionSetting = positionsWithGlasses.First(p => p.Position == position);
            if (_emergencyStop.EmergencyStopPressed)
            {
                break;
            }

            switch (positionSetting.Quantity)
            {
                case Quantity.OneShot:
                    await DriveToPositionAsync(positionSetting.Position);
                    await _spiritDispenserControl.FillGlas();
                    OneShotPoured?.Invoke();
                    break;
                case Quantity.DoubleShot:
                    await DriveToPositionAsync(positionSetting.Position);
                    await _spiritDispenserControl.FillGlas();
                    OneShotPoured?.Invoke();
                    await _spiritDispenserControl.FillGlas();
                    OneShotPoured?.Invoke();
                    break;
                case Quantity.Empty:
                    // do nothing
                    break;
            }
        }

        CalculateStatuts(true);
    }

    private List<Position> GetOptimizedRoute(List<ShotGlassPositionSetting> positionSettings)
    {
        var currentPosition = new Position() { X = _X_Axis.CurrentPosition, Y = _Y_Axis.CurrentPosition };
        var positions = positionSettings.Select(posSetting => posSetting.Position).ToList();
        var optimizedRoute = RouteOptimizer.RouteOptimizer.GetFastestWayToGetDrunk(currentPosition, positions, _X_Axis.DriveSetting, _Y_Axis.DriveSetting);

        return optimizedRoute;
    }

    public async Task DriveToPositionAsync(Position? position)
    {
        if (position != null)
        {
            var taskX = _X_Axis.DriveToPositionAsync(position.X);
            var taskY = _Y_Axis.DriveToPositionAsync(position.Y);

            await taskX;
            await taskY;
        }
    }

    private void CalculateStatuts(bool resetState = false)
    {
        if (_currentStatus.Value != Status.Running || resetState)
        {
            if (_emergencyStop.EmergencyStopPressed)
                _currentStatus.OnNext(Status.Error);

            if (IsStartPossible())
                _currentStatus.OnNext(Status.Ready);
            else
                _currentStatus.OnNext(Status.NotReady);
        }
    }

    private bool IsStartPossible()
    {
        var startPossible = !_emergencyStop.EmergencyStopPressed &&
                            _areComponentsReady;

        return startPossible;
    }
}
