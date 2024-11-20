using System;
using System.Drawing;
using System.Windows.Forms;

namespace LastByteStandingSimulator
{
    public class MainGameplay : Form
    {
        private Label lblEnergy, lblLives, lblCoins;
        private Button btnICell, btnFarm, btnEndDay;
        private int playerEnergy = 100;
        private int playerLives = 3;
        private int coins = 50;

        public MainGameplay()
        {
            // Konfigurasi Form
            this.Text = "Main Gameplay";
            this.Size = new System.Drawing.Size(600, 400);

            // Label Energi
            lblEnergy = new Label();
            lblEnergy.Text = $"Energy: {playerEnergy}";
            lblEnergy.Location = new Point(20, 20);
            lblEnergy.Size = new Size(150, 30);

            // Label Nyawa
            lblLives = new Label();
            lblLives.Text = $"Lives: {playerLives}";
            lblLives.Location = new Point(200, 20);
            lblLives.Size = new Size(150, 30);

            // Label Koin
            lblCoins = new Label();
            lblCoins.Text = $"Coins: {coins}";
            lblCoins.Location = new Point(380, 20);
            lblCoins.Size = new Size(150, 30);

            // Tombol I-Cell
            btnICell = new Button();
            btnICell.Text = "I-Cell";
            btnICell.Location = new Point(100, 100);
            btnICell.Size = new Size(100, 50);
            btnICell.Click += BtnICell_Click;

            // Tombol Farm
            btnFarm = new Button();
            btnFarm.Text = "Farm";
            btnFarm.Location = new Point(250, 100);
            btnFarm.Size = new Size(100, 50);
            btnFarm.Click += BtnFarm_Click;

            // Tombol End Day
            btnEndDay = new Button();
            btnEndDay.Text = "End Day";
            btnEndDay.Location = new Point(400, 100);
            btnEndDay.Size = new Size(100, 50);
            btnEndDay.Click += BtnEndDay_Click;

            // Tambahkan elemen ke form
            this.Controls.Add(lblEnergy);
            this.Controls.Add(lblLives);
            this.Controls.Add(lblCoins);
            this.Controls.Add(btnICell);
            this.Controls.Add(btnFarm);
            this.Controls.Add(btnEndDay);
        }

        private void BtnICell_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            bool success = random.Next(0, 2) == 1;

            if (success)
            {
                coins += 20;
                playerEnergy -= 10;
                MessageBox.Show("You solved the challenge and earned 20 coins!", "Success");
            }
            else
            {
                playerEnergy -= 20;
                MessageBox.Show("You failed the challenge!", "Failed");
            }

            UpdateUI();
        }

        private void BtnFarm_Click(object sender, EventArgs e)
        {
            if (coins >= 10)
            {
                coins -= 10;
                playerEnergy -= 10;
                MessageBox.Show("You planted crops in the farm!", "Success");
            }
            else
            {
                MessageBox.Show("Not enough coins to plant!", "Failed");
            }

            UpdateUI();
        }

        private void BtnEndDay_Click(object sender, EventArgs e)
        {
            if (playerEnergy <= 0)
            {
                playerLives--;
                playerEnergy = 100; // Reset energi
                MessageBox.Show("You ran out of energ
