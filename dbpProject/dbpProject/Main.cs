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
    public partial class Main : Form
    {
        private string loggedInUserId;  // 전역 변수로 로그인한 사용자의 아이디 저장

        public Main(string userId)
        {
            InitializeComponent();
            loggedInUserId = userId;  // 생성자에서 전달받은 아이디를 저장
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet1.PRODUCT' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            //this.pRODUCTTableAdapter.Fill(this.dataSet1.PRODUCT);
            // ... 이전 코드 ...

            // DataGridView의 열과 행 크기를 자동으로 조절
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            
            // DataGridView를 읽기 전용으로 설정하여 사용자가 데이터를 직접 추가할 수 없게 함
            dataGridView1.ReadOnly = true;

            // DataGridView의 CellClick 이벤트에 이벤트 핸들러를 추가
            dataGridView1.CellClick += dataGridView1_CellClick;

            producT_TYPETableAdapter1.Fill(dataSet1.PRODUCT_TYPE);

            DataTable mytable = dataSet1.Tables["PRODUCT_TYPE"];
            foreach (DataRow mydataRow in mytable.Rows)
            {
                listBox1.Items.Add(mydataRow["TYPE_NAME"].ToString());
            }

            noticeTableAdapter1.Fill(dataSet1.NOTICE);

            DataTable mytable2 = dataSet1.Tables["NOTICE"];
            foreach (DataRow mydataRow in mytable2.Rows)
            {
                listBox2.Items.Add(mydataRow["N_KEYWORD"].ToString());
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 셀을 클릭하면 해당 행을 선택
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 새로고침 버튼
            // 데이터를 다시 로드

            string selectedTypeName = listBox1.SelectedItem.ToString();

            this.pRODUCTTableAdapter.FillByType(this.dataSet1.PRODUCT, selectedTypeName);

            // DataGridView 업데이트
            dataGridView1.DataSource = pRODUCTBindingSource;
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 장바구니 담기 버튼
            try
            {
                if (loggedInUserId != null)
                {
                    // 선택된 행의 정보 확인
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        // 선택된 행의 정보를 사용하여 장바구니에 추가하는 로직
                        long productId = Convert.ToInt32(row.Cells["pIDDataGridViewTextBoxColumn"].Value);
                        String sellerId = Convert.ToString(row.Cells["sELLERIDDataGridViewTextBoxColumn"].Value);
                        // "pRICEDataGridViewTextBoxColumn" 열의 셀을 가져와서 가격을 추출
                        object priceCellValue = row.Cells["pRICEDataGridViewTextBoxColumn"].Value;

                        // 가격이 null이 아니라면 형변환하여 사용
                        if (priceCellValue != null)
                        {
                            
                            long price = Convert.ToInt32(row.Cells["pRICEDataGridViewTextBoxColumn"].Value);

                            // 장바구니 데이터셋에서 장바구니 테이블에 새로운 행 추가
                            DataSet1.CARTRow newRow = dataSet1.CART.NewCARTRow();
                            newRow.USER_ID = loggedInUserId; // 현재 로그인한 사용자의 ID로 설정
                            newRow.PRODUCT_ID = productId;
                            newRow.PRICE = price.ToString();
                            newRow.QUANTITY = 1;
                            newRow.SELLER_ID = sellerId;

                            dataSet1.CART.AddCARTRow(newRow);
                        }
                        else
                        {
                            MessageBox.Show("가격이 null입니다.");
                        }
                    }

                    // 변경 내용을 데이터베이스에 반영
                    this.cartTableAdapter1.Update(this.dataSet1.CART);

                    MessageBox.Show("장바구니에 제품이 추가되었습니다.");
                }
                else
                {
                    MessageBox.Show("로그인이 필요합니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 마이페이지로 이동
            MyPage myPageForm = new MyPage(loggedInUserId);
            myPageForm.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 검색 버튼
            // 선택된 상품 종류 가져오기
            string selectedTypeName = listBox1.SelectedItem.ToString();

            this.pRODUCTTableAdapter.FillByType(this.dataSet1.PRODUCT,selectedTypeName);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            // 후기보기 버튼
            try
            {
                if (loggedInUserId != null)
                {
                    // 선택된 행의 정보 확인
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        // 선택된 행의 정보를 사용하여 장바구니에 추가하는 로직
                        int productId = Convert.ToInt32(row.Cells["pIDDataGridViewTextBoxColumn"].Value);

                        ReviewForm reviewForm = new ReviewForm(productId);
                        reviewForm.Show();
                       
                    }

                    
                }
                else
                {
                    MessageBox.Show("로그인이 필요합니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = listBox2.IndexFromPoint(e.Location);

            if (selectedIndex != ListBox.NoMatches)
            {
                // 선택한 항목의 텍스트 가져오기
                string selectedKeyword = listBox2.Items[selectedIndex].ToString();

                // 선택한 키워드에 해당하는 내용 가져오기
                string selectedContents = GetContentsByKeyword(selectedKeyword);

                // 선택한 키워드에 해당하는 날짜 가져오기
                string selectedDate = GetDateByKeyword(selectedKeyword);

                // RichTextBox에 텍스트 출력
                richTextBox1.Text = $"날짜: {selectedDate}\n\n{selectedContents}";
            }
        }

        private string GetDateByKeyword(string keyword)
        {
            // keyword에 해당하는 날짜를 조회하는 쿼리 또는 메서드를 호출
            // DataSet1.NOTICE 테이블에서 keyword에 해당하는 날짜를 찾아서 반환
            // 예제에서는 가상의 메서드로 대체
            string date = RetrieveDateFromDataSet(keyword);
            return date;
        }

        private string RetrieveDateFromDataSet(string keyword)
        {
            // DataSet1.NOTICE 테이블에서 keyword에 해당하는 날짜 조회
            string date = string.Empty;

            DataTable noticeTable = dataSet1.Tables["NOTICE"];
            foreach (DataRow row in noticeTable.Rows)
            {
                if (row["N_KEYWORD"].ToString() == keyword)
                {
                    date = row["N_DATE"].ToString();
                    break;
                }
            }

            return date;
        }
        private string GetContentsByKeyword(string keyword)
        {
            // keyword에 해당하는 내용을 조회하는 쿼리 또는 메서드를 호출
            // DataSet1.NOTICE 테이블에서 keyword에 해당하는 내용을 찾아서 반환
            // 예제에서는 가상의 메서드로 대체
            string contents = RetrieveContentsFromDataSet(keyword);
            return contents;
        }

        private string RetrieveContentsFromDataSet(string keyword)
        {
            // DataSet1.NOTICE 테이블에서 keyword에 해당하는 내용 조회
            string contents = string.Empty;

            DataTable noticeTable = dataSet1.Tables["NOTICE"];
            foreach (DataRow row in noticeTable.Rows)
            {
                if (row["N_KEYWORD"].ToString() == keyword)
                {
                    contents = row["N_CONTENTS"].ToString();
                    break;
                }
            }

            return contents;
        }

        
    }
}
