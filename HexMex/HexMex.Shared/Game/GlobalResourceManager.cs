using System;
using System.Collections.Generic;
using System.Linq;
using HexMex.Game.Settings;

namespace HexMex.Game
{
    public class GlobalResourceManager
    {
        public GameplaySettings GameplaySettings { get; }
        private Knowledge knowledge;
        private EnvironmentResource environmentResource;
        public event Action<GlobalResourceManager> ValueChanged;

        public Knowledge Knowledge
        {
            get => knowledge;
            set
            {
                knowledge = value;
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
            if (!EnergyQueue.Any())
                return;
            var nextEnergyPackage = EnergyQueue.Peek();
            if (EnvironmentResource.Energy >= nextEnergyPackage.RequiredEnergy)
            {
                environmentResource -= (EnvironmentResource)nextEnergyPackage.RequiredEnergy;
                EnergyQueue.Dequeue();
                nextEnergyPackage.Callback(nextEnergyPackage);
            }
        }

        private Queue<EnergyPackage> EnergyQueue { get; } = new Queue<EnergyPackage>();

        public void Enqueue(EnergyPackage energyPackage)
        {
            EnergyQueue.Enqueue(energyPackage);
            CheckForEnergyChange(this);
        }
        public bool EnoughKnowledgeFor(Knowledge other)
        {
            if (Knowledge.Knowledge3 < other.Knowledge3)
                return false;
            if (Knowledge.Knowledge2 < other.Knowledge2)
                return false;
            if (Knowledge.Knowledge1 < other.Knowledge1)
                return false;
            return true;
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