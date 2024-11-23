using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbpProject
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            usersTableAdapter1.Fill(dataSet1.USERS);

            DataTable mytable = dataSet1.Tables["USERS"];
            foreach (DataRow mydataRow in mytable.Rows)
            {
                listBox1.Items.Add(mydataRow["ID"].ToString());
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("사용자를 선택하세요.");
                return;
            }

            string userid = listBox1.SelectedItem.ToString();

            // ORDER_DETAILS 테이블 채우기
            this.oRDER_DETAILSTableAdapter.FillByHISTORY(this.dataSet1.ORDER_DETAILS, userid);

            // USERS 테이블에서 선택된 사용자의 행을 찾음
            DataTable mytable2 = dataSet1.Tables["USERS"];
            DataRow[] foundRows = mytable2.Select($"ID = '{userid}'");

            if (foundRows.Length > 0)
            {
                DataRow dataRow = foundRows[0];
                textBox1.Text = dataRow["COUNT"].ToString();
                textBox2.Text = dataRow["TOTAL"].ToString();
            }
            else
            {
                MessageBox.Show("선택된 사용자의 데이터를 찾을 수 없습니다.");
            }
        }
    }
}
