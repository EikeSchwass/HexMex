namespace HexMex.Game
{
    public struct ResourceSpawnInfo
    {
        public double SpawnProbability { get; }
        public double TargetPayoutProbability { get; }
        public double PayoutProbabilityDeviation { get; }

        public ResourceSpawnInfo(double spawnProbability, double targetPayoutProbability, double payoutProbabilityDeviation)
        {
            SpawnProbability = spawnProbability;
            TargetPayoutProbability = targetPayoutProbability;
            PayoutProbabilityDeviation = payoutProbabilityDeviation;
        }

        public bool Equals(ResourceSpawnInfo other)
        {
            return SpawnProbability.Equals(other.SpawnProbability) && TargetPayoutProbability.Equals(other.TargetPayoutProbability) && PayoutProbabilityDeviation.Equals(other.PayoutProbabilityDeviation);
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
                hashCode = (hashCode * 397) ^ TargetPayoutProbability.GetHashCode();
                hashCode = (hashCode * 397) ^ PayoutProbabilityDeviation.GetHashCode();
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