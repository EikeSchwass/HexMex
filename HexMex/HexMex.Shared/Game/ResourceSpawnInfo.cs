namespace HexMex.Game
{
    public struct ResourceSpawnInfo
    {
        public double SpawnProbability { get; }
        public float PayoutInterval { get; }

        public ResourceSpawnInfo(double spawnProbability, float payoutInterval)
        {
            SpawnProbability = spawnProbability;
            PayoutInterval = payoutInterval;
        }

        public bool Equals(ResourceSpawnInfo other)
        {
            return SpawnProbability.Equals(other.SpawnProbability) && PayoutInterval.Equals(other.PayoutInterval);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is ResourceSpawnInfo && Equals((ResourceSpawnInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SpawnProbability.GetHashCode();
                hashCode = (hashCode * 397) ^ PayoutInterval.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ResourceSpawnInfo left, ResourceSpawnInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ResourceSpawnInfo left, ResourceSpawnInfo right)
        {
            return !left.Equals(right);
        }
    }
}