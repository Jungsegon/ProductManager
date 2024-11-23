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
    public partial class SellerForm : Form
    {
        private string loggedInUserId;  // 전역 변수로 로그인한 사용자의 아이디 저장
        public SellerForm(String userId)
        {
            InitializeComponent();
            loggedInUserId = userId;

           
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet1.ORDERLIST' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.oRDER_DETAILSTableAdapter.Fill(this.dataSet1.ORDER_DETAILS, loggedInUserId);
            // DataGridView를 읽기 전용으로 설정하여 사용자가 데이터를 직접 추가할 수 없게 함
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // DataGridView의 CellClick 이벤트에 이벤트 핸들러를 추가
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView2.CellClick += dataGridView1_CellClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 새로고침 버튼
            // 데이터를 다시 로드
            this.oRDER_DETAILSTableAdapter.Fill(this.dataSet1.ORDER_DETAILS, loggedInUserId);

            // DataGridView 업데이트
            dataGridView1.DataSource = oRDERDETAILSBindingSource;
            dataGridView1.Refresh();
            DisplaySellerTotal(loggedInUserId);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 셀을 클릭하면 해당 행을 선택
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 구매확정 버튼
            try
            {
                // 선택된 행의 정보 확인
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // 선택된 행의 정보를 사용하여 구매확정 로직
                    int orderDetailId = Convert.ToInt32(row.Cells["oRDERDETAILIDDataGridViewTextBoxColumn"].Value);

                    // 구매확정 상태 업데이트
                    UpdateOrderDetailStatus(orderDetailId, "확정");
                    UpdateOrderDetailStatus(orderDetailId);
                    // 상품 재고량 감소
                    int productId = Convert.ToInt32(row.Cells["pRODUCTIDDataGridViewTextBoxColumn"].Value);
                    DecreaseInventory(productId);
                    String userid = Convert.ToString(row.Cells["uSERIDDataGridViewTextBoxColumn"].Value);
                    // 사용자 COUNT 증가
                    IncreaseUserCount(userid);

                    UpdateUserTotal(userid, productId);

                    // 상품 가격을 가져오기
                    int productPrice = GetProductPrice(productId);

                    // 판매자의 TOTAL 업데이트
                    UpdateSellerTotal(productId, productPrice);

                    // 영수증 정보 생성 및 표시
                    string receipt = GenerateReceipt(orderDetailId, productId, userid);
                    MessageBox.Show(receipt, "영수증");

                }

                // 새로고침
                button1_Click(sender, e);

                MessageBox.Show("구매확정이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }

        private void UpdateOrderDetailStatus(int orderDetailId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 날짜를 가져옵니다.
                DateTime currentDate = DateTime.Now;

                // 쿼리문 작성
                // BOOL_DATE 필드에 현재 날짜를 설정합니다.
                string updateQuery = @"
            UPDATE ORDER_DETAILS 
            SET BOOL_DATE = :boolDate 
            WHERE ORDER_DETAIL_ID = :orderDetailId";

                using (OracleCommand cmd = new OracleCommand(updateQuery, connection))
                {
                    // 매개변수 추가
                    cmd.Parameters.Add(":boolDate", OracleDbType.Date).Value = currentDate;
                    cmd.Parameters.Add(":orderDetailId", OracleDbType.Int32).Value = orderDetailId;

                    // 쿼리 실행
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GenerateReceipt(int orderDetailId, int productId, string userId)
        {
            // 영수증 생성 로직

            int productPrice = GetProductPrice(productId);
            string receipt = $"주문 상세 번호 : {orderDetailId}\n" +
                             $"제품 번호 : {productId}\n" +
                             $"구매자 ID : {userId}\n" +
                             $"구매 가격 : {productPrice}\n" +
                             $"구매 확정 일자 : {DateTime.Now}\n";

            return receipt;
        }

        private int GetProductPrice(int productId)
        {
            using (var productAdapter = new DataSet1TableAdapters.PRODUCTTableAdapter())
            {
                var productTable = productAdapter.GetDataById(productId);
                if (productTable.Rows.Count == 0)
                {
                    throw new Exception("Product not found.");
                }

                return Convert.ToInt32(productTable[0]["PRICE"]);
            }
        }

        private void UpdateSellerTotal(int productId, int productPrice)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
                {
                    connection.Open();

                    // 현재 SELLER의 TOTAL 조회
                    string selectQuery = "SELECT TOTAL FROM SELLER WHERE SELLER_ID = :sellerId";

                    using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                    {
                        selectCmd.Parameters.Add(":sellerId", OracleDbType.Varchar2).Value = loggedInUserId;

                        int currentTotal = Convert.ToInt32(selectCmd.ExecuteScalar());

                        // SELLER의 TOTAL 업데이트
                        string updateQuery = "UPDATE SELLER SET TOTAL = :newTotal WHERE SELLER_ID = :sellerId";
                        using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                        {

                            int newTotal = currentTotal + productPrice;

                            updateCmd.Parameters.Add(":newTotal", OracleDbType.Int32).Value = newTotal;
                            updateCmd.Parameters.Add(":sellerId", OracleDbType.Varchar2).Value = loggedInUserId;

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
                Console.WriteLine("스택 추적: " + ex.StackTrace);
            }
            
        }

        private void UpdateUserTotal(string userId, int productId)
        {
            // 가정: dataSet과 tableAdapters는 이미 설정되어 있으며, 필요한 테이블에 접근할 수 있습니다.
            // 예를 들어, dataSet이 MyDataSet이라고 하고, 사용자와 제품에 대한 TableAdapters가 각각 UserTableAdapter와 ProductTableAdapter라고 가정합니다.

            using (var productAdapter = new DataSet1TableAdapters.PRODUCTTableAdapter())
            {
                // 제품 가격을 가져옵니다.
                var productTable = productAdapter.GetDataById(productId); // ID로 제품을 조회합니다.
                if (productTable.Rows.Count == 0)
                    throw new Exception("Product not found.");

                int productPrice = Convert.ToInt32(productTable[0]["PRICE"]);  

                using (var userAdapter = new DataSet1TableAdapters.USERSTableAdapter())
                {
                    // 사용자의 TOTAL을 업데이트합니다.
                    var userTable = userAdapter.GetDataById(userId); // ID로 사용자를 조회합니다.
                    if (userTable.Rows.Count == 0)
                        throw new Exception("User not found.");

                    userTable[0]["TOTAL"] = Convert.ToInt32(userTable[0]["TOTAL"]) + productPrice;
                    userAdapter.Update(userTable); // 변경된 데이터를 데이터베이스에 반영합니다.
                }
            }
        }


        private void IncreaseProductRecount(int productId)
        {
            //환불 횟수 증가
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 RECOUNT 조회 쿼리
                string selectQuery = "SELECT RECOUNT FROM PRODUCT WHERE P_ID = :productId";

                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                    // 현재 RECOUNT 값을 가져옴
                    int currentRecount = Convert.ToInt32(selectCmd.ExecuteScalar());

                    // RECOUNT 증가 쿼리
                    string updateQuery = "UPDATE PRODUCT SET RECOUNT = :newRecount WHERE P_ID = :productId";

                    using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                    {
                        // RECOUNT를 1 증가시킴
                        int newRecount = currentRecount + 1;

                        // 매개변수 추가
                        updateCmd.Parameters.Add(":newRecount", OracleDbType.Int32).Value = newRecount;
                        updateCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                        // 쿼리 실행
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void IncreaseUserRecount(string userId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 USER의 RECOUNT 조회
                string selectQuery = "SELECT RECOUNT FROM USERS WHERE ID = :userId";
                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                    object result = selectCmd.ExecuteScalar();
                    int currentRecount = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    // USER의 RECOUNT 증가
                    string updateQuery = "UPDATE USERS SET RECOUNT = :newRecount WHERE ID = :userId";
                    using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                    {
                        int newRecount = currentRecount + 1;

                        updateCmd.Parameters.Add(":newRecount", OracleDbType.Int32).Value = newRecount;
                        updateCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void IncreaseUserCount(string userId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 COUNT 조회 쿼리
                string selectQuery = "SELECT COUNT FROM USERS WHERE ID = :userId";

                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                    // 현재 COUNT 값을 가져옴
                    int currentCount = Convert.ToInt32(selectCmd.ExecuteScalar());

                    // COUNT 증가 쿼리
                    string updateQuery = "UPDATE USERS SET COUNT = :newCount WHERE ID = :userId";

                    using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                    {
                        // COUNT를 1 증가시킴
                        int newCount = currentCount + 1;

                        // 매개변수 추가
                        updateCmd.Parameters.Add(":newCount", OracleDbType.Int32).Value = newCount;
                        updateCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                        // 쿼리 실행
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void DecreaseInventory(int productId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 재고량 조회 쿼리
                string selectQuery = "SELECT INVENTORY FROM PRODUCT WHERE P_ID = :productId";

                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                    // 현재 재고량을 가져옴
                    int currentInventory = Convert.ToInt32(selectCmd.ExecuteScalar());

                    // 재고량이 0 이상이면 감소시킴
                    if (currentInventory > 0)
                    {
                        // 재고량 감소 쿼리
                        string updateQuery = "UPDATE PRODUCT SET INVENTORY = :newInventory, COUNT = COUNT + 1 WHERE P_ID = :productId";

                        using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                        {
                            // 재고량 감소 후의 값
                            int newInventory = currentInventory - 1;

                            // 매개변수 추가
                            updateCmd.Parameters.Add(":newInventory", OracleDbType.Int32).Value = newInventory;
                            updateCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                            // 쿼리 실행
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("재고가 부족합니다.");
                    }
                }
            }
        }

        private void IncreaseInventory(int productId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 재고량 복원 쿼리
                string updateQuery = "UPDATE PRODUCT SET INVENTORY = INVENTORY + 1 WHERE P_ID = :productId";

                using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                {
                    // 매개변수 추가
                    updateCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                    // 쿼리 실행
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateOrderDetailStatus(int orderDetailId, string status)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 쿼리문 작성
                string updateQuery = "UPDATE ORDER_DETAILS SET BOOL = :status WHERE ORDER_DETAIL_ID = :orderDetailId";

                using (OracleCommand cmd = new OracleCommand(updateQuery, connection))
                {
                    // 매개변수 추가
                    cmd.Parameters.Add(":status", OracleDbType.Varchar2).Value = status;
                    cmd.Parameters.Add(":orderDetailId", OracleDbType.Int32).Value = orderDetailId;

                    // 쿼리 실행
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //환불확정버튼
            try
            {
                // 선택된 행의 정보 확인
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    // 선택된 행의 정보를 사용하여 환불 확정 로직
                    int orderDetailId = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn1"].Value);

                    // 환불 확정 상태 업데이트
                    UpdateOrderDetailStatus(orderDetailId, "환불확정");

                    // 상품 재고량 복원 (BOOL 값이 "확정"인 경우에만 수행)
                    string status = row.Cells["dataGridViewTextBoxColumn6"].Value.ToString();
                    if (status == "확정")
                    {

                        int productId = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn3"].Value);
                        String userid = Convert.ToString(row.Cells["dataGridViewTextBoxColumn5"].Value);
                        int productPrice = GetProductPrice(productId);
                        IncreaseInventory(productId);
                        DecreaseCount(productId);
                        DecreaseUserCount(userid);
                        DecreaseUserTotal(userid, productId);
                        IncreaseProductRecount(productId);
                        // 판매자의 TOTAL 감소
                        DecreaseSellerTotal(productId, productPrice);
                        IncreaseUserRecount(userid);
                    }
                }

                // 새로고침
                button4_Click(sender, e);
                
                MessageBox.Show("환불확정이 완료되었습니다.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 새로고침 버튼
            // 데이터를 다시 로드
            this.oRDER_DETAILSTableAdapter.Fill(this.dataSet1.ORDER_DETAILS, loggedInUserId);

            // DataGridView 업데이트
            dataGridView2.DataSource = oRDERDETAILSBindingSource;
            dataGridView2.Refresh();
            DisplaySellerTotal(loggedInUserId);
        }

        private void DecreaseSellerTotal(int productId, int productPrice)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                       "PASSWORD=s5532960;" +
                       "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 SELLER의 TOTAL 조회
                string selectQuery = "SELECT TOTAL FROM SELLER WHERE SELLER_ID = :sellerId";
                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":sellerId", OracleDbType.Varchar2).Value = loggedInUserId;

                    int currentTotal = Convert.ToInt32(selectCmd.ExecuteScalar());

                    // SELLER의 TOTAL 업데이트
                    string updateQuery = "UPDATE SELLER SET TOTAL = :newTotal WHERE SELLER_ID = :sellerId";
                    using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                    {
                        int newTotal = currentTotal - productPrice;

                        updateCmd.Parameters.Add(":newTotal", OracleDbType.Int32).Value = newTotal;
                        updateCmd.Parameters.Add(":sellerId", OracleDbType.Varchar2).Value = loggedInUserId;

                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void DecreaseCount(int productId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // COUNT 감소 쿼리
                string updateQuery = "UPDATE PRODUCT SET COUNT = COUNT - 1 WHERE P_ID = :productId";

                using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                {
                    // 매개변수 추가
                    updateCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                    // 쿼리 실행
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        
        private void DecreaseUserTotal(string userId, int productId)
        {
            using (var productAdapter = new DataSet1TableAdapters.PRODUCTTableAdapter())
            {
                // 제품 가격을 가져옵니다.
                var productTable = productAdapter.GetDataById(productId); // ID로 제품을 조회합니다.
                if (productTable.Rows.Count == 0)
                    throw new Exception("Product not found.");

                int productPrice = Convert.ToInt32(productTable[0]["PRICE"]);

                using (var userAdapter = new DataSet1TableAdapters.USERSTableAdapter())
                {
                    // 사용자의 TOTAL을 갱신합니다.
                    var userTable = userAdapter.GetDataById(userId); // ID로 사용자를 조회합니다.
                    if (userTable.Rows.Count == 0)
                        throw new Exception("User not found.");

                    // TOTAL에서 제품 가격을 차감합니다.
                    userTable[0]["TOTAL"] = Convert.ToInt32(userTable[0]["TOTAL"]) - productPrice;
                    userAdapter.Update(userTable); // 변경된 데이터를 데이터베이스에 반영합니다.
                }
            }
        }
        private void DisplaySellerTotal(string sellerId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // SELLER 테이블에서 특정 SELLER_ID에 해당하는 TOTAL 값 조회
                string query = "SELECT TOTAL FROM SELLER WHERE SELLER_ID = :sellerId";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(":sellerId", OracleDbType.Varchar2).Value = sellerId;

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // 조회된 TOTAL 값을 TextBox에 표시
                        textBox1.Text = result.ToString();
                        textBox2.Text = result.ToString();
                    }
                    else
                    {
                        textBox1.Text = "0"; // 값이 없는 경우 0으로 표시
                        textBox2.Text = "0"; // 값이 없는 경우 0으로 표시
                    }
                }
            }
        }


        private void DecreaseUserCount(string userId)
        {
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                // 현재 COUNT 조회 쿼리
                string selectQuery = "SELECT COUNT FROM USERS WHERE ID = :userId";

                using (OracleCommand selectCmd = new OracleCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                    // 현재 COUNT 값을 가져옴
                    int currentCount = Convert.ToInt32(selectCmd.ExecuteScalar());

                    // COUNT 감소 쿼리
                    string updateQuery = "UPDATE USERS SET COUNT = :newCount WHERE ID = :userId";

                    using (OracleCommand updateCmd = new OracleCommand(updateQuery, connection))
                    {
                        // COUNT를 1 감소시킴
                        int newCount = Math.Max(0, currentCount - 1);

                        // 매개변수 추가
                        updateCmd.Parameters.Add(":newCount", OracleDbType.Int32).Value = newCount;
                        updateCmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = userId;

                        // 쿼리 실행
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
