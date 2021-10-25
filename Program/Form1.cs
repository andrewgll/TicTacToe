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

        private void setPlayer(PictureBox pictureBox, int index1, int index2)
        {
            if (currentPlayer == player)
            {
                board[index1, index2] = player;
                pictureBox.Image = playerImage;
                currentPlayer = ai;
            }
            else
            {
                board[index1, index2] = ai;
                pictureBox.Image = aiImage;
                currentPlayer = player;
            }

            pictureBox.Enabled = false;
            string winner = checkWinner();
            if (winner != "")
            {
                pictureBox1.Enabled = false;
                pictureBox2.Enabled = false;
                pictureBox3.Enabled = false;
                pictureBox4.Enabled = false;
                pictureBox5.Enabled = false;
                pictureBox6.Enabled = false;
                pictureBox7.Enabled = false;
                pictureBox8.Enabled = false;
                pictureBox9.Enabled = false;

                MessageBox.Show($"Winner: {winner}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            setPlayer(pictureBox1, 0,0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox2,0,1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox3,0,2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox4,1,0);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox5,1,1);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox6,1,2);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox7,2,0);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox8,2,1);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

            setPlayer(pictureBox9,2,2);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox4.Enabled = true;
            pictureBox5.Enabled = true;
            pictureBox6.Enabled = true;
            pictureBox7.Enabled = true;
            pictureBox8.Enabled = true;
            pictureBox9.Enabled = true;

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

        private void loadImages()
        {
            pictureBox1.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox2.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox3.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox4.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox5.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox6.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox7.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox8.Image = new Bitmap(Image.FromFile(fileDirectory));
            pictureBox9.Image = new Bitmap(Image.FromFile(fileDirectory));
        }
    }
}
