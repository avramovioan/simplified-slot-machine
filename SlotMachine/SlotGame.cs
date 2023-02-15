namespace SlotMachine
{
    public class SlotGame
    {
        private readonly List<Symbol> Symbols;
        private readonly int Rows, Columns;
        private readonly Random random;
        public SlotGame(List<Symbol> symbols, int rows, int columns)
        {
            this.Symbols = symbols;
            this.Rows = rows;
            this.Columns = columns;
            random = new Random();
        }

        public void StartGame()
        {
            double deposit = GetDeposit();
            while (deposit != 0)
            {
                double stake = GetStake();
                if (stake > deposit)
                {
                    System.Console.WriteLine("Stake must be less or equal to your deposit.");
                    continue;
                }

                System.Console.WriteLine();
                double accumulatedCoefficient = Slot(); // getting accumulated coefficient from one slot
                System.Console.WriteLine();

                double winnings = CalculateWinnings(accumulatedCoefficient, stake);
                deposit = deposit - stake + winnings;
                System.Console.WriteLine($"You have won: {winnings.ToString("N2")}");
                System.Console.WriteLine($"Current balance is: {deposit.ToString("N2")}");
            }
            System.Console.WriteLine("Insufficient funds.");
        }

        private double Slot()
        {
            double accumulatedCoefficient = 0;
            for (int i = 0; i < Rows; i++)
            {
                double currentCoefficient = 0;
                bool allSymbolsMatch = true; // flagging if all symbols on this row match
                Symbol lastLandedSymbol = null; //saving the last symbol
                for (int j = 0; j < Columns; j++)
                {
                    int landedProbability = random.Next(0, 101);//101 as it's exclusive
                    Symbol landedSymbol = GetNextSymbol(landedProbability);
                    currentCoefficient += landedSymbol.Coefficient;
                    System.Console.Write(landedSymbol.Label);
                    if (lastLandedSymbol == null)
                    {
                        lastLandedSymbol = landedSymbol; //saving the last symbol
                        continue;
                    }
                    if (allSymbolsMatch &&
                        !landedSymbol.Label.Equals(lastLandedSymbol.Label) && !landedSymbol.IsWildcard)
                    {
                        allSymbolsMatch = false;
                    }
                }
                if (allSymbolsMatch) accumulatedCoefficient += currentCoefficient;
                System.Console.WriteLine();
            }
            return accumulatedCoefficient; //returning the total winning coefficient of slotting
        }


        private double CalculateWinnings(double coefficient, double stake)
        {
            return coefficient * stake;
        }

        private double GetStake()
        {
            double stake;
            bool conversion = false;
            do
            {
                System.Console.WriteLine("Enter stake amount:");
                conversion = double.TryParse(Console.ReadLine(), out stake);
            } while (!conversion); //prompt again for valid input
            return stake;
        }

        private double GetDeposit()
        {
            System.Console.WriteLine("Please deposit money you would like to play with:");
            double deposit;
            bool conversion = double.TryParse(Console.ReadLine(), out deposit);
            if (!conversion) throw new ArgumentException("Invalid input.");
            return deposit;
        }

        //symbols need to be sorted by percentProbability descending
        private Symbol GetNextSymbol(int landedProbability)
        {
            double currentProbability = 0;
            foreach (Symbol symbol in Symbols)
            {
                currentProbability += symbol.PercentProbability;
                if (currentProbability >= landedProbability) return symbol;
            }
            //will only throw if landedProbability is not within the range 0 and 100 
            throw new ArgumentOutOfRangeException(landedProbability.ToString(), "The value must be between 0 and 100(%).");

        }

        //alternative (quicker), but hardcoded

        // private Symbol GetNextSymbol(int landedProbability)
        // {
        //     if (landedProbability < 0 || landedProbability > 100)
        //     {
        //         throw new ArgumentOutOfRangeException(landedProbability.ToString(), "The value must be between 0 and 100(%).");
        //     }
        //     if (landedProbability <= 45) return new Symbol('A', 0.4); //45
        //     if (landedProbability <= 80) return new Symbol('B', 0.6); //45+35
        //     if (landedProbability <= 95) return new Symbol('C', 0.8); //80+15
        //     if (landedProbability <= 100) return new Symbol('*', 0.0);//95+5
        // }
    }
}