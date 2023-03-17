namespace SpiritSpenderServer.NC_Communication
{
    using System.ComponentModel;
    using System.Composition;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using HardwareControl;
    using Persistence.Positions;
    using SpiritSpenderServer.HardwareControl.EmergencyStop;
    using SpiritSpenderServer.NC_Communication.AxisConfigurations;
    using UnitsNet;

    public record AxisPosition(string AxisName, Length Position);

    public enum NcState
    {
        Unknown,
        Idle,
        Run,
        Home,
        Hold,
        Alarm
    }

    public interface INcCommunication : IComponentWithStatus
    {
        event Action<string>? MessageReceived;
        event Action<Position>? PositionChanged;
        NcState CurrentState { get; }
        Task InitAsync();
        Position CurrentAxisPosition { get; }
        void DriveTo(AxisPosition[] axisPositions);
        void ReferenceAllAxis();
        void ReferenceAxis(string axisName);
        IAxisConfiguration GetAxisConfiguration(Axis axis);
        void SendCommand(string command);
    }

    public enum Axis
    {
        X,
        Y
    }

    public class NcCommunication : INcCommunication
    {
        private const int REPORT_INTERVAL_MS = 100;
        private readonly ISerialCommunication _serialCommunication;
        private readonly IAxisConfiguration _xAxisConfiguration;
        private readonly IAxisConfiguration _yAxisConfiguration;
        private readonly IEmergencyStop _emergencyStop;
        private BehaviorSubject<Status> _statusSubject;

        public NcCommunication(ISerialCommunication serialCommunication, 
            IXAxisConfiguration xAxisConfiguration,
            IYAxisConfiguration yAxisConfiguration,
            IEmergencyStop emergencyStop)
        {
            _serialCommunication = serialCommunication;
            _serialCommunication.MessageReceived += MessageReceivedHandler;
            _xAxisConfiguration = xAxisConfiguration;
            _yAxisConfiguration = yAxisConfiguration;
            _emergencyStop = emergencyStop;
            
            CurrentAxisPosition = new Position();

            _statusSubject = new BehaviorSubject<Status>(emergencyStop.EmergencyStopPressed ? Status.Error : Status.NotReady);
            _emergencyStop.EmergencyStopPressedChanged += EmergencyStopPressedChanged;
        }

        public event Action<string>? MessageReceived;
        public event Action<Position>? PositionChanged;

        public NcState CurrentState { get; private set; }
        public async Task InitAsync()
        {
            await _serialCommunication.StartAsync();
            SetReportInterval(REPORT_INTERVAL_MS);
        }

        public void SetReportInterval(int interval)
        {
            var setIntervalString = $"$Report/Interval={interval}{Environment.NewLine}";
            _serialCommunication.Write(setIntervalString);
        }

        public Position CurrentAxisPosition { get; private set; }

        private void MessageReceivedHandler(string message)
        {
            MessageReceived?.Invoke(message);
            DecodeFluidNcMessage(message);
        }

        private void DecodeFluidNcMessage(string message)
        {
            if (message != null && message.StartsWith("<"))
            {
                try
                {
                    string[] parts = message.TrimStart('<').TrimEnd('>').Split('|');
                    var newState = ToNcState(parts[0]);
                    SetNcState(newState);
                    string[] positions = parts[1].Substring(5).Split(',');
                    var currentXPosition = Length.FromMillimeters(double.Parse(positions[0]));
                    var currentYPosition = Length.FromMillimeters(double.Parse(positions[1]));
                    CurrentAxisPosition = new Position() { X = currentXPosition, Y = currentYPosition };
                    PositionChanged?.Invoke(CurrentAxisPosition);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                //string[] feedrate = parts[2].Substring(3).Split(',');
                //int fRateXAxis = int.Parse(feedrate[0]);
                //int fRateYAxis = int.Parse(feedrate[1]);
            }
        }

        private void SetNcState(NcState state)
        {
            Console.WriteLine($"Set new nc state: {state}");
            CurrentState = state;
            switch (CurrentState)
            {
                case NcState.Unknown:
                    _statusSubject.OnNext(Status.NotReady);
                    break;
                case NcState.Idle:
                    _statusSubject.OnNext(Status.Ready);
                    break;
                case NcState.Run:
                case NcState.Home:
                    _statusSubject.OnNext(Status.Running);
                    break;
                case NcState.Hold:
                case NcState.Alarm:
                    _statusSubject.OnNext(Status.Error);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void DriveTo(AxisPosition[] axisPositions)
        {
            var ncCommand = NcCommandCreator.FastPositioning(axisPositions);
            SendCommandToSerialInterface(ncCommand);

            while (!AreDestinationPositionsReached(axisPositions))
            {
                Thread.Sleep(50);
            }
        }

        public void ReferenceAllAxis()
        {
            SetReportInterval(REPORT_INTERVAL_MS);
            SendCommandToSerialInterface("$H");
        }

        public void ReferenceAxis(string axisName)
        {
            SendCommandToSerialInterface($"$H{axisName}");
        }

        public IAxisConfiguration GetAxisConfiguration(Axis axis) => axis switch
        {
            Axis.X => _xAxisConfiguration,
            Axis.Y => _yAxisConfiguration,
            _ => throw new InvalidEnumArgumentException(),
        };

        public void SendCommand(string command)
        {
            SendCommandToSerialInterface(command);
        }

        private void SendCommandToSerialInterface(string command)
        {
            _serialCommunication.Write($"{command}{Environment.NewLine}");
        }

        private bool AreDestinationPositionsReached(AxisPosition[] axisPositions)
        {
            var xAxisPosition = axisPositions.FirstOrDefault(a => a.AxisName == "X");
            var yAxisPosition= axisPositions.FirstOrDefault(a => a.AxisName == "Y");

            var xPositionReached = IsPositionReached(xAxisPosition, CurrentAxisPosition.X);
            var yPositionsReached = IsPositionReached(yAxisPosition, CurrentAxisPosition.Y);

            return xPositionReached && yPositionsReached;
        }

        private bool IsPositionReached(AxisPosition? axisPosition, Length destinationPosition)
        {
            if ((axisPosition != null && axisPosition.Position == destinationPosition)
                || axisPosition == null)
            {
                return true;
            }

            return false;
        }

        private NcState ToNcState(string state) => state.ToLower() switch
        {
            "home" => NcState.Home,
            "idle" => NcState.Idle,
            "run" => NcState.Run,
            "hold" => NcState.Hold,
            "alarm" => NcState.Alarm,
            _ => NcState.Unknown
        };


        public IObservable<Status> GetStatusObservable()
        {
            return _statusSubject.AsObservable();
        }

        private void EmergencyStopPressedChanged(bool emergencyStopPressed)
        {
            if (emergencyStopPressed)
            {
                _statusSubject.OnNext(Status.Error);
            }
            else
            {
                _statusSubject.OnNext(Status.NotReady);
            }
        }

    }

    public static class NcCommandCreator
    {
        private const string FAST_POSITIONING = "G0";

        public static string FastPositioning(AxisPosition[] axisPositions)
        {
            var ncCommand = FAST_POSITIONING;
            foreach (var axisPosition in axisPositions)
            {
                ncCommand += $" {axisPosition.AxisName}{axisPosition.Position.Millimeters}";
            }

            return ncCommand;
        }
    }
}
