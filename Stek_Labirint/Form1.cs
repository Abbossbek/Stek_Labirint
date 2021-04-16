using Newtonsoft.Json.Linq;

using Perceptron.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        //List<int> data;
        int tetta;
        int[] weight = new int[25];
        List<int[]> listData = new List<int[]>();
        public Form1()
        {
            InitializeComponent();
            
            int m = 5;
            dataGridView1.RowCount = m;
            dataGridView1.ColumnCount = m;
            dataGridView1.Visible = true;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                {
                    dataGridView1.Columns[i].Width = (dataGridView1.Width) / m;
                    dataGridView1.Rows[i].Height = (dataGridView1.Height) / m;
                    dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", (dataGridView1.Height) / m, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
        }
        public int n=0;

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                n = Convert.ToInt32(txbSize.Text);
            }
            catch
            {
                MessageBox.Show("Enter n correctly!", "Error");
                return;
            }
            for(int i = 0; i < n; i++)
            {
                flowLayoutPanel1.Controls.Add(new Item());
            }
            btnFindWeight.Enabled = true;
        }

        private void btnFindWeight_Click(object sender, EventArgs e)
        {
            weight = new int[25];
            try
            {
                tetta = Convert.ToInt32(tbTetta.Text);
            }
            catch
            {
                MessageBox.Show("Enter T correctly!", "Error");
                return;
            }
            listData.Clear();
            List<int> classes = new List<int>();
            foreach (Item item in flowLayoutPanel1.Controls)
            {
                if (item.Class != -1)
                    classes.Add(item.Class);
                else
                    MessageBox.Show("Enter class correctly!", "Error");
                int[] data = new int[25];
                for (int i = 0; i < item.DataGridView.Rows.Count; i++)
                    for (int j = 0; j < item.DataGridView.Columns.Count; j++)
                    {
                        if(item.DataGridView.Rows[i].Cells[j].Value != "")
                        {
                            data[i * item.DataGridView.Columns.Count + j] = 1;
                        }
                        else
                        {
                            data[i * item.DataGridView.Columns.Count + j] = 0;
                        }
                    }
                listData.Add(data);
            }

            int[] results = new int[n];
            while(!IsEqualItems(results, classes.ToArray()))
            {
                for (int i=0;i<listData.Count;i++)
                {
                    int outSum = 0;
                    for (int j=0; j<weight.Length; j++)
                    {
                        outSum += weight[j]*listData[i][j];
                    }
                    if (outSum <= tetta)
                    {
                        results[i] = 0;
                    }
                    else
                    {
                        results[i] = 1;
                    }
                    if (results[i] != classes[i])
                    {
                        for (int j = 0; j < weight.Length; j++)
                        {
                            weight[j] += results[i]==0? listData[i][j]:-listData[i][j];
                        }
                    }
                }
            }
            lblWeight.Text = "weight = {" + string.Join(",", weight) + "}"; 
            btnFindClass.Enabled = true;
        }

        private bool IsEqualItems(int[] sums, int[] vs)
        {
            for (int i = 0; i<sums.Length;i++)
            {
                if (sums[i] != vs[i])
                    return false;
            }
            return true;
        }

        private void btnFindWeight_MouseDown(object sender, MouseEventArgs e)
        {
            lblWeight.Text = "WAIT...";
        }

        private void btnFindClass_Click(object sender, EventArgs e)
        {
            int outSum = 0;
            int[] data = new int[25];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != "")
                    {
                        data[i * dataGridView1.Columns.Count + j] = 1;
                    }
                    else
                    {
                        data[i * dataGridView1.Columns.Count + j] = 0;
                    }
                }
            for (int j = 0; j < weight.Length; j++)
            {
                outSum += weight[j] * data[j];
            }
            if (outSum == 0)
            {
                MessageBox.Show("Fill data");
                return;
            }
            if (outSum <= tetta)
            {
                lblClass.Text = "Class: 0";
            }
            else
            {
                lblClass.Text = "Class: 1";
            }
        }
    }
}
