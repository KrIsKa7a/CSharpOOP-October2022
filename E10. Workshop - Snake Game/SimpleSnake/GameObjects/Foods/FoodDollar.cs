namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class FoodDollar : Food
    {
        private const char DefaultSymbol = '$';
        private const int DefaultPoints = 2;
        private const ConsoleColor DefaultColor = ConsoleColor.Green;

        public FoodDollar(Wall wall) 
            : base(wall, DefaultSymbol, DefaultPoints, DefaultColor)
        {

        }
    }
}
