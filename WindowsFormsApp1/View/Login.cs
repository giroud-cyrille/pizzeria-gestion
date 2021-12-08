using Newtonsoft.Json;
using ProjetcLibrary.Extensions;
using ProjetcLibrary.Models;
using ProjetcLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.View;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public bool AuthentificiedSucces
        {
            get;
            private set;
        }

        public User User
        {
            get;
            set;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var test = "test";
                var hash = test.GetSHA256();
                var result = ApiHelper.Get(loginText.Text, "users");
                User user = JsonConvert.DeserializeObject<User>(result);

                if(user is null || user.PasswordHash != passwordText.Text.GetSHA256())
                {
                    MessageBox.Show("Identifiants incorrect");
                    return;
                }

                if(user.Role != RoleEnum.Administrator)
                {
                    MessageBox.Show("Cet utilisateur n'a pas les droits requis");
                    return;
                }

                User = user;
                AuthentificiedSucces = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Identification impossible. Vérifier votre connection internet.");
                return;
            }
        }
    }
}
