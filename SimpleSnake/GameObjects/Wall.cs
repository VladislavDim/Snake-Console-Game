using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '\u25A0';
        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            InitializeWallBordes();
        }
        private void InitializeWallBordes()
        {
            this.SetHorizontalLine(0);
            this.SetHorizontalLine(this.TopY);

            this.SetVerticallLine(0);
            this.SetVerticallLine(this.LeftX - 1);

        }
        public bool IsPointOfWall(Point snakeHead)
        {
            return snakeHead.LeftX == 0
                || snakeHead.LeftX == this.LeftX - 1
                || snakeHead.TopY == 0
                || snakeHead.TopY == this.TopY;
        }
        private void SetHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < this.LeftX; leftX++)
            {
                this.Draw(leftX, topY, WallSymbol);
            }
        }
        private void SetVerticallLine(int leftX)
        {
            for (int topY = 0; topY < this.TopY; topY++)
            {
                this.Draw(leftX, topY, WallSymbol);
            }
        }
    }
}
