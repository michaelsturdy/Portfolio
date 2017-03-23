using System;
using System.Drawing;


namespace HungryHippo
{
    class Mine
    {
        Image mineImage = Image.FromFile(@"Images/Mine.png");
        private Rectangle mineArea;
        private Rectangle gameArea;
        private int size = 30;
        /// <summary>
        /// X axis velocity
        /// </summary>
        public int XVelocity { get; set; }
        /// <summary>
        /// Y axis velocity
        /// </summary>
        public int YVelocity { get; set; }

        /// <summary>
        /// Constructor for the Mine object
        /// </summary>
        /// <param name="gameArea">Game area Rectangle</param>
        public Mine(Rectangle gameArea)
        {
            this.gameArea = gameArea;

            mineArea.Height = size;
            mineArea.Width = size;

            Random random = new Random();
            mineArea.X = 30;//random.Next(10, gameArea.Width - size);
            mineArea.Y = 30;//random.Next(10, gameArea.Height - size);

            XVelocity = random.Next(-10, 10);
            YVelocity = random.Next(-10, 10);

        }
        /// <summary>
        /// moves the mine
        /// </summary>
        public void Move()
        {
            mineArea.X += XVelocity;
            mineArea.Y += YVelocity;
        }
        /// <summary>
        /// draws the mine
        /// </summary>
        /// <param name="graphics">graphics object from the paint event args</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(mineImage, MineDisplayArea);
        }
        /// <summary>
        /// gets the display Rectangle of the mine
        /// </summary>
        public Rectangle MineDisplayArea
        {
            get { return this.mineArea; }
        }
        /// <summary>
        /// sets the y position of the mine area
        /// </summary>
        public int Y
        {
            set { mineArea.Y = value; }
        }
        /// <summary>
        /// gets the size of the mine
        /// </summary>
        public int Size
        {
            get { return this.size; }
        }
    }
}
