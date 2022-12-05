namespace SimpleSnake.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Foods;
    using Utilities;

    public class Snake
    {
        //private const int SnakeStartTopY = 1;
        private const int SnakeStartLength = 6;
        //private const int SnakeEndTopY = SnakeStartTopY + SnakeStartLength;
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySpace = ' ';

        private readonly Queue<Point> snakeElements;
        private readonly Wall wall;
        private readonly ReflectionHelper reflectionHelper;
        private IList<Food> foods;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        private Snake()
        {
            this.snakeElements = new Queue<Point>();
            this.foods = new List<Food>();
            this.foodIndex = this.RandomFoodNumber;
            this.reflectionHelper = new ReflectionHelper();

            this.CreateSnake();
        }

        public Snake(Wall wall)
            : this()
        {
            this.wall = wall;

            this.GetFoods();
        }

        public int FoodEaten { get; set; }

        private int RandomFoodNumber
            => new Random().Next(0, this.foods.Count);

        public bool CanMove(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();
            this.GetNextPoint(direction, currentSnakeHead);

            bool hasSnakeOverlapped = this.snakeElements
                .Any(e => e.LeftX == this.nextLeftX && e.TopY == this.nextTopY);
            if (hasSnakeOverlapped)
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);
            if (this.wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(SnakeSymbol);

            if (this.foods[this.foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(EmptySpace);

            return true;
        }

        private void CreateSnake()
        {
            for (int topY = 1; topY <= SnakeStartLength; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }
        }

        private void GetFoods()
        {
            this.foods = this.reflectionHelper
                .GenerateFoods(this.wall)
                .ToList();
            this.SpawnFood();
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int foodPoints = this.foods[this.foodIndex].FoodPoints;

            for (int i = 0; i < foodPoints; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                this.GetNextPoint(direction, currentSnakeHead);
            }

            this.FoodEaten += foodPoints;
            this.SpawnFood();
        }

        private void SpawnFood()
        {
            this.foodIndex = this.RandomFoodNumber;
            this.foods[foodIndex].SetRandomPosition(this.snakeElements);
        }
    }
}
