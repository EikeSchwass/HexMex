using System;
using System.Collections.Generic;
namespace HexMex.Game
{
    public class UnlockManager
    {
        public GlobalResourceManager GlobalResourceManager { get; }
        private Dictionary<BuildingDescription, bool> UnlockedStructures { get; } = new Dictionary<BuildingDescription, bool>();

        public event Action<UnlockManager, BuildingDescription> NewStructureUnlocked;

        public UnlockManager(GlobalResourceManager globalResourceManager,BuildingDescriptionDatabase buildingDescriptionDatabase)
        {
            GlobalResourceManager = globalResourceManager;
            foreach (var buildingDescription in buildingDescriptionDatabase.BuildingDescriptions)
            {
                UnlockedStructures.Add(buildingDescription, buildingDescription.UnlockCost == Knowledge.Zero);
            }
        }

        public void Unlock(BuildingDescription structureDescription)
        {
            if (!GlobalResourceManager.EnoughKnowledgeFor(structureDescription.UnlockCost))
                throw new InvalidOperationException("Not enough knowledge aquired");
            GlobalResourceManager.Knowledge -= structureDescription.UnlockCost;
            UnlockedStructures[structureDescription] = true;
            NewStructureUnlocked?.Invoke(this, structureDescription);
        }

        public bool this[BuildingDescription structureDescription] => UnlockedStructures[structureDescription];
    }
}