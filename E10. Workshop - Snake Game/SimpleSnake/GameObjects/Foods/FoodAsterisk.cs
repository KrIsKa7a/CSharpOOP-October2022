namespace SimpleSnake.GameObjects.Foods
{
    using System;

    public class FoodAsterisk : Food
    {
        private const char DefaultSymbol = '*';
        private const int DefaultPoints = 1;
        private const ConsoleColor DefaultColor = ConsoleColor.Red;

        public FoodAsterisk(Wall wall) 
            : base(wall, DefaultSymbol, DefaultPoints, DefaultColor)
        {

        }
    }
}
