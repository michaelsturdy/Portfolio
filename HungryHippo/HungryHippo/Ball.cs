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
        /// <summary>
        /// X axis velocity
        /// </summary>
        public int XVelocity { get;set; }
        /// <summary>
        /// Y  axis velocity
        /// </summary>
        public int YVelocity { get; set; }
        /// <summary>
        /// Ball constructor
        /// </summary>
        /// <param name="gameArea">Rectangle of the game area</param>
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
        /// <summary>
        /// moves the ball object
        /// </summary>
        public void Move()
        {           
            ballArea.X += XVelocity;
            ballArea.Y += YVelocity;
        }
        /// <summary>
        /// draws the ball object
        /// </summary>
        /// <param name="graphics">graphic from the paint event args</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(ballImage, ballDisplayArea);
        }
        /// <summary>
        /// gets the balls display area
        /// </summary>
        public Rectangle ballDisplayArea
        {
            get { return this.ballArea; }
        }
        /// <summary>
        /// gets the ball size
        /// </summary>
        public int Size
        {
            get { return this.size; }
        }
    }
}
