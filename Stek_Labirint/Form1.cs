using Newtonsoft.Json.Linq;

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
        List<bool> data;
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("data.json"))
            {
                File.Create("data.json").Close();
            }
        }
        public int n=0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lblResult.Text = "";
                n = Convert.ToInt32(txbSize.Text);
                dataGridView1.RowCount = n;
                dataGridView1.ColumnCount = n;
                dataGridView1.Width = this.Width - 220 ;
                dataGridView1.Height = this.Height - 50;
                dataGridView1.Visible = true;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        dataGridView1.Columns[i].Width = (this.Width-220)/n;
                        dataGridView1.Rows[i].Height = (this.Height - 50) / n;
                        dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", (this.Height - 250) / n, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    }
            }
            catch
            {
                lblResult.Text = "Insert\n the number!";
            }
          
        }

     

        private void button2_Click_1(object sender, EventArgs e)
        {
            bool hasResult = false;
            string json = File.ReadAllText("data.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                List<Perceptron.Models.Perceptron> perceptrons = JArray.Parse(json).ToObject<List<Perceptron.Models.Perceptron>>();
                data = new List<bool>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        data.Add(!dataGridView1.Rows[i].Cells[j].Value.Equals(""));
                    }
                }
                var perceptron = perceptrons.FirstOrDefault(p => p.Data?.SequenceEqual(data.ToArray())??false);
                if (perceptron != null)
                {
                    lblResult.Text = perceptron.Result;
                    hasResult = true;
                }
            }
            
            if(!hasResult)
            {
                txbNewResult.Visible = true;
                btnSave.Visible = true;
                lblResult.Text = "I cannot find. What is this?";
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            lblResult.Text = "WAIT...";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Perceptron.Models.Perceptron> perceptrons;
            string json = File.ReadAllText("data.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                perceptrons = JArray.Parse(json).ToObject<List<Perceptron.Models.Perceptron>>();
            }
            else
            {
                perceptrons = new List<Perceptron.Models.Perceptron>();
            }
            perceptrons.Add(new Perceptron.Models.Perceptron()
            {
                Id = perceptrons.Count,
                Data = data.ToArray(),
                Result = txbNewResult.Text
            });
            File.WriteAllText("data.json", JArray.FromObject(perceptrons).ToString());
            txbNewResult.Text = "";
            txbNewResult.Visible = false;
            btnSave.Visible = false;
            lblResult.Text = "Saved. Thank you!";
        }
    }
}
