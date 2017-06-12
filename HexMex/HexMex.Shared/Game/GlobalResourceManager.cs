using System;
using System.Collections.Generic;
using HexMex.Game.Settings;

namespace HexMex.Game
{
    public class GlobalResourceManager
    {
        public GameplaySettings GameplaySettings { get; }
        private int knowledge1;
        private int knowledge2;
        private int knowledge3;
        private EnvironmentResource environmentResource;
        public event Action<GlobalResourceManager> ValueChanged;

        public int Knowledge1
        {
            get => knowledge1;
            set
            {
                knowledge1 = value;
                ValueChanged?.Invoke(this);
            }
        }
        public int Knowledge2
        {
            get => knowledge2;
            set
            {
                knowledge2 = value;
                ValueChanged?.Invoke(this);
            }
        }
        public int Knowledge3
        {
            get => knowledge3;
            set
            {
                knowledge3 = value;
                ValueChanged?.Invoke(this);
            }
        }

        public EnvironmentResource EnvironmentResource
        {
            get => environmentResource;
            set
            {
                environmentResource = value;
                ValueChanged?.Invoke(this);
            }
        }

        public GlobalResourceManager(GameplaySettings gameplaySettings)
        {
            GameplaySettings = gameplaySettings;
            EnvironmentResource = new EnvironmentResource(GameplaySettings.StartCO2, GameplaySettings.StartO2, GameplaySettings.StartEnergy);
            ValueChanged += CheckForEnergyChange;
        }
        private void CheckForEnergyChange(GlobalResourceManager obj)
        {
            var nextEnergyPackage = EnergyQueue.Peek();
            if (EnvironmentResource.Energy >= nextEnergyPackage.RequiredEnergy)
            {
                EnvironmentResource -= (EnvironmentResource)nextEnergyPackage.RequiredEnergy;
                EnergyQueue.Dequeue();
                nextEnergyPackage.Callback(nextEnergyPackage);
            }
        }

        private Queue<EnergyPackage> EnergyQueue { get; } = new Queue<EnergyPackage>();

        public void Enqueue(EnergyPackage energyPackage)
        {
            EnergyQueue.Enqueue(energyPackage);
        }
    }

    public class EnergyPackage
    {
        public float RequiredEnergy { get; }
        public Action<EnergyPackage> Callback { get; }

        public EnergyPackage(float requiredEnergy, Action<EnergyPackage> callback)
        {
            RequiredEnergy = requiredEnergy;
            Callback = callback;
        }
    }
}