using System;
using System.Windows.Forms;

namespace Domain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Main AddPerson = new Form_Main(textBox1.Text,  textBox2.Text);
            Visible = false;
            AddPerson.ShowDialog();
            Visible = true;
        }
    }
}
