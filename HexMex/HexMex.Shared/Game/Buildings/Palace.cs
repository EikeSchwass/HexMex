namespace HexMex.Game.Buildings
{
    public class Palace : Building
    {
        public float WinTime { get; }
        public float CurrentWinTime { get; private set; }

        public Palace(HexagonNode position, World world, BuildingDescription buildingDescription) : base(position, world, buildingDescription)
        {
            WinTime = world.GameSettings.GameplaySettings.PalastWinTime;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            var environmentResource = World.GlobalResourceManager.EnvironmentResource;
            if (environmentResource.O2 <= 100 || environmentResource.CO2 / (environmentResource.CO2 + environmentResource.O2) > 0.25)
            {
                CurrentWinTime = 0;
                return;
            }
            if (CurrentWinTime >= WinTime)
                World.OnVictory();
        }

        protected override void OnProductionCompleted()
        {
            base.OnProductionCompleted();
            CurrentWinTime++;
        }
    }
}