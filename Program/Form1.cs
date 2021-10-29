using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class Form1 : Form
    {
        private List<PictureBox> pictureBoxes;

        private string[,] board =
        {
           {"","","" },
           {"","","" },
           {"","","" }
        };


        private string ai;
        private string player;
        private string currentPlayer;

        private Bitmap playerImage;
        private Bitmap aiImage;

        private string circleDirectory = @"C:\Users\Andrew\source\repos\Program\Program\Resources\Circle.png";
        private string xDirectory = @"C:\Users\Andrew\source\repos\Program\Program\Resources\X.png";
        private string fileDirectory = @"C:\Users\Andrew\source\repos\Program\Program\Resources\Mone.jpg";

        public Form1()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox>
            {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6,
                pictureBox7,
                pictureBox8,
                pictureBox9
            };
            loadImages();
        }

        private bool equals3(string a, string b, string c)
        { 
           return a == b && b == c && a != ""; 
        }

        private string checkWinner()
        {
            string winner = "";

            //horizontal
            for (int i = 0; i < 3; i++)
            {

                if (equals3(board[i, 0], board[i, 1], board[i, 2]))
                {
                    winner = board[i, 0];
                }
            }

            //vertical
            for (int i = 0; i < 3; i++)
            {
                if (equals3(board[0, i], board[1, i], board[2, i]))
                {
                    winner = board[0, i];
                }
            }

            //diagonal
            if(equals3(board[0,0], board[1,1], board[2, 2]))
            {
                winner = board[0, 0];
            }
            if(equals3(board[2,0], board[1,1], board[0,2]))
            {
                winner = board[2, 0];
            }

            int openSpots = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(board[i,j] == "")
                    {
                        openSpots++;
                    }
                }
            }

            if (winner == "" && openSpots == 0)
            {
                return "tie";
            }
            else
                return winner;


        }

        private void setPlayer(PictureBox pictureBox)
        {
            
            int index1 = int.Parse(pictureBox.Tag.ToString()) / 3;
            int index2 = int.Parse(pictureBox.Tag.ToString()) % 3;

           
            board[index1, index2] = player;
            pictureBox.Image = playerImage;
            currentPlayer = ai;
            pictureBox.Enabled = false;
            bestMove();


            string winner = checkWinner();
            if (winner != "")
            {

                pictureBoxEnabled(false);

                MessageBox.Show($"Winner: {winner}");
            }
        }

        private void bestMove()
        {
            int bestScore = -1000;
            int moveX = 0, moveY = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(board[i, j] == "")
                    {
                        board[i, j] = ai;
                        int score = minimax(board, 0, false);
                        board[i, j] = "";

                        if(score > bestScore)
                        {
                            bestScore = score;
                            moveX = i;
                            moveY = j;
                        }

                    }
                }
            }

            board[moveX, moveY] = ai;
            pictureBoxes[moveX * 3 + moveY].Image = aiImage;
            currentPlayer = player;

        }



        private int minimax(string[,] board, int depth, bool isMaximizing)
        {
            string winner = checkWinner();
            int result = 0;
            if (winner != null)
            {
                if (winner == ai)
                    return 10;
                else if (winner == player)
                    return -10;
                else if (winner == "tie")
                    return 0;
            }



            if(isMaximizing)
            {
                int bestScore = -1000;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0;  j < 3;  j++)
                    {
                        if(board[i, j] == "")
                        {
                            board[i, j] = ai;
                            int score = minimax(board, depth + 1, false);
                            board[i, j] = "";
                            if (score > bestScore)
                                bestScore = score;
                        }
                    }
                }
                return bestScore;
            }
            else
            {

                int bestScore = 1000;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == "")
                        {
                            board[i, j] = player;
                            int score = minimax(board, depth + 1, true);
                            board[i, j] = "";
                            if (score < bestScore)
                                bestScore = score;
                        }
                    }
                }
                return bestScore;
            }

        }

        private void pictureBoxEnabled(bool enable)
            => pictureBoxes.ForEach((pb) => pb.Enabled = enable);

        private void loadImages()
            => pictureBoxes.ForEach((pb) => pb.Image = new Bitmap(Image.FromFile(fileDirectory)));

        private void pictureBox_Click(object sender, EventArgs e)
        {
            setPlayer(sender as PictureBox);
        }
     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            pictureBoxEnabled(true);

            player = comboBox1.SelectedItem.ToString();

            if (player == "X")
            {
                player = "X";
                playerImage = new Bitmap(Image.FromFile(xDirectory));

                ai = "O";
                aiImage = new Bitmap(Image.FromFile(circleDirectory));
            }
            else
            {
                player = "O";
                playerImage = new Bitmap(Image.FromFile(circleDirectory));

                ai = "X";
                aiImage = new Bitmap(Image.FromFile(xDirectory));
            }

            currentPlayer = player;
        }

        
    }
}
