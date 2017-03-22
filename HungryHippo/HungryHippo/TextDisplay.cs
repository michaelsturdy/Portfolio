using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryHippo
{
    class TextDisplay
    {

        Font font = new Font("comicSans", 24);
        SolidBrush brush = new SolidBrush(Color.Yellow);

        public void Draw(Graphics graphics, string message, int xPoint, int yPoint)
        {
            Point point = new Point(xPoint, yPoint);
            graphics.DrawString(message, font, brush, point);
        }
    }
}
