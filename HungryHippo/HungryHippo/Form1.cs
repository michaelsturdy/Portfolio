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

namespace HungryHippo
{
    public partial class GameForm : Form
    {
        bool paused = false;
        int maxMines = 5;
        int ballsCollected = -1;
        TextDisplay text;
        Hippo hippo;
        HashSet<Ball> balls = new HashSet<Ball>();
        HashSet<Mine> mines = new HashSet<Mine>();
        public GameForm()
        {
            InitializeComponent();   
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            hippo = new Hippo(this.DisplayRectangle);
            balls.Add(new Ball(this.DisplayRectangle));
            text = new TextDisplay();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            if (ballsCollected == -1)
            {
                text.Draw(e.Graphics, "press space to start and pause" + "\n" + "avoid the mines" +"\n"+ "collect 20 balls to level up", DisplayRectangle.Width / 3, DisplayRectangle.Height / 3);
            }
            
            UpdateBallsCollected(e.Graphics);
            hippo.Draw(e.Graphics);
            foreach (Ball ball in balls)
            {
                ball.Draw(e.Graphics);
            }
            foreach (Mine mine in mines)
            {
                mine.Draw(e.Graphics);
            }
            if (paused)
            {
                text.Draw(e.Graphics,"PAUSED",DisplayRectangle.Width/2,DisplayRectangle.Height/2);
            }
            if (ballsCollected >= 5)//change 5 back to 20
            {
                ballsCollected = 0;
                maxMines += 2;
                AnimationTimer.Stop();
                BallTimer.Stop();
                text.Draw(e.Graphics, "Level Up Press Space For The Next level", DisplayRectangle.Width / 3, DisplayRectangle.Height / 2);

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
            if (e.KeyData == Keys.Space)
            {
                if (ballsCollected == -1)
                {
                    ballsCollected = 0;
                }
                
                if (AnimationTimer.Enabled)
                {
                    paused = true;
                    AnimationTimer.Stop();
                    BallTimer.Stop();
                    Invalidate();
                }
                else
                {
                    paused = false;
                    
                    AnimationTimer.Start();
                    BallTimer.Start();

                }             
            }
            

            if (AnimationTimer.Enabled)
            {
                switch (e.KeyData)
                {
                    case Keys.Left:
                        {
                            hippo.Move(Hippo.Direction.Left);
                            break;
                        }
                    case Keys.Right:
                        {
                            hippo.Move(Hippo.Direction.Right);
                            break;
                        }

                    case Keys.Up:
                        {
                            hippo.Move(Hippo.Direction.Up);
                            break;
                        }
                    case Keys.Down:
                        {
                            hippo.Move(Hippo.Direction.Down);
                            break;
                        }

                   
                }
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            
            CheckForCollision();
            foreach (Ball ball in balls)
            {
                ball.Move();
            }
            foreach (Mine mine in mines)
            {
                mine.Move();
            }

            Invalidate();
        }

        private void CheckForCollision()
        {
            balls.RemoveWhere(BallHitsHippo);
            mines.RemoveWhere(MineHitsHippo);

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
                else if (ball.ballDisplayArea.Y >= this.DisplayRectangle.Height - ball.Size)
                {
                    ball.YVelocity = ball.YVelocity * -1;
                }
                
            }
            foreach (Mine mine in mines)
            {
                //ball touches wall
                if (mine.MineDisplayArea.X <= 0)
                {
                    mine.XVelocity = mine.XVelocity * -1;
                }
                else if (mine.MineDisplayArea.X >= this.DisplayRectangle.Right - mine.Size)
                {
                    mine.XVelocity = mine.XVelocity * -1;
                }


                //ball touches top or bottom
                else if (mine.MineDisplayArea.Y <= 0)
                {
                    mine.YVelocity = mine.YVelocity * -1;
                }
                else if (mine.MineDisplayArea.Y >= this.DisplayRectangle.Height - mine.Size)
                {
                    mine.YVelocity = mine.YVelocity * -1;
                }

            }
        }

        private bool BallHitsHippo(Ball ball)
        {
            if (ball.ballDisplayArea.IntersectsWith(hippo.DisplayArea))
            {
                ballsCollected += 1;
                return true;
            }
            return false;

        }

        private bool MineHitsHippo(Mine mine)
        {
            if (mine.MineDisplayArea.IntersectsWith(hippo.DisplayArea))
            {
                ballsCollected += -10;
                if (ballsCollected < 0)
                {
                    ballsCollected = 0;
                }
                return true;
            }
            return false;
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            if (balls.Count < 30)
            {
                balls.Add(new Ball(this.DisplayRectangle));
            }
            if (mines.Count < maxMines)
            {
                mines.Add(new Mine(this.DisplayRectangle));
            }

         }
    }
}
