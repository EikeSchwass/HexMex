using System.Collections.Generic;

namespace HexMex.Game.Buildings
{
    public abstract class Building : Structure
    {
        public float ProductionTime { get; }

        protected Building(HexagonNode position, World world, float productionTime, IEnumerable<ResourceType> inputs, IEnumerable<ResourceType> outputs) : base(position, world, inputs, outputs)
        {
            ProductionTime = productionTime;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Producing)
                return;
            CurrentProductionTime += dt;
            if (CurrentProductionTime >= ProductionTime)
            {
                OnProductionFinished();
                CurrentProductionTime = 0;
                Producing = false;
            }
        }

        protected override void StartProduction()
        {
            base.StartProduction();
            if (Producing)
                return;
            Producing = true;
            CurrentProductionTime = 0;
        }

        private float CurrentProductionTime { get; set; }
        private bool Producing { get; set; }
    }
}