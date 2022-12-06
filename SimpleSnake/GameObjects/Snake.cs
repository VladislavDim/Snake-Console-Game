using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const int SnakeStartLength = 6;
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySpaceSymbol = ' ';

        private readonly Food[] foods;
        private readonly Queue<Point> snakeElements;
        private readonly Wall wall;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        public Snake(Wall wall)
        {
            this.snakeElements = new Queue<Point>();
            this.foods = new Food[3];
            this.wall = wall;
            this.foodIndex = RandomFoodNumber;

            this.GetFoods();
            this.CreateSnake();
            this.foods[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private int RandomFoodNumber => new Random().Next(0, this.foods.Length);
        public bool IsMoving(Point direction)
        {
            Point crnSnakeHead = this.snakeElements
                .Last();

            GetNextPoint(direction, crnSnakeHead);

            bool isPointOfSnake = this.snakeElements
                .Any(p => p.LeftX == this.nextLeftX
                && p.TopY == this.nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point newSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (this.wall.IsPointOfWall(newSnakeHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(SnakeSymbol);

            if (foods[this.foodIndex].IsFoodPoint(newSnakeHead))
            {
                this.Eat(direction, crnSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(EmptySpaceSymbol);

            return true;
        }
        private void Eat(Point direction, Point crnSnakeHead)
        {
            int lenght = this.foods[foodIndex].FoodPoints;

            for (int i = 0; i < lenght; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                GetNextPoint(direction, crnSnakeHead);
            }

            this.foodIndex = RandomFoodNumber;
            this.foods[foodIndex].SetRandomPosition(this.snakeElements);
        }
        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = direction.LeftX + snakeHead.LeftX;
            this.nextTopY = direction.TopY + snakeHead.TopY;
        }
        private void CreateSnake()
        {
            for (int topY = 1; topY < SnakeStartLength; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }
        }
        private void GetFoods()
        {
            this.foods[0] = new FoodHash(this.wall);
            this.foods[1] = new FoodDollar(this.wall);
            this.foods[2] = new FoodAsterisk(this.wall);
        }

    }
}
