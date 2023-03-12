﻿using AutomaticParking.Agents.Data;
using AutomaticParking.Car;
using UnityEngine;

namespace AutomaticParking.Agents.Components
{
    public class ParkingAgentInitializer : MonoBehaviour
    {
        [SerializeField] public ParkingAgentTargetData targetData;

        public ParkingAgentData InitializeAgentData()
        {
            var agentRigidbody = GetComponent<Rigidbody>();
            var agentTransform = GetComponent<Transform>();
            return new ParkingAgentData
            {
                Rigidbody = agentRigidbody,
                Transform = agentTransform,
                InitialPosition = agentTransform.position,
                InitialRotation = agentTransform.rotation,
                TargetData = targetData
            };
        }

        public CarData InitializeCarData() => GetComponentInChildren<CarData>();

        public ParkingAgentTargetTrackingData InitializeTargetTrackingData(ParkingAgentData data)
        {
            float initialDistanceToTarget = Vector3.Distance(data.InitialPosition, targetData.Transform.position);
            float initialAngleToTarget = Quaternion.Angle(data.InitialRotation, targetData.Transform.rotation);
            return new ParkingAgentTargetTrackingData
            {
                InitialDistanceToTarget = initialDistanceToTarget,
                MinDistanceToTarget = default,
                MaxDistanceToTarget = initialDistanceToTarget,
                InitialAngleToTarget = initialAngleToTarget,
                MinAngleToTarget = default,
                MaxAngleToTarget = initialAngleToTarget
            };
        }

        public void InitializeAgentDataComponents(ParkingAgentData data)
        {
            data.ActionsHandler = new ParkingAgentActionsHandler(data.CarData);
            data.MetricsCalculator = new ParkingAgentMetricsCalculator(data, data.TargetData, data.TargetTrackingData);
            data.RewardCalculator = new ParkingAgentRewardCalculator(data.TargetTrackingData);
            data.ObservationsCollector = new ParkingAgentObservationsCollector(data);
        }
    }
}