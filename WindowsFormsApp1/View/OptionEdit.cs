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
    public partial class OptionEdit : Form
    {
        public List<ProductOption> options;
        public List<ProductOption> optionsSelected = new List<ProductOption>();

        public OptionEdit(List<ProductOption> options, List<ProductOption> selected)
        {
            InitializeComponent();
            this.options = options;
            checkedListBox1.Items.AddRange(options.Select(x => x.Name).ToArray());

            if (selected is null)
                return;

            for(int i = 0; i < options.Count; i++)
            {
                if (selected.Any(x => x.Id == options[i].Id))
                    checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var item in checkedListBox1.CheckedItems)
                optionsSelected.Add(options.First(x => x.Name == item));
            
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
