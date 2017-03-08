using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBallDemo
{
    public partial class GameForm : Form
    {
        Paddle paddle;
        Ball ball;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            paddle = new Paddle(this.DisplayRectangle);
            ball = new Ball(this.DisplayRectangle);                    
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            paddle.Draw(e.Graphics);
            ball.Draw(e.Graphics);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    {
                        paddle.Move(Paddle.Direction.Left);
                        //Invalidate();
                        break;
                    }
                case Keys.Right:
                    {
                        paddle.Move(Paddle.Direction.Right);
                        //Invalidate();
                        break;
                    }
                case Keys.Space:
                    {
                        if (timer1.Enabled)
                        {
                            timer1.Stop();
                        }
                        else
                        {
                            timer1.Start();  
                        }
                        break;
                    }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Move();
            Invalidate();
        }
    }
}
