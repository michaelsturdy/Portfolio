﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallDemo
{
    public class Paddle
    {
        private readonly int paddleHeight = 10;
        private readonly int paddleWidth = 70;
        private Rectangle paddleDisplayArea;
        private Rectangle gameArea;
        public enum Direction {Left, Right }


        public Paddle(Rectangle gameArea)
        {
            this.gameArea = gameArea;
            paddleDisplayArea.Width = paddleWidth;
            paddleDisplayArea.Height = paddleHeight;
            paddleDisplayArea.X = gameArea.Width/2 - paddleWidth/2 ;
            paddleDisplayArea.Y = gameArea.Bottom-50;
        }

        //move

        //draw
        public void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                graphics.FillRectangle(brush, paddleDisplayArea);
            }
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        paddleDisplayArea.X = (paddleDisplayArea.X >= 20) ? paddleDisplayArea.X - 20 : 0;
                        
                            break;
                        
                    }
                case Direction.Right:
                    {
                        paddleDisplayArea.X = (paddleDisplayArea.X < gameArea.Right-paddleWidth) ? paddleDisplayArea.X + 20 : gameArea.Right - paddleWidth;
                        break;
                    }
            }
        }

    }
}
