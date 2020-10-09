using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabLINQ_M
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataAccess dataAcc = new DataAccess();


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1Example.DataSource = dataAcc.Query1Example();

            dataGridView1.DataSource = dataAcc.Query1();
            if (dataGridView1.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabQuery1"];

            dataGridView2.DataSource = dataAcc.Query2();
            if (dataGridView2.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabQuery2"];

            dataGridView3.DataSource = dataAcc.Query3();
            if (dataGridView3.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabQuery3"];

            dataGridView4.DataSource = dataAcc.Query4();
            if (dataGridView4.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabQuery4"];

            IOrderedEnumerable<IGrouping<string, Models.Tutors>> groupsEx = dataAcc.Task5Example();
            if (groupsEx != null)
            {
                foreach (var gr in groupsEx)
                {
                    textBoxGroupExample.Text += gr.Key + "\r\n";
                    foreach (Models.Tutors t in gr)
                    {
                        textBoxGroupExample.Text += "    " + t.NameFio + "\r\n";
                    }
                }
            }

            IOrderedEnumerable<IGrouping<string, Models.Students>> groupsSt = dataAcc.Task5();
            if (groupsSt != null)
            {
                foreach (var gr in groupsSt)
                {
                    textBoxGroup.Text += gr.Key + "\r\n";
                    foreach (Models.Students st in gr)
                    {
                        textBoxGroup.Text += "    " + st.Surname + " " + (st.Name ?? "") + " " + (st.Patronymic ?? "");
                        if (st.Group != null)
                            textBoxGroup.Text += ", " + (st.Group.GroupNumber ?? "");
                        textBoxGroup.Text += "\r\n";
                    }
                }
            }

            if (textBoxGroup.Text.Length > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabTask5"];

            object Task6DataEx = dataAcc.Task6Example();
            dataGridViewAggrExample.DataSource = Task6DataEx;
            dataGridViewAggrExample.Refresh();

            object Task6Data = dataAcc.Task6();
            dataGridViewAggr.DataSource = Task6Data;
            if (dataGridViewAggr.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabTask6"];

            object Task7DataEx = dataAcc.Task7Example();
            dataGridView7Example.DataSource = Task7DataEx;
            dataGridView7Example.Refresh();

            object Task7Data = dataAcc.Task7();
            dataGridView7.DataSource = Task7Data;
            if (dataGridView7.RowCount > 0)
                tabControl.SelectedTab = tabControl.TabPages["tabTask7"];
        }
    }
}
