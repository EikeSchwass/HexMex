namespace HexMex.Game.Buildings
{
    public abstract class Building : Structure
    {
        protected Building(HexagonNode position,ResourceManager resourceManager) : base(position, resourceManager)
        {
        }
    }
}