namespace MotionCalc
{
    using System;

    public static class MotionCalculatorHelper
    {
        public static double GetTimeForDistance(double distance, double acceleration, double travelSpeed)
        {
            double totalTime;
            distance = Math.Abs(distance);
            var isMaxSpeedReachable = IsMaxSpeedReached(distance, acceleration, travelSpeed);
            if (isMaxSpeedReachable)
            {
                var timeToReachMaxSpeed = GetTimeToReachTravelSpeed(acceleration, travelSpeed);
                var distanceNeededToAccelAndDeccel = 2 * GetDistanceForConstAcceleration(acceleration, timeToReachMaxSpeed);
                var distanceTravelledWithMaxSpeed = distance - distanceNeededToAccelAndDeccel;
                var timeWithMaxSpeed = GetTimeForConstVelocity(travelSpeed, distanceTravelledWithMaxSpeed);
                totalTime = timeToReachMaxSpeed + timeWithMaxSpeed + timeToReachMaxSpeed;
            }
            else
            {
                var halfDistance = distance / 2;
                var timeForAcceleration = GetTimeForConstAcceleration(halfDistance, acceleration);
                totalTime = 2 * timeForAcceleration;
            }

            return totalTime;
        }

        private static bool IsMaxSpeedReached(double distance, double acceleration, double travelSpeed)
        {
            var maxReachableSpeed = Math.Sqrt(distance / acceleration);
            return maxReachableSpeed > travelSpeed;
        }

        private static double GetTimeForConstAcceleration(double distance, double acceleration)
        {
            var time = Math.Sqrt(2 * distance / acceleration);
            return time;
        }

        private static double GetTimeToReachTravelSpeed(double acceleration, double travelSpeed)
        {
            var time = travelSpeed / acceleration;
            return time;
        }

        private static double GetDistanceForConstAcceleration(double acceleration, double time)
        {
            var distance = 0.5 * acceleration * Math.Pow(time, 2);
            return distance;
        }

        private static double GetTimeForConstVelocity(double velocity, double distance)
        {
            var time = distance / velocity;
            return time;
        }
    }
}
