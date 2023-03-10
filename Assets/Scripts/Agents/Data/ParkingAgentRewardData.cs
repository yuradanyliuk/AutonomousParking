﻿namespace AutomaticParking.Agents.Data
{
    public class ParkingAgentRewardData
    {
        public const float MaxRewardForDecreasingDistanceToTarget = 10f;
        public const float MaxRewardForDecreasingAngleToTarget = 10f;

        public const float RewardForWallCollisionEnter = -5f;
    }
}