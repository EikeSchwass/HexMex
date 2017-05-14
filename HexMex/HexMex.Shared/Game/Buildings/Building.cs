using System;

namespace HexMex.Game.Buildings
{
    public abstract class Building : Structure
    {
        public event Action<Building> ProductionCompleted;
        public bool IsIdle => !IsProducing && ResourceDirector.PendingProvisions.Count == 0 && ResourceDirector.PendingRequests.Count == 0;
        public bool IsProducing { get; private set; }
        public float ProductionTime { get; private set; } = 1;
        public float RemainingProductionTime { get; private set; }

        protected Building(HexagonNode position, World world) : base(position, world)
        {
        }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (IsIdle)
            {
                Idling();
            }
            else if (IsProducing)
            {
                RemainingProductionTime -= dt;
                if (RemainingProductionTime <= 0)
                {
                    IsProducing = false;
                    RemainingProductionTime = 0;
                    OnProductionCompleted();
                    ProductionCompleted?.Invoke(this);
                }
            }
        }

        protected virtual void OnProductionCompleted() { }

        protected virtual void Idling()
        {
        }

        protected void StartProduction(float productionTime)
        {
            if (IsProducing)
                throw new InvalidOperationException("Building is already producing");
            IsProducing = true;
            RemainingProductionTime = productionTime;
            ProductionTime = productionTime;
        }
    }
}