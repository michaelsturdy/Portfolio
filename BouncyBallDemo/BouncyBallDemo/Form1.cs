using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBallDemo
{
    public partial class GameForm : Form
    {
        
        int ballsCollected = 0;
        Paddle paddle;
        HashSet<Ball> balls = new HashSet<Ball>();
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            paddle = new Paddle(this.DisplayRectangle);
            balls.Add(new Ball(this.DisplayRectangle));                    
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            UpdateBallsCollected(e.Graphics);
            paddle.Draw(e.Graphics);
            foreach (Ball ball in balls)
            {
                ball.Draw(e.Graphics);
            }
        }
        private void UpdateBallsCollected(Graphics graphics)
        {
            string message = @"Balls Collected: {0}";
            Font font = new Font("comicSans", 16);
            SolidBrush brush = new SolidBrush(Color.Yellow);
            Point point = new Point(20, 20);
            graphics.DrawString(string.Format(message, ballsCollected),font,brush,point);

        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    {
                        paddle.Move(Paddle.Direction.Left);
                        break;
                    }
                case Keys.Right:
                    {
                        paddle.Move(Paddle.Direction.Right);
                        break;
                    }

                case Keys.Up:
                    {
                        paddle.Move(Paddle.Direction.Up);
                        break;
                    }
                case Keys.Down:
                    {
                        paddle.Move(Paddle.Direction.Down);
                        break;
                    }

                case Keys.Space:
                    {
                        if (timer1.Enabled)
                        {
                            balls.Add(new Ball(this.DisplayRectangle));
                            //timer1.Stop();
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
            
            CheckForCollision();
            foreach (Ball ball in balls)
            {
                
                ball.Move();
            }
            
            Invalidate();
        }

        private void CheckForCollision()
        {
            balls.RemoveWhere(BallHitsPaddle);

            foreach (Ball ball in balls)
            {
                //ball touches wall
                if (ball.ballDisplayArea.X <= 0)
                {
                    ball.XVelocity = ball.XVelocity * -1;
                }
                else if (ball.ballDisplayArea.X >= this.DisplayRectangle.Right - ball.Size)
                {
                    ball.XVelocity = ball.XVelocity * -1;
                }


                //ball touches top or bottom
                else if (ball.ballDisplayArea.Y <= 0)
                {
                    ball.YVelocity = ball.YVelocity * -1;
                }
                else if (ball.ballDisplayArea.Y >= this.Height - ball.Size)
                {
                    ball.YVelocity = ball.YVelocity * -1;
                }
                
            }
        }

        private bool BallHitsPaddle(Ball ball)
        {
            if (ball.ballDisplayArea.IntersectsWith(paddle.DisplayArea))
            {
                ballsCollected += 1;
                return true;
            }
            return false;

        }
    }
}
