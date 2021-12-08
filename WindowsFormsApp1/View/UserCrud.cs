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

namespace WindowsFormsApp1.View
{
    public partial class UserCrud : Form
    {
        List<User> _users;
        bool isLoading = false;

        public UserCrud()
        {
            InitializeComponent();
            GetUsers();
            LoadUsers();
        }

        private void GetUsers()
        {
            try
            {
                var result = ApiHelper.Get(string.Empty, "users");

                _users = JsonConvert.DeserializeObject<List<User>>(result);
            }
            catch
            {
                MessageBox.Show("Vérifier votre connection internet");
            }
        }

        private void LoadUsers(string text = "")
        {
            bindingSource1.DataSource = _users;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.Add(new User()
            {
                Role = RoleEnum.User
            });
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                var value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value.ToString().GetSHA256();
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLoading)
            {
                isLoading = !isLoading;
                return;
            }

            if (e.RowIndex > _users.Count - 1)
                return;

            User user = _users[e.RowIndex];
            ApiHelper.Put("users", user.Id, user);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            User user = e.Row.DataBoundItem as User;
            ApiHelper.Delete("users", user.Id, e.Row.DataBoundItem);
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            _users.Last().Id = _users.Max(x => x.Id) + 1;
            ApiHelper.Post("users", dataGridView1.CurrentRow.DataBoundItem);
        }
    }
}
