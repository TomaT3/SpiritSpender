using MotionCalc;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;

namespace SpiritSpenderServer.Automatic.RouteOptimizer
{
    public static class RouteOptimizer
    {
        public static List<Position> GetFastestWayToGetDrunk(Position currentPosition, List<Position> positionsToTravelTo, DriveSetting xAxis, DriveSetting yAxis)
        {
            List<Position> getDrunkFastWayToTravel = new();

            while (positionsToTravelTo.Any())
            {
                var nextPosition = GetNextFastestReachablePosition(currentPosition, positionsToTravelTo, xAxis, yAxis);
                currentPosition = nextPosition;
                positionsToTravelTo.Remove(currentPosition);
                getDrunkFastWayToTravel.Add(currentPosition);
            }

            return getDrunkFastWayToTravel;
        }

        private static Position GetNextFastestReachablePosition(Position currentPosition, List<Position> positionsToTravelTo, DriveSetting xAxis, DriveSetting yAxis)
        {
            bool firstIteration = true;
            Duration fastestWay = default(Duration);
            var nextPosition = positionsToTravelTo.First();

            foreach (var position in positionsToTravelTo)
            {
                var xDistance = currentPosition.X - position.X;
                var yDistance = currentPosition.Y - position.Y;

                var timeX = MotionCalculatorHelper.GetTimeForDistance(xDistance.Millimeters, xAxis.Acceleration.MillimetersPerSecondSquared, xAxis.MaxSpeed.MillimetersPerSecond);
                var timeY = MotionCalculatorHelper.GetTimeForDistance(yDistance.Millimeters, yAxis.Acceleration.MillimetersPerSecondSquared, yAxis.MaxSpeed.MillimetersPerSecond);

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
}
