using System;
using System.Collections.Generic;
using System.Linq;
using HexMex.Game.Settings;

namespace HexMex.Game
{
    public class GlobalResourceManager
    {
        private EnvironmentResource environmentResource;
        private Knowledge knowledge = new Knowledge(0, 0, 0);
        public event Action<GlobalResourceManager> ValueChanged;
        public event Action<GlobalResourceManager> OutOfOxygen;
        public GameplaySettings GameplaySettings { get; }

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
                if (EnvironmentResource.O2 <= 0)
                    OutOfOxygen?.Invoke(this);
            }
        }

        private Queue<EnergyPackage> EnergyQueue { get; } = new Queue<EnergyPackage>();

        public GlobalResourceManager(GameplaySettings gameplaySettings)
        {
            GameplaySettings = gameplaySettings;
            EnvironmentResource = new EnvironmentResource(0, GameplaySettings.StartO2, GameplaySettings.StartEnergy);
            ValueChanged += CheckForEnergyChange;
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

        public void Enqueue(EnergyPackage energyPackage)
        {
            EnergyQueue.Enqueue(energyPackage);
            CheckForEnergyChange(this);
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