using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xogame.Properties;

namespace xogame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enPlayer PlayTurn = enPlayer.Player1;
        stGameStatus GameStatus;
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public short PlayCount;
            public bool GameOver;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen WhitePen = new Pen(White);

            WhitePen.Width = 12;

            WhitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            WhitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(WhitePen, 350, 220, 850, 220);
            e.Graphics.DrawLine(WhitePen, 350, 335, 850, 335);

            e.Graphics.DrawLine(WhitePen, 500, 120, 500, 480);
            e.Graphics.DrawLine(WhitePen, 680, 120, 680, 480);
        }

        public void EndGame()
        {
            lblTurn.Text = "Game Over";
            
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                case enWinner.Draw:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.YellowGreen;
                btn2.BackColor = Color.YellowGreen;
                btn3.BackColor = Color.YellowGreen;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.GameOver = true;
                    GameStatus.Winner = enWinner.Player1;
                    EndGame();
                }
                else
                {
                    GameStatus.GameOver = true;
                    GameStatus.Winner = enWinner.Player2;
                    EndGame();
                }

                return true;
            }

            GameStatus.GameOver = false;
            return false;
        }

        public void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return; 

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;           
                                  
            if (CheckValues(button3, button6, button9))
                return;            
                                   
            if (CheckValues(button1, button5, button9))
                return;          
                                 
            if (CheckValues(button3, button5, button7))
                return;


            if (GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();  
            }


        }

        public void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        btn.Tag = "X";
                        PlayTurn = enPlayer.Player2;
                        GameStatus.PlayCount++;
                        lblTurn.Text = "Player 2";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        btn.Tag = "O";
                        GameStatus.PlayCount++;
                        PlayTurn = enPlayer.Player1;
                        lblTurn.Text = "Player 1";
                        CheckWinner();
                        break;
                }


            }
        }
        private void btn_click(object sender, EventArgs e)
        {
            ChangeImage(sender as Button);
        }

        public void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;    
            btn.BackColor = Color.Black;
            btn.Tag = "?";

        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            PlayTurn = enPlayer.Player1;
            GameStatus.PlayCount = 0;
            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";
            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.GameOver = false;

            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

        }
    }
}
