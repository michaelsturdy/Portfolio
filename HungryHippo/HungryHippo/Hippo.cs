using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryHippo
{
    public class Hippo
    {
        private readonly int hippoHeight = 70;
        private readonly int hippoWidth = 70;
        private Rectangle hippoDisplayArea;
        private Rectangle gameArea;
        Image hippoPic = Image.FromFile(@"Images/Hippo.png");
        /// <summary>
        /// Direction to move the object 
        /// </summary>
        public enum Direction {Left, Right, Up, Down }

        /// <summary>
        /// constructor for the hippo
        /// </summary>
        /// <param name="gameArea">Rectangle for the game area</param>
        public Hippo(Rectangle gameArea)
        {
            this.gameArea = gameArea;
            hippoDisplayArea.Width = hippoWidth;
            hippoDisplayArea.Height = hippoHeight;
            hippoDisplayArea.X = gameArea.Width/2 - hippoWidth/2 ;
            hippoDisplayArea.Y = gameArea.Bottom-70;
        }

       /// <summary>
       /// draws the hippo
       /// </summary>
       /// <param name="graphics">graphics from paint event args</param>
        public void Draw(Graphics graphics)
        { 
            graphics.DrawImage(hippoPic, hippoDisplayArea);
        }
        /// <summary>
        /// gets the display area rectangle that holds the hippo object
        /// </summary>
        public Rectangle DisplayArea
        {
            get { return hippoDisplayArea; }
        }
        /// <summary>
        /// Moves the hippo in a direction
        /// </summary>
        /// <param name="direction">direction to move</param>
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        hippoDisplayArea.X = (hippoDisplayArea.X >= 20) ? hippoDisplayArea.X - 20 : 0;
                        break;
                    }
                case Direction.Right:
                    {
                        hippoDisplayArea.X = (hippoDisplayArea.X < gameArea.Right-hippoWidth) ? hippoDisplayArea.X + 20 : gameArea.Right - hippoWidth;
                        break;
                    }
                    
                case Direction.Up:
                    {
                        hippoDisplayArea.Y = (hippoDisplayArea.Y >= 20) ? hippoDisplayArea.Y - 20 : 0;
                        break;
                    }
                case Direction.Down:
                    {
                        hippoDisplayArea.Y = (hippoDisplayArea.Y < gameArea.Height - hippoHeight) ? hippoDisplayArea.Y + 20 : gameArea.Height - hippoHeight;
                        break;
                    }

            }
        }

    }
}
