using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stek_Labirint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int n=0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = "";
                n = Convert.ToInt32(textBox1.Text);
                dataGridView1.RowCount = n;
                dataGridView1.ColumnCount = n;
                dataGridView1.Width = this.Width - 110 ;
                dataGridView1.Height = this.Height - 50;
                dataGridView1.Visible = true;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        dataGridView1.Columns[i].Width = (this.Width-110)/n;
                        dataGridView1.Rows[i].Height = (this.Height - 50) / n;
                        dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", (this.Height - 250) / n, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                    }
            }
            catch
            {
                label2.Text = "Insert\n the number!";
            }
          
        }

     

        private void button2_Click_1(object sender, EventArgs e)
        {
            Stek stek = new Stek(n);
            int s = 0, Min=int.MaxValue;
            int[,] mas = new int[n, n], min=new int[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    try
                    {
                        mas[i, j] =  Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    }
                    catch
                    {
                        mas[i, j] = 1;
                        dataGridView1.Rows[i].Cells[j].Value = 1;
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    }
                }
            }
            int[][,] ways = stek.ways(mas);
            for (int i = 0; i < n; i++)
            {
                s = 0;
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        s += ways[i][j, k];
                    }
                }
                if (Min > s&&s!=0)
                {
                    Min = s;
                    min = ways[i];
                }
            }
            if (Min != 0 && Min != int.MaxValue)
            {
                label2.Text = Min.ToString() + " STEPS!";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        if (min[i, j] == 1)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor=Color.White;
                            dataGridView1.Rows[i].Cells[j].Value="🔴";
                        }
                }
            }
            else
            {
                label2.Text = "NO WAY!";
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            label2.Text = "WAIT...";
        }
    }
}
