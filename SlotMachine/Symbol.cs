namespace SlotMachine
{
    public class Symbol
    {
        public char Label { get; private set; }
        public double Coefficient { get; private set; }
        public double PercentProbability { get; private set; }
        public bool IsWildcard { get; private set; } = false;
        public Symbol(char label, double coefficient, double percentProbability)
        {
            this.Label = label;
            this.Coefficient = coefficient;
            this.PercentProbability = percentProbability;
        }

        public Symbol(char label, double coefficient, double percentProbability, bool isWildcard)
            : this(label, coefficient, percentProbability)
        {
            this.IsWildcard = isWildcard;
        }
    }
}