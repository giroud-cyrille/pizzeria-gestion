using Newtonsoft.Json;
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
    public partial class OptionCrud : Form
    {
        List<ProductOption> _options;
        bool isLoaded = false;

        public OptionCrud()
        {
            InitializeComponent();
            try
            {
                var result = ApiHelper.Get(string.Empty, "options");

                _options = JsonConvert.DeserializeObject<List<ProductOption>>(result);
                bindingSource1.DataSource = _options;
            }
            catch
            {
                MessageBox.Show("Impossible d'accéder aux données. Vérifier votre connexion internet");
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            ApiHelper.Post("options", dataGridView1.CurrentRow.DataBoundItem);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            ProductOption user = e.Row.DataBoundItem as ProductOption;
            ApiHelper.Delete("options", user.Id, e.Row.DataBoundItem);
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLoaded)
            {
                isLoaded = !isLoaded;
                return;
            }

            if (e.RowIndex > _options.Count - 1)
                return;

            ProductOption user = _options[e.RowIndex];
            ApiHelper.Put("options", user.Id, user);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
