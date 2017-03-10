﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallDemo
{
    public class Ball
    {
        private Rectangle ballArea;
        private Rectangle gameArea;
        private int size = 10;
        public int XVelocity { get;set; }
        public int YVelocity { get; set; }

        public Ball(Rectangle gameArea)
        {
            this.gameArea = gameArea;

            ballArea.Height = size;
            ballArea.Width = size;

            ballArea.X = gameArea.Width / 2 - size/2;
            ballArea.Y = gameArea.Height / 2 - size/2;

            Random random = new Random();
            XVelocity = random.Next(-10, 10);
            YVelocity= random.Next(-10, 10);

        }

        public void Move()
        {

           
            
            ballArea.X += XVelocity;
            ballArea.Y += YVelocity;
        }

        public void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color.Gray))
            {
                graphics.FillEllipse(brush, ballArea);
            }
        }
        public Rectangle ballDisplayArea
        {
            get { return this.ballArea; }
        }

        public int Size
        {
            get { return this.size; }
        }
    }
}
