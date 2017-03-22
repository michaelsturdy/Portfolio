using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryHippo
{
    public class Ball
    {
        Image ballImage = Image.FromFile(@"Images/Ball.png");
        private Rectangle ballArea;
        private Rectangle gameArea;
        private int size = 20;
        public int XVelocity { get;set; }
        public int YVelocity { get; set; }

        public Ball(Rectangle gameArea)
        {
            this.gameArea = gameArea;

            ballArea.Height = size;
            ballArea.Width = size;

            Random random = new Random();
            ballArea.X = random.Next(5,gameArea.Width - size);
            ballArea.Y = random.Next(5, gameArea.Height - size);

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
            graphics.DrawImage(ballImage, ballDisplayArea);
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
