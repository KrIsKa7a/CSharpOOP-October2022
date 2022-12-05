namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class FoodPercent : Food
    {
        private const char DefaultSymbol = '%';
        private const int DefaultPoints = 4;
        private const ConsoleColor DefaultColor = ConsoleColor.Magenta;

        public FoodPercent(Wall wall) 
            : base(wall, DefaultSymbol, DefaultPoints, DefaultColor)
        {

        }
    }
}
