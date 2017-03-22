using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryHippo
{
    class Pause
    {
        string message = "PAUSED";
        Font font = new Font("comicSans", 24);
        SolidBrush brush = new SolidBrush(Color.Yellow);
       
        Rectangle gameArea;

        public Pause(Rectangle gameArea)
        {
            this.gameArea = gameArea;
        }

        public void Draw(Graphics graphics)
        {
            Point point = new Point(gameArea.Width/2, gameArea.Height/2);
            graphics.DrawString(message, font, brush, point);
        }
    }
}
