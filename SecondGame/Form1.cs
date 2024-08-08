using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SecondGame
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer musicplayer = new WindowsMediaPlayer();
        int ballXspeed = 4;
        int ballYspeed = 4;
        int ballSpeed = 2;
        Random random = new Random();
        bool goDown, goUp;
        int Computer_speed_Change = 50;
        int playerScore = 0;
        int computerScore = 0;
        int playerSpeed = 8;
        int[] i = { 5, 6, 8, 9 };
        int[] j = { 10, 9, 8, 11, 12 };

        public Form1()
        {
            InitializeComponent();
            musicplayer.URL = "door-to-unreality-175605.mp3";
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            ball.Top -=ballYspeed;
            ball.Left -=ballXspeed;

            this.Text = "Pong Game" + "Player Score: " + playerScore + "-- Computer Score: " + computerScore;
            if(ball.Top < 0|| ball. Bottom > this.ClientSize.Height)
            {
                ballYspeed = -ballYspeed;
            }
            if (ball.Left < -2)
            {
                ball.Left = 300;
                ballXspeed = -ballXspeed;
                computerScore++;
            }
            if(ball.Right > this.ClientSize.Width + 2)
            {
                ball.Left = 300;
                ballXspeed = -ballXspeed;
                playerScore++;
            }
            if(computer.Top <= 1)
            {
                computer.Top = 0;
            }
            else if(computer.Bottom >= this.ClientSize.Height)
            {
                computer.Top = this.ClientSize.Height - computer.Height;
            }
            if (ball.Top < computer.Top + (computer.Height / 2) &&  ball.Left > 300)
            {
                computer.Top -= ballSpeed;
            }
            if(ball.Top > computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                computer.Top += ballSpeed;
            }
            Computer_speed_Change -= 1;
            
            if (Computer_speed_Change < 0)
            {
                ballSpeed = i[random.Next(i.Length)];
                Computer_speed_Change = 50;

            }
            if(goDown && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }
            if(goUp && player.Top > 0)
            {
                player.Top -= playerSpeed;
            }
            CheckCollision(ball, player, player.Right + 5);
            CheckCollision(ball, computer, computer.Left - 35);

            if(computerScore > 5)
            {
                GameOver("You have lost the game");
            }
            else if(playerScore > 5)
            {
                GameOver("You have won the game");
            }
        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if(e.KeyCode == Keys.Up)
            {
                goUp = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if(e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
        }
        private void CheckCollision(PictureBox PicOne, PictureBox PicTwo, int offset)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                PicOne.Left = offset;

                int x = j[random.Next(j.Length)];
                int y = j[random.Next(j.Length)];

                if(ballXspeed< 0)
                {
                    ballXspeed = x;
                }
                else
                {
                    ballXspeed = -x;
                }
                if (ballYspeed< 0)
                {
                    ballYspeed = -y;
                }
                else
                {
                    ballYspeed = y;
                }
            }

        }
        private void GameOver(string message)
        {
            GameTimer.Stop();
            MessageBox.Show(message, "Sam Rock: ");
            computerScore = 0;
            playerScore = 0;
            ballXspeed = ballYspeed = 4;
            Computer_speed_Change = 50;
            GameTimer.Start();

        }
    }
}
