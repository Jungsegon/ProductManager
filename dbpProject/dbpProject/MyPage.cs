using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbpProject
{
    public partial class MyPage : Form
    {
        private string loggedInUserId;

        public MyPage(string userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
        }

        private void MyPage_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet11.ORDER_DETAILS' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.ordeR_DETAILSTableAdapter1.FillByUserId(this.dataSet11.ORDER_DETAILS, loggedInUserId);
            // TODO: 이 코드는 데이터를 'dataSet11.CART' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.cartTableAdapter.Fill(this.dataSet11.CART);

            LoadShoppingCart();

            ordeR_DETAILSTableAdapter1.FillByUserId(dataSet1.ORDER_DETAILS, loggedInUserId);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable mytable = dataSet1.Tables["ORDER_DETAILS"];
            foreach (DataRow mydataRow in mytable.Rows)
            {
                string orderDetailId = mydataRow["ORDER_DETAIL_ID"].ToString();
                string productId = mydataRow["PRODUCT_ID"].ToString();
                string status = mydataRow["BOOL"].ToString();

                // BOOL이 "확정"이면서 PRODUCT_ID에 해당하는 제품명만 출력
                if (status == "확정")
                {
                    // 해당하는 PRODUCT_ID에 대한 제품명을 가져오는 쿼리를 작성하여 사용
                    string productName = GetProductNameById(Convert.ToInt32(productId));

                    listBox1.Items.Add(orderDetailId + " " + productId + " " + productName);
                }
            }
        }
        private string GetProductNameById(int productId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                "PASSWORD=s5532960;" +
                "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                string selectQuery = "SELECT P_NAME FROM PRODUCT WHERE P_ID = :productId";

                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                    object result = selectCmd.ExecuteScalar();

                    return result != null ? result.ToString() : "";
                }
            }
        }

        private void LoadShoppingCart()
        {
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = true;
            //// DataGridView의 열과 행 크기를 자동으로 조절
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            // DataGridView를 읽기 전용으로 설정하여 사용자가 데이터를 직접 추가할 수 없게 함
            dataGridView1.ReadOnly = true;



            // TODO: 이 코드는 데이터를 'dataSet1.CART' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.cartTableAdapter.FillByUserId(this.dataSet1.CART, loggedInUserId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 주문 정보 생성

                DataSet1.ORDERLISTRow orderRow = dataSet1.ORDERLIST.NewORDERLISTRow();
                orderRow.ORDER_DATE = DateTime.Now;
                orderRow.USER_ID = loggedInUserId;
                //orderRow.SELLER_ID = 
                // 데이터베이스에서 시퀀스 값을 가져와서 주문 ID 설정
                using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                "PASSWORD=s5532960;" +
                "Data source=localhost:1521/xepdb1"))
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand("SELECT order_seq.NEXTVAL FROM DUAL", connection);
                    orderRow.ORDER_ID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 주문 정보를 데이터셋에 추가
                dataSet1.ORDERLIST.AddORDERLISTRow(orderRow);

                // 변경 내용을 데이터베이스에 반영
                this.orderlistTableAdapter1.Update(this.dataSet1.ORDERLIST);

                MessageBox.Show("주문이 완료되었습니다.");


                int rowCount = dataGridView1.Rows.Count;
                for (int i = 0; i < rowCount - 1; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    decimal productId = Convert.ToDecimal(row.Cells["pRODUCTIDDataGridViewTextBoxColumn"].Value);
                    String sellerId = Convert.ToString(row.Cells["sELLERIDDataGridViewTextBoxColumn"].Value);
                    int quantity = 1;  // 임시로 1로 설정, 수량을 지정하고 싶다면 그에 맞게 수정

                    // 주문 상세 정보 생성
                    DataSet1.ORDER_DETAILSRow orderDetailRow = dataSet1.ORDER_DETAILS.NewORDER_DETAILSRow();
                    orderDetailRow.ORDER_ID = orderRow.ORDER_ID;  // 주문 정보의 ID를 참조
                    orderDetailRow.PRODUCT_ID = productId;
                    orderDetailRow.QUANTITY = quantity;
                    orderDetailRow.USER_ID = loggedInUserId;
                    orderDetailRow.SELLER_ID = sellerId;
                    // 데이터베이스에서 시퀀스 값을 가져와서 주문 상세 정보의 ID 설정
                    using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
                    {
                        connection.Open();
                        OracleCommand cmd = new OracleCommand("SELECT order_detail_seq.NEXTVAL FROM DUAL", connection);
                        orderDetailRow.ORDER_DETAIL_ID = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 주문 상세 정보를 데이터셋에 추가
                    dataSet1.ORDER_DETAILS.AddORDER_DETAILSRow(orderDetailRow);
                }

                // 변경 내용을 데이터베이스에 반영
                this.ordeR_DETAILSTableAdapter1.Update(this.dataSet1.ORDER_DETAILS);


                // 주문 처리 후 장바구니 데이터 삭제
                ClearShoppingCart();
                dataGridView1.Refresh();
                // 장바구니 데이터 다시 로드
                LoadShoppingCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

        }
        private void ClearShoppingCart()
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // CART 테이블에서 loggedInUserId에 해당하는 모든 행 삭제
                string deleteQuery = "DELETE FROM CART WHERE USER_ID = :loggedInUserId";

                using (OracleCommand deleteCmd = new OracleCommand(deleteQuery, connection))
                {
                    deleteCmd.Parameters.Add(":loggedInUserId", OracleDbType.Varchar2).Value = loggedInUserId;

                    int rowsAffected = deleteCmd.ExecuteNonQuery(); // 삭제된 행의 수

                    
                }
            }

            // DataGridView 업데이트
            LoadShoppingCart();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // 새로고침 버튼
            // TODO: 이 코드는 데이터를 'dataSet11.ORDER_DETAILS' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.ordeR_DETAILSTableAdapter1.FillByUserId(this.dataSet11.ORDER_DETAILS, loggedInUserId);
            ordeR_DETAILSTableAdapter1.FillByUserId(dataSet1.ORDER_DETAILS, loggedInUserId);

            
            // DataGridView 업데이트
            dataGridView2.DataSource = oRDERDETAILSBindingSource;
            dataGridView2.Refresh();

            this.cartTableAdapter.FillByUserId(this.dataSet1.CART, loggedInUserId);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // ListBox에서 선택된 아이템 가져오기
            string selectedListItem = listBox1.SelectedItem as string;

            // 선택된 아이템이 없으면 메시지를 표시하고 종료
            if (string.IsNullOrEmpty(selectedListItem))
            {
                MessageBox.Show("목록에서 아이템을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 선택된 아이템에서 필요한 정보 추출 (예: "ORDER_DETAIL_ID PRODUCT_ID PRODUCT_NAME")
            string[] itemInfo = selectedListItem.Split(' ');
            string orderDetailId = itemInfo[0];
            string productId = itemInfo[1];

            // 후기를 입력할 키워드 가져오기
            string keyword = textBox1.Text;

            // 후기 내용 가져오기
            string contents = richTextBox1.Text;

            // 현재 날짜 가져오기
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // MEMO_TABLE에 추가할 행 생성
            DataRow newRow = dataSet1.MEMO_TABLE.NewRow();
            newRow["M_ID"] = GetNewMemoId(); // 새로운 메모 ID 생성 메서드 호출 (필요에 따라 구현)
            newRow["M_KEYWORD"] = keyword;
            newRow["M_DATE"] = currentDate;
            newRow["M_CONTENTS"] = contents;
            newRow["PRODUCT_ID"] = Convert.ToInt32(productId);

            // MEMO_TABLE에 행 추가
            dataSet1.MEMO_TABLE.Rows.Add(newRow);

            // 변경 내용 저장
            memO_TABLETableAdapter1.Update(dataSet1.MEMO_TABLE);

            textBox1.Clear();
            richTextBox1.Clear();
            MessageBox.Show("후기가 성공적으로 저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int GetNewMemoId()
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                using (OracleCommand cmd = new OracleCommand("SELECT MEMO_SEQ.NEXTVAL FROM DUAL", connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }




        private void label1_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            // 선택된 행이 있는지 확인
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // 선택된 행 가져오기
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // 선택된 행에서 필요한 열의 값을 가져오기
                string orderDetailId = selectedRow.Cells["oRDERDETAILIDDataGridViewTextBoxColumn"].Value.ToString();

                // 데이터베이스에서 "BOOL" 열 값을 "환불요청"으로 업데이트
                UpdateBoolColumn(orderDetailId);

                // datagridview2를 업데이트하여 변경 사항을 반영
                this.ordeR_DETAILSTableAdapter1.FillByUserId(this.dataSet11.ORDER_DETAILS, loggedInUserId);
                dataGridView2.Refresh();
            }
            else
            {
                MessageBox.Show("행을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show("환불 요청이 완료되었습니다.");
        }
        private void UpdateBoolColumn(string orderDetailId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                "PASSWORD=s5532960;" +
                "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                string updateQuery = "UPDATE ORDER_DETAILS SET BOOL = '환불요청' WHERE ORDER_DETAIL_ID = :orderDetailId";

                using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.Add(":orderDetailId", OracleDbType.Varchar2).Value = orderDetailId;

                    updateCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
