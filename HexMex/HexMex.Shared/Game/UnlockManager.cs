using System;
using System.Collections.Generic;
namespace HexMex.Game
{
    public class UnlockManager
    {
        public GlobalResourceManager GlobalResourceManager { get; }
        private Dictionary<BuildingDescription, bool> UnlockedStructures { get; } = new Dictionary<BuildingDescription, bool>();

        public event Action<UnlockManager, BuildingDescription> NewStructureUnlocked;

        public UnlockManager(GlobalResourceManager globalResourceManager)
        {
            GlobalResourceManager = globalResourceManager;
            foreach (var buildingConstructionFactory in BuildingConstructionFactory.Factories.Values)
            {
                UnlockedStructures.Add(buildingConstructionFactory.StructureDescription, false);
            }
        }

        public void Unlock(BuildingDescription structureDescription)
        {
            if (GlobalResourceManager.Knowledge < structureDescription.UnlockCost)
                throw new InvalidOperationException("Not enough knowledge aquired");
            GlobalResourceManager.Knowledge -= structureDescription.UnlockCost;
            UnlockedStructures[structureDescription] = true;
            NewStructureUnlocked?.Invoke(this, structureDescription);
        }
        public bool this[BuildingDescription structureDescription] => UnlockedStructures[structureDescription];
    }
}