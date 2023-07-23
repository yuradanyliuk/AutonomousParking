using System.Collections.Generic;
using AutonomousParking.Common;
using AutonomousParking.Common.Patterns.Factory;
using AutonomousParking.Common.Patterns.Pool;
using AutomaticParking.Demonstration.Architecture;
using UnityEngine;
using UnityEngine.Pool;

namespace AutonomousParking.Car.Creation
{
    public class CarPoolsInitializer : MonoBehaviour
    {
        [SerializeField] private PoolData poolData;
        [SerializeField] private CarsFactoryData factoryData;

        public List<IObjectPool<Transform>> CarPools { get; } = new();
        public NextRandomItemProvider<IObjectPool<Transform>> NextRandomCarPoolProvider { get; private set; }

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);

            foreach (GameObject carPrefab in factoryData.Prefabs)
            {
                var carFactory = new ComponentFactory<Transform>(carPrefab, factoryData.Parent);
                var carPoolFactory = new ComponentPoolFactory<Transform>(carFactory, poolData);
                CarPools.Add(carPoolFactory.FillWithStartObjects(carPoolFactory.Create()));
            }

            NextRandomCarPoolProvider = new NextRandomItemProvider<IObjectPool<Transform>>(CarPools);
        }
    }
}