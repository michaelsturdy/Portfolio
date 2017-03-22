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
        private readonly int hippoHeight = 50;
        private readonly int hippoWidth = 50;
        private Rectangle hippoDisplayArea;
        private Rectangle gameArea;
        Image hippoPic = Image.FromFile(@"Images/Hippo.png");
        public enum Direction {Left, Right, Up, Down }


        public Hippo(Rectangle gameArea)
        {
            this.gameArea = gameArea;
            hippoDisplayArea.Width = hippoWidth;
            hippoDisplayArea.Height = hippoHeight;
            hippoDisplayArea.X = gameArea.Width/2 - hippoWidth/2 ;
            hippoDisplayArea.Y = gameArea.Bottom-50;
        }

        //move

        //draw
        public void Draw(Graphics graphics)
        {
            //using (SolidBrush brush = new SolidBrush(Color.White))
            {
                //graphics.FillRectangle(brush, paddleDisplayArea);
            }
            graphics.DrawImage(hippoPic, hippoDisplayArea);


        }

        public Rectangle DisplayArea
        {
            get { return hippoDisplayArea; }
        }
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
