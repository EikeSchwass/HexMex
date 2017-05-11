using System;

namespace HexMex.Game.Buildings
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BuildingInformationAttribute : Attribute
    {
        public BuildingInformationAttribute(string name, float constructionTime, params ResourceType[] constructionCost)
        {
            ConstructionTime = constructionTime;
            ConstructionCost = constructionCost;
            Name = name;
        }

        public float ConstructionTime { get; }
        public ResourceType[] ConstructionCost { get; }
        public string Name { get; }
    }
}
