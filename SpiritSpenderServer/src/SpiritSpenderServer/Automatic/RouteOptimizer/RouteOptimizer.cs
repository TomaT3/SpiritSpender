namespace SpiritSpenderServer.Automatic.RouteOptimizer;

using MotionCalc;
using NC_Communication;
using SpiritSpenderServer.NC_Communication.AxisConfigurations;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using UnitsNet;

public static class RouteOptimizer
{
    public static List<Position> GetFastestWayToGetDrunk(Position currentPosition, List<Position> positionsToTravelTo, IAxisConfiguration xAxisConfiguration, IAxisConfiguration yAxisConfiguration)
    {
        List<Position> getDrunkFastWayToTravel = new();

        while (positionsToTravelTo.Any())
        {
            var nextPosition = GetNextFastestReachablePosition(currentPosition, positionsToTravelTo, xAxisConfiguration, yAxisConfiguration);
            currentPosition = nextPosition;
            positionsToTravelTo.Remove(currentPosition);
            getDrunkFastWayToTravel.Add(currentPosition);
        }

        return getDrunkFastWayToTravel;
    }

    private static Position GetNextFastestReachablePosition(Position currentPosition, List<Position> positionsToTravelTo, IAxisConfiguration xAxisConfiguration, IAxisConfiguration yAxisConfiguration)
    {
        bool firstIteration = true;
        Duration fastestWay = default(Duration);
        var nextPosition = positionsToTravelTo.First();

        foreach (var position in positionsToTravelTo)
        {
            var xDistance = currentPosition.X - position.X;
            var yDistance = currentPosition.Y - position.Y;

            var timeX = MotionCalculatorHelper.GetTimeForDistance(xDistance.Millimeters, xAxisConfiguration.Acceleration.MillimetersPerSecondSquared, xAxisConfiguration.MaxSpeed.MillimetersPerSecond);
            var timeY = MotionCalculatorHelper.GetTimeForDistance(yDistance.Millimeters, yAxisConfiguration.Acceleration.MillimetersPerSecondSquared, yAxisConfiguration.MaxSpeed.MillimetersPerSecond);

            double timeForTravelinSeconds = timeX > timeY ? timeX : timeY;
            var timeForTravel = new Duration(timeForTravelinSeconds, UnitsNet.Units.DurationUnit.Second);

            if (timeForTravel < fastestWay
                || firstIteration)
            {
                firstIteration = false;
                fastestWay = timeForTravel;
                nextPosition = position;
            }
        }

        return nextPosition;
    }
}
