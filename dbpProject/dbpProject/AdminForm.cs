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
    public partial class AdminForm : Form
    {
       
        public AdminForm()
        {
            InitializeComponent();
            
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 1000; // 충분히 큰 값으로 설정

            // TODO: 이 코드는 데이터를 'dataSet11.USERS' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.uSERSTableAdapter.FillByUser(this.dataSet11.USERS);
            // TODO: 이 코드는 데이터를 'dataSet1.PRODUCT' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.pRODUCTTableAdapter.Fill(this.dataSet1.PRODUCT);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 차트에 'Sales'라는 이름의 시리즈 추가
            chart1.Series.Add("Sales");
            // Oracle 연결 문자열 설정
            string connectionString = "USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1";

            // Oracle 연결 생성
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                // Oracle 명령문 생성
                string query = "SELECT P_NAME, COUNT FROM S5532960.PRODUCT ORDER BY COUNT DESC FETCH FIRST 5 ROWS ONLY";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    // 연결 열기
                    connection.Open();

                    // 데이터 가져오기
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        // 차트에 데이터 추가
                        while (reader.Read())
                        {
                            chart1.Series["Sales"].Points.AddXY(reader["P_NAME"].ToString(), reader["COUNT"]);
                        }
                    }
                }
            }

            // 차트 설정
            chart1.ChartAreas[0].AxisX.Title = "상품명";
            chart1.ChartAreas[0].AxisY.Title = "판매량";
            chart1.Titles.Add("Top 5 판매 상품");
            chart1.Series["Sales"].IsVisibleInLegend = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // 공지 저장하기 버튼
            string keyword = textBox1.Text;
            string contents = richTextBox1.Text;

            // 현재 날짜 및 시간 가져오기
            DateTime currentDate = DateTime.Now;

            // 시퀀스로부터 다음 ID 값을 가져오기
            int nextId = GetNextSequenceValue(); // 사용자 정의 함수로 구현 필요

            // DataSet에 새로운 행 추가
            DataRow newRow = dataSet1.Tables["notice"].NewRow();
            newRow["id"] = nextId; // 시퀀스에서 가져온 값 사용
            newRow["n_keyword"] = keyword;
            newRow["n_contents"] = contents;
            newRow["n_date"] = currentDate;

            // DataSet에 새로운 행 추가
            dataSet1.Tables["notice"].Rows.Add(newRow);

            // 변경 내용 데이터베이스에 반영
            noticeTableAdapter1.Update(dataSet1.NOTICE);
            textBox1.Clear();
            richTextBox1.Clear();
            MessageBox.Show("공지가 등록되었습니다");
        }
        private int GetNextSequenceValue()
        {
            int nextId;

            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                string sequenceQuery = "SELECT NOTICE_SEQ.NEXTVAL FROM DUAL"; // YOUR_SEQUENCE에 실제 시퀀스 이름 사용

                using (OracleCommand sequenceCmd = new OracleCommand(sequenceQuery, connection))
                {
                    nextId = Convert.ToInt32(sequenceCmd.ExecuteScalar());
                }
            }

            return nextId;
        }

        private void FillProductData()
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                string query = "SELECT ProductId, ProductName, COUNT(*) AS SaleCount FROM Sales GROUP BY ProductId, ProductName";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    // 이미 존재하는 DataSet의 PRODUCT 테이블에 채우기
                    adapter.Fill(dataSet1, "PRODUCT");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 상품 편집
            ProductChange productChange = new ProductChange();
            productChange.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SalesForm salesForm = new SalesForm();
            salesForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewPRODUCT newPRODUCT = new NewPRODUCT();
            newPRODUCT.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //우수고객
            LoadDataIntoDataGridView(queryExcellentCustomers);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //불량고객
            LoadDataIntoDataGridView(queryPoorCustomers);
        }
        private void LoadDataIntoDataGridView(string query)
        {
            using (OracleConnection connection = new OracleConnection("USER ID = S5532960; " +
                        "PASSWORD=s5532960;" +
                        "Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }
        string queryExcellentCustomers = @"
    SELECT ID, NAME, regisdate, count, total
    FROM USERS
    WHERE COUNT > 10 AND TOTAL > 5000000";

        string queryPoorCustomers = @"
    SELECT ID, NAME, regisdate, count, total
    FROM USERS
    WHERE COUNT * 2 < RECOUNT ";
        string Customers = @"
    SELECT ID, NAME, regisdate, count, total
    FROM USERS";

        private void button9_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView(Customers);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 저장
            this.uSERSTableAdapter.Update(this.dataSet11.USERS);
            MessageBox.Show("저장되었습니다");
        }
    }
}
