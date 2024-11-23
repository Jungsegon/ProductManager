using Oracle.ManagedDataAccess.Client;
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
    public partial class ReviewForm : Form
    {
        private int product_Id;

        public ReviewForm(int productId)
        {
            InitializeComponent();
            product_Id = productId;
        }
        

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            FillListBoxWithKeywords();

        }
        private void FillListBoxWithKeywords()
        {
            string connectionString = "USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"; // 연결 문자열 설정
            string query = "SELECT m_keyword FROM MEMO_TABLE WHERE product_Id = :product_Id"; // 적절한 쿼리 작성

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("product_Id", product_Id));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["m_keyword"].ToString());
                        }
                    }
                }
            }
        }


        

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selectedKeyword = listBox1.SelectedItem.ToString();
            string connectionString = "USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"; // 연결 문자열 설정
            string query = "SELECT m_date, m_contents FROM MEMO_TABLE WHERE m_keyword = :selectedKeyword AND product_Id = :product_Id"; // 적절한 쿼리 작성

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("selectedKeyword", selectedKeyword));
                    command.Parameters.Add(new OracleParameter("product_Id", product_Id));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string reviewContent = reader["m_date"].ToString() + Environment.NewLine + reader["m_contents"].ToString();
                            richTextBox1.Text = reviewContent;
                        }
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
