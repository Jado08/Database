using System;
using System.Windows.Forms;

namespace SimpleStudentRegistrationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            student1.Visible = true;
        }
    }
}
