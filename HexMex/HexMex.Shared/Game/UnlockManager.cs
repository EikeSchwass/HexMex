using System;
using System.Collections.Generic;
namespace HexMex.Game
{
    public class UnlockManager
    {
        public GlobalResourceManager GlobalResourceManager { get; }
        private Dictionary<StructureDescription, bool> UnlockedStructures { get; } = new Dictionary<StructureDescription, bool>();

        public event Action<UnlockManager, StructureDescription> NewStructureUnlocked;

        public UnlockManager(GlobalResourceManager globalResourceManager)
        {
            GlobalResourceManager = globalResourceManager;
            foreach (var buildingConstructionFactory in BuildingConstructionFactory.Factories.Values)
            {
                UnlockedStructures.Add(buildingConstructionFactory.StructureDescription, false);
            }
        }

        public void Unlock(StructureDescription structureDescription)
        {
            if (GlobalResourceManager.Knowledge < structureDescription.UnlockCost)
                throw new InvalidOperationException("Not enough knowledge aquired");
            GlobalResourceManager.Knowledge -= structureDescription.UnlockCost;
            UnlockedStructures[structureDescription] = true;
            NewStructureUnlocked?.Invoke(this, structureDescription);
        }
        public bool this[StructureDescription structureDescription] => UnlockedStructures[structureDescription];
    }
}