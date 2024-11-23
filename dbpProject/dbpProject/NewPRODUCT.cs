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
    public partial class NewPRODUCT : Form
    {
        public NewPRODUCT()
        {
            InitializeComponent();
        }

        private void NewPRODUCT_Load(object sender, EventArgs e)
        {
            LoadSellerIDs();
        }
        private void LoadSellerIDs()
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = "SELECT SELLER_ID FROM SELLER";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["SELLER_ID"].ToString());
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 각 필드에서 값 가져오기
            string pName = textBox1.Text;
            decimal price = decimal.Parse(textBox2.Text); // 가격은 decimal로 변환
            int inventory = int.Parse(textBox3.Text); // 재고량은 int로 변환
            int type = int.Parse(textBox4.Text); // 타입은 int로 변환
            string sellerId = listBox1.SelectedItem.ToString(); // 선택된 판매자 ID

            // 새 제품 ID 가져오기
            int newProductId = GetNewProductId();

            // 데이터베이스에 제품 추가
            AddNewProduct(newProductId, pName, price, inventory, type, sellerId);

        }

        private int GetNewProductId()
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = "SELECT MAX(P_ID) FROM PRODUCT";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        return Convert.ToInt32(result) + 1;
                    }
                    return 1; // 테이블이 비어있을 경우
                }
            }
        }

        private void AddNewProduct(int pId, string pName, decimal price, int inventory, int type, string sellerId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = "INSERT INTO PRODUCT (P_ID, P_NAME, PRICE, INVENTORY, SELLERID, TYPE, COUNT, RECOUNT) " +
                               "VALUES (:pId, :pName, :price, :inventory, :sellerId, :type, 0, 0)";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("pId", pId));
                    command.Parameters.Add(new OracleParameter("pName", pName));
                    command.Parameters.Add(new OracleParameter("price", price));
                    command.Parameters.Add(new OracleParameter("inventory", inventory));
                    command.Parameters.Add(new OracleParameter("sellerId", sellerId));
                    command.Parameters.Add(new OracleParameter("type", type));

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("제품이 성공적으로 추가되었습니다.");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
