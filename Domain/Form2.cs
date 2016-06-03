using System;
using System.Windows.Forms;
using Brokers;
using Persons;

namespace Domain
{
    public partial class Form_Main : Form
    {
        private Broker b;

        public Form_Main(string log, string pass)
        {
            InitializeComponent();
            b = new Broker();

            if (log != "Admin" || pass != "123")
            {
                tabControl1.TabPages.RemoveAt(2);
                tabControl1.TabPages.RemoveAt(1); 
            }
        }

        private void btn_AddPerson_Click(object sender, EventArgs e)
        {
            Person p = new Person();

            p.FirstName = tBox_FirstName.Text;
            p.LastName = tBox_LastName.Text;
            p.Class = Convert.ToInt32(UpDown_Class.Text);
            p.Progress = cBox_Progress.Text;
            p.Omissions = Convert.ToInt32(UpDown_Omissions.Text);

            b.Insert(p);

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void btn_InsertControl_Click(object sender, EventArgs e)
        {
            Persons.Control c = new Persons.Control();

            c.Date = dTimePicker_Control.Value;
            c.Lesson = tBox_Control_Lesson.Text;
            c.Mark = Convert.ToInt32(UpDown_Control_Mark.Text);
            c.Presence = cBox_Presence.Checked;
            c.ID_student = Convert.ToInt32(UpDown_Control_IDStudent.Text);

            b.Insert_Control(c);

            Form_Main_Load(sender, e);
            controlDataGridView.Update();
            controlDataGridView.Refresh();
        }

        private void btn_Choose_Click(object sender, EventArgs e)
        {
            cBox_Persons.DataSource = b.FillComboBox();
        }

        private void btn_Upd_Click_1(object sender, EventArgs e)
        {
            Person oldPerson = new Person();
            Person newPerson = new Person();

            oldPerson = cBox_Persons.SelectedItem as Person;

            newPerson.FirstName = tBox_NewFirstName.Text;
            newPerson.LastName = tBox_NewLastName.Text;
            newPerson.Class = Convert.ToInt32(UpDown_NewClass.Text);
            newPerson.Omissions = Convert.ToInt32(UpDown_NewOmissions.Text);
            newPerson.Progress = cBox_NewProgress.Text;

            b.Update(oldPerson, newPerson);

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p = cBox_Persons.SelectedItem as Person;

            b.Delete(p);

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            personsDataGridView.DataSource = b.Rebuild();

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Extended". При необходимости она может быть перемещена или удалена.
            this.extendedTableAdapter.Fill(this._UPP_1DataSet.Extended);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Control". При необходимости она может быть перемещена или удалена.
            this.controlTableAdapter.Fill(this._UPP_1DataSet.Control);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Persons". При необходимости она может быть перемещена или удалена.
            this.personsTableAdapter.Fill(this._UPP_1DataSet.Persons);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Control". При необходимости она может быть перемещена или удалена.
            this.controlTableAdapter.Fill(this._UPP_1DataSet.Control);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Persons". При необходимости она может быть перемещена или удалена.
            this.personsTableAdapter.Fill(this._UPP_1DataSet.Persons);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_UPP_1DataSet.Persons". При необходимости она может быть перемещена или удалена.
            this.personsTableAdapter.Fill(this._UPP_1DataSet.Persons);
        }

        private void personsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView2.DataSource = b.JoinReader(int.Parse(personsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()));

            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView4.DataSource = b.JoinReader((int)personsDataGridView.Rows[e.RowIndex].Cells[0].Value);
            controlDataGridView1.DataSource = b.JoinReaderHistory((int)personsDataGridView.Rows[e.RowIndex].Cells[0].Value);
            //dataGridView4.DataSource = b.JoinReader();

            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void btn_CLearDGV_Click_1(object sender, EventArgs e)
        {
            b.Clear();

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void btn_ChooseControl_Click(object sender, EventArgs e)
        {
            cBox_ChooseControl.DataSource = b.FillComboBox_Control();
        }

        private void btn_Control_Edit_Click(object sender, EventArgs e)
        {
            Persons.Control oldControl = new Persons.Control();
            Persons.Control newControl = new Persons.Control();

            oldControl = cBox_ChooseControl.SelectedItem as Persons.Control;

            newControl.Date = dTimePicker_NewControl.Value;
            newControl.Lesson = tBox_Control_NewLesson.Text;
            newControl.Mark = Convert.ToInt32(UpDown_Control_NewMark.Text);
            newControl.Presence = cBox_Control_NewPresense.Checked;
            newControl.ID_student = Convert.ToInt32(UpDown_Control_NewIDStudent.Text);

            b.Update_Control(oldControl, newControl);

            Form_Main_Load(sender, e);
            controlDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void controlDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DGV_Control_Extended.DataSource = b.JoinReaderControl_2(int.Parse(controlDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));

            DGV_Control_Person.DataSource = b.JoinReaderControl_1(int.Parse(controlDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));

            controlDataGridView.Update();
            controlDataGridView.Refresh();
        }

        private void btn_Control_Delete_Click(object sender, EventArgs e)
        {
            Persons.Control c = new Persons.Control();
            c = cBox_ChooseControl.SelectedItem as Persons.Control;

            b.Delete_Control(c);

            Form_Main_Load(sender, e);
            personsDataGridView.Update();
            personsDataGridView.Refresh();
        }

        private void controlDataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView4.DataSource = b.JoinReader(int.Parse(controlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
        }

        private void btn_Control_Clear_Click(object sender, EventArgs e)
        {
            b.Clear_Control();

            Form_Main_Load(sender, e);
            controlDataGridView.Update();
            controlDataGridView.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы держитесь здесь! Счастья вам, хорошего настроения и здоровья!", "Денег нет.");
        }
    }
}
