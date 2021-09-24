using System;

namespace Enums_Mission
{
    class Program
    {
        public enum Suits
        {
            Hearts,
            Spades,
            Diamonds,
            Clubs
        };
        public enum Rank
        {
            Ace=1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        };

        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                string input = Console.ReadLine();
                DrawCard(Suits.Hearts, Convert.ToInt32(input));
            }
        }

        static void DrawCard(Suits suit, int inputRank)
        {
            Rank rank = (Rank)inputRank;
            string line_0 = "+─────────+";
            Console.WriteLine(line_0);
            string printingSuit = "♥";
            string line_1 = "│         │";
            switch (suit)
            {
                case Suits.Hearts:
                    printingSuit = "♥";
                    break;
                case Suits.Spades:
                    printingSuit = "♠";
                    break;
                case Suits.Diamonds:
                    printingSuit = "♦";
                    break;
                case Suits.Clubs:
                    printingSuit = "♣";
                    break;
            }
           switch (rank)
            {
                case Rank.Ace:
                    Console.WriteLine("│A        │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine("│        A│");
                    break;
                case Rank.Two:
                    Console.WriteLine($"│2   {printingSuit}    │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine(line_1);
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│    {printingSuit}   2│");
                    break;
                case Rank.Three:
                    Console.WriteLine($"│3   {printingSuit}    │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│    {printingSuit}   3│");
                    break;
                case Rank.Four:
                    Console.WriteLine($"│4 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine(line_1);
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 4│");
                    break;
                case Rank.Five:
                    Console.WriteLine($"│5 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 5│");
                    break;
                case Rank.Six:
                    Console.WriteLine($"│6 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 6│");
                    break;
                case Rank.Seven:
                    Console.WriteLine($"│7 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 7│");
                    break;

                case Rank.Eight:
                    Console.WriteLine($"│8 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 8│");
                    break;
                case Rank.Nine:
                    Console.WriteLine($"│9 {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│{printingSuit}        │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"│        {printingSuit}│");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit} 9│");
                    break;
                case Rank.Ten:
                    Console.WriteLine($"│10{printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine(line_1);
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}  │");
                    Console.WriteLine($"|    {printingSuit}    │");
                    Console.WriteLine($"│  {printingSuit}   {printingSuit}10│");
                    break;
                case Rank.Jack:
                    Console.WriteLine("│J┌─────┐ │");
                    Console.WriteLine($"│{printingSuit}│{printingSuit}\\__/│ │");
                    Console.WriteLine("│ │|(_/|│ │");
                    Console.WriteLine("│ │} / {│ │");
                    Console.WriteLine("│ │|/_)|│ │");
                    Console.WriteLine($"│ │/  \\{printingSuit}│{printingSuit}│");
                    Console.WriteLine("│ └─────┘J│");
                    break;
                case Rank.Queen:
                    Console.WriteLine("│Q┌─────┐ │");
                    Console.WriteLine($"│{printingSuit}│{printingSuit}(_(/│ │");
                    Console.WriteLine("│ │  )/*│ │");
                    Console.WriteLine("│ │{ / }│ │");
                    Console.WriteLine("│ │*/(  │ │");
                    Console.WriteLine($"│ │/) ){printingSuit}│{printingSuit}│");
                    Console.WriteLine("│ └─────┘Q│");
                    break;
                case Rank.King:
                    Console.WriteLine("│K┌─────┐ │");
                    Console.WriteLine($"│{printingSuit}│{printingSuit}\\__/│ │");
                    Console.WriteLine("│ │ (_/|│ │");
                    Console.WriteLine("│ │+ / +│ │");
                    Console.WriteLine("│ │|/_) │ │");
                    Console.WriteLine($"│ │/  \\{printingSuit}│{printingSuit}│");
                    Console.WriteLine("│ └─────┘K│");
                    break;

            }


            Console.WriteLine(line_0);
        }
    }
}
