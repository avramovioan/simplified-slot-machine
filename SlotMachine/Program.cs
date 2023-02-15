using System;

namespace SlotMachine
{
    internal class Program
    {
        //Can add more symbols. Just follow 2 conditions:
        //1. must be ordered by probability in DESC order
        //2. the sum of all probabilities must equal 100(%)
        public static readonly List<Symbol> Symbols = new List<Symbol>
        {
            new Symbol('A', 0.4, 45),
            new Symbol('B', 0.8, 35),
            new Symbol('C', 1.0, 15),
            new Symbol('*', 0.0, 5, true),
        };
        static void Main(string[] args)
        {
            SlotGame slotGame = new SlotGame(Symbols, 4, 3);
            slotGame.StartGame();
        }
    }
}