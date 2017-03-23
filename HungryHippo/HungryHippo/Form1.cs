using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HungryHippo
{
    public partial class GameForm : Form
    {
        bool gameStart = true;
        bool paused = false;
        int maxMines = 5;
        int ballsCollected = 0;
        int level = 1;
        TextDisplay text;
        Hippo hippo;
        HashSet<Ball> balls = new HashSet<Ball>();
        HashSet<Mine> mines = new HashSet<Mine>();
        List<Keys> KeyList = new List<Keys>();
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
            if (gameStart)
            {
                text.Draw(e.Graphics, "Press space to start and pause" + "\n"+"Use the arrow keys to move around" + "\n" + "Mines reduce the amount of balls collected" +"\n"+ "Collect 20 balls to level up", DisplayRectangle.Width / 3, DisplayRectangle.Height / 3);
            }
            text.Draw(e.Graphics, string.Format("Level: {0}", level) , DisplayRectangle.Width - 200, 20);
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
            if (ballsCollected >= 20)//balls needed to level up
            {
                level++;
                ballsCollected = 0;
                maxMines += 2;
                AnimationTimer.Stop();
                BallTimer.Stop();
                text.Draw(e.Graphics, "Level Up. Press Space For The Next level", DisplayRectangle.Width / 3, DisplayRectangle.Height / 2);

            }
        }
        /// <summary>
        /// draws the balls collected message
        /// </summary>
        /// <param name="graphics">paint event argument graphics</param>
        private void UpdateBallsCollected(Graphics graphics)
        {
            string message = @"Balls Collected: {0}";
            Font font = new Font("comicSans", 24);
            SolidBrush brush = new SolidBrush(Color.Yellow);
            Point point = new Point(DisplayRectangle.Width / 3, 20);
            graphics.DrawString(string.Format(message, ballsCollected),font,brush,point);

        }
        /// <summary>
        /// Listens for keys pressed
        /// </summary>
        /// <param name="sender">key pressed object</param>
        /// <param name="e">Key event arguments</param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (AnimationTimer.Enabled)
            {
                switch (e.KeyData)
                {
                    case Keys.Left:
                        {
                            if (!KeyList.Contains(Keys.Left))
                            {
                                KeyList.Add(Keys.Left);
                            }
                            break;
                        }
                    case Keys.Right:
                        {
                            if (!KeyList.Contains(Keys.Right))
                            {
                                KeyList.Add(Keys.Right);
                            }
                            break;
                        }

                    case Keys.Up:
                        {
                            if (!KeyList.Contains(Keys.Up))
                            {
                                KeyList.Add(Keys.Up);
                            }
                            break;
                        }
                    case Keys.Down:
                        {
                            if (!KeyList.Contains(Keys.Down))
                            {
                                KeyList.Add(Keys.Down);
                            }
                            break;
                        }
                }

            }

            if (e.KeyData == Keys.Space)
            {
                if (gameStart)
                {
                    gameStart = false;
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

        }
        /// <summary>
        /// Timer for animation
        /// </summary>
        /// <param name="sender">sending object</param>
        /// <param name="e">Event arguments</param>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            CheckForCollision();
            if (KeyList.Contains(Keys.Left))
            {
                hippo.Move(Hippo.Direction.Left);
            }
            if (KeyList.Contains(Keys.Right))
            {
                hippo.Move(Hippo.Direction.Right);
            }
            if (KeyList.Contains(Keys.Up))
            {
                hippo.Move(Hippo.Direction.Up);
            }
            if (KeyList.Contains(Keys.Down))
            {
                hippo.Move(Hippo.Direction.Down);
            }
            
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

        /// <summary>
        /// checks for object collisions
        /// </summary>
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
        /// <summary>
        /// checks if a ball hits the hippo
        /// </summary>
        /// <param name="ball">ball object to check</param>
        /// <returns>true if collision occurs. false if none occur</returns>
        private bool BallHitsHippo(Ball ball)
        {
            if (ball.ballDisplayArea.IntersectsWith(hippo.DisplayArea))
            {
                ballsCollected += 1;
                return true;
            }
            return false;

        }
        /// <summary>
        /// checks if a mine hits the hippo
        /// </summary>
        /// <param name="mine">mine object to check</param>
        /// <returns>true if collision occurs. false if none occur</returns>
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
        /// <summary>
        /// timer used for creating balls and mines
        /// </summary>
        /// <param name="sender">sending object</param>
        /// <param name="e">event arguments</param>
        private void BallTimer_Tick(object sender, EventArgs e)
        {
            if (balls.Count < 15)
            {
                balls.Add(new Ball(this.DisplayRectangle));
            }
            if (mines.Count < maxMines)
            {
                mines.Add(new Mine(this.DisplayRectangle));
            }

         }
        /// <summary>
        /// Listens for Key up
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Key Event Arguments</param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
                switch (e.KeyData)
                {
                    case Keys.Left:
                        {
                            if (KeyList.Contains(Keys.Left))
                            {
                                KeyList.Remove(Keys.Left);
                            }
                            break;
                        }
                    case Keys.Right:
                        {
                            if (KeyList.Contains(Keys.Right))
                            {
                                KeyList.Remove(Keys.Right);
                            }
                            break;
                        }

                    case Keys.Up:
                        {
                            if (KeyList.Contains(Keys.Up))
                            {
                                KeyList.Remove(Keys.Up);
                            }
                            break;
                        }
                    case Keys.Down:
                        {
                            if (KeyList.Contains(Keys.Down))
                            {
                                KeyList.Remove(Keys.Down);
                            }
                            break;
                        }


                }
            
        }
    }
}
