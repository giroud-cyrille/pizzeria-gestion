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
    public partial class ProductCrud : Form
    {
        List<Produit> _products;
        List<ProductOption> _options;

        bool isLoaded = false;

        public ProductCrud()
        {
            InitializeComponent();
            try
            {
                var result = ApiHelper.Get(string.Empty, "produits");

                _products = JsonConvert.DeserializeObject<List<Produit>>(result);

                result = ApiHelper.Get(string.Empty, "options");

                _options = JsonConvert.DeserializeObject<List<ProductOption>>(result);

                LoadProducts(string.Empty);
            }
            catch
            {
                MessageBox.Show("Impossible d'accéder aux données. Vérifier votre connexion internet");
            }

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Suppléments",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 50,
            });
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void LoadProducts(string text)
        {
            bindingSource1.DataSource = _products;
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            ApiHelper.Post("produits", dataGridView1.CurrentRow.DataBoundItem);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is null)
                return;

            Produit user = e.Row.DataBoundItem as Produit;
            ApiHelper.Delete("produits", user.Id, e.Row.DataBoundItem);
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLoaded)
            {
                isLoaded = !isLoaded;
                return;
            }

            if (e.RowIndex > _products.Count - 1)
                return;

            Produit user = _products[e.RowIndex];
            ApiHelper.Put("produits", user.Id, user);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                Produit product = _products[e.RowIndex];

                if (product is null)
                    return;

                var options = new OptionEdit(_options, product.Options);

                if(options.ShowDialog() == DialogResult.OK)
                {
                    product.Options = options.optionsSelected;
                }
            }
        }
    }
}
