using System;
using System.Windows.Forms;

namespace LastByteStandingSimulator
{
    public class MainForm : Form
    {
        private Button btnStart;
        private Button btnExit;

        public MainForm()
        {
            // Konfigurasi Form
            this.Text = "Last Byte Standing";
            this.Size = new System.Drawing.Size(400, 300);

            // Tombol Start
            btnStart = new Button();
            btnStart.Text = "Start";
            btnStart.Size = new System.Drawing.Size(100, 50);
            btnStart.Location = new System.Drawing.Point(150, 80);
            btnStart.Click += BtnStart_Click;

            // Tombol Exit
            btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Size = new System.Drawing.Size(100, 50);
            btnExit.Location = new System.Drawing.Point(150, 150);
            btnExit.Click += BtnExit_Click;

            // Tambahkan tombol ke form
            this.Controls.Add(btnStart);
            this.Controls.Add(btnExit);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Buka Scene Gameplay
            MainGameplay gameplayForm = new MainGameplay();
            gameplayForm.Show();
            this.Hide(); // Sembunyikan Starting Page
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Keluar dari aplikasi
        }
    }
}
