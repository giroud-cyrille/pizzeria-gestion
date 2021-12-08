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

        public ProductCrud()
        {
            InitializeComponent();
            try
            {
                var result = ApiHelper.Get(string.Empty, "products");

                _products = JsonConvert.DeserializeObject<List<Produit>>(result);
                LoadProducts(string.Empty);
            }
            catch
            {
                MessageBox.Show("Impossible d'accéder aux données. Vérifier votre connexion internet");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text);
        }

        void LoadProducts(string text)
        {
            listBox1.Items.Clear();

            if (string.IsNullOrEmpty(text))
                listBox1.Items.AddRange(_products.Select(x => x.Title).ToArray());
            else
                listBox1.Items.AddRange(_products.Where(x => x.Title.Contains(text)).ToArray());
        }
    }
}
