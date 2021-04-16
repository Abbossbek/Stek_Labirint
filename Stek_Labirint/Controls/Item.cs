using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron.Controls
{
    public partial class Item : UserControl
    {
        public DataGridView DataGridView
        {
            get
            {
                return dataGridView1;
            }
            set
            {
                dataGridView1 = value;
            }
        }
        public int Class { get
            {
                try
                {
                    return Convert.ToInt32(textBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return -1;
                }
            } set { textBox1.Text = value.ToString(); } }
        public Item()
        {
            InitializeComponent();
            int n = 5;
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            dataGridView1.Visible = true;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Columns[i].Width = (dataGridView1.Width) / n;
                    dataGridView1.Rows[i].Height = (dataGridView1.Height) / n;
                    dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", (dataGridView1.Height) / n, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
        }
    }
}
