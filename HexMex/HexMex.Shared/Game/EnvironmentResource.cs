namespace HexMex.Game
{
    public struct EnvironmentResource
    {
        public float CO2 { get; }
        public float O2 { get; }
        public float Energy { get; }

        public EnvironmentResource(float co2, float o2, float energy)
        {
            CO2 = co2;
            O2 = o2;
            Energy = energy;
        }

        public EnvironmentResource(float energy) : this(0, 0, energy) { }

        public static explicit operator EnvironmentResource(float energy)
        {
            return new EnvironmentResource(energy);
        }

        public static EnvironmentResource operator +(EnvironmentResource e1, EnvironmentResource e2) => new EnvironmentResource(e1.CO2 + e2.CO2, e1.O2 + e2.O2, e1.Energy + e2.Energy);
        public static EnvironmentResource operator -(EnvironmentResource e1, EnvironmentResource e2) => new EnvironmentResource(e1.CO2 - e2.CO2, e1.O2 - e2.O2, e1.Energy - e2.Energy);
        public static EnvironmentResource operator *(EnvironmentResource e1, int factor) => new EnvironmentResource(e1.CO2 * factor, e1.O2 * factor, e1.Energy * factor);
        public static EnvironmentResource operator /(EnvironmentResource e1, int factor) => new EnvironmentResource(e1.CO2 / factor, e1.O2 / factor, e1.Energy / factor);

        public static explicit operator float(EnvironmentResource environmentResource)
        {
            return environmentResource.Energy;
        }

        public bool Equals(EnvironmentResource other)
        {
            return CO2.Equals(other.CO2) && O2.Equals(other.O2) && Energy.Equals(other.Energy);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is EnvironmentResource && Equals((EnvironmentResource)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CO2.GetHashCode();
                hashCode = (hashCode * 397) ^ O2.GetHashCode();
                hashCode = (hashCode * 397) ^ Energy.GetHashCode();
                return hashCode;
            }
        }
        public static bool operator ==(EnvironmentResource left, EnvironmentResource right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(EnvironmentResource left, EnvironmentResource right)
        {
            return !left.Equals(right);
        }
    }
}