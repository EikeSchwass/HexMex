namespace HexMex.Game
{
    public struct ResourceSpawnInfo
    {
        public double SpawnProbability { get; }
        public double AmountMean { get; }
        public double AmountDeviation { get; }

        public ResourceSpawnInfo(double spawnProbability, double amountMean, double amountDeviation)
        {
            SpawnProbability = spawnProbability;
            AmountMean = amountMean;
            AmountDeviation = amountDeviation;
        }

        public bool Equals(ResourceSpawnInfo other)
        {
            return SpawnProbability.Equals(other.SpawnProbability) && AmountMean.Equals(other.AmountMean) && AmountDeviation.Equals(other.AmountDeviation);
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
                hashCode = (hashCode * 397) ^ AmountMean.GetHashCode();
                hashCode = (hashCode * 397) ^ AmountDeviation.GetHashCode();
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