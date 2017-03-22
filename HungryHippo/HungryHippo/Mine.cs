using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryHippo
{
    class Mine
    {
        private Rectangle mineArea;
        private Rectangle gameArea;
        private int size = 20;
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public Mine(Rectangle gameArea)
        {
            this.gameArea = gameArea;

            mineArea.Height = size;
            mineArea.Width = size;

            Random random = new Random();
            mineArea.X = 10;//random.Next(10, gameArea.Width - size);
            mineArea.Y = 10;//random.Next(10, gameArea.Height - size);

            XVelocity = random.Next(-10, 10);
            YVelocity = random.Next(-10, 10);

        }

        public void Move()
        {
            mineArea.X += XVelocity;
            mineArea.Y += YVelocity;
        }

        public void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(Color.Red))
            {
                graphics.FillEllipse(brush, mineArea);
            }
        }
        public Rectangle MineDisplayArea
        {
            get { return this.mineArea; }
        }

        public int Size
        {
            get { return this.size; }
        }
    }
}
