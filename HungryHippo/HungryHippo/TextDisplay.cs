using System.Drawing;

namespace HungryHippo
{
    class TextDisplay
    {

        Font font = new Font("comicSans", 24);
        SolidBrush brush = new SolidBrush(Color.Yellow);
        /// <summary>
        /// draws text on the screen
        /// </summary>
        /// <param name="graphics">graphics object from paint event args</param>
        /// <param name="message">text to be displayed</param>
        /// <param name="xPoint">x coordinate to draw on</param>
        /// <param name="yPoint">y coordinate to draw on</param>
        public void Draw(Graphics graphics, string message, int xPoint, int yPoint)
        {
            Point point = new Point(xPoint, yPoint);
            graphics.DrawString(message, font, brush, point);
        }
    }
}
