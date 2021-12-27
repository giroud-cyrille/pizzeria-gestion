
using ProjetcLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.View
{
    public partial class Main : Form
    {
        private Form activeForm;

        public Main(User user)
        {
            InitializeComponent();
            this.user.Text = user.Login;
            OpenChild(new ProductCrud());
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChild(new UserCrud());
        }

        private void OpenChild(Form child)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = child;

            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            panelChild.Controls.Add(child);
            panelChild.Tag = child;

            child.BringToFront();
            child.Show();
        }

        private void CloseChild()
        {
            if (activeForm != null)
                activeForm.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChild(new ProductCrud());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChild(new OptionCrud());
        }
    }
}
