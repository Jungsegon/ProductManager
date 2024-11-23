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
    public partial class ProductChange : Form
    {
        public ProductChange()
        {
            InitializeComponent();
        }

        private void ProductChange_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet1.PRODUCT' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.pRODUCTTableAdapter.Fill(this.dataSet1.PRODUCT);

            producT_TYPETableAdapter1.Fill(dataSet1.PRODUCT_TYPE);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable mytable = dataSet1.Tables["PRODUCT_TYPE"];
            foreach (DataRow mydataRow in mytable.Rows)
            {
                listBox1.Items.Add(mydataRow["TYPE_NAME"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 검색 버튼
            // 선택된 상품 종류 가져오기
            string selectedTypeName = listBox1.SelectedItem.ToString();

            this.pRODUCTTableAdapter.FillByType(this.dataSet1.PRODUCT, selectedTypeName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //변경된 DataGridView db에 업데이트
            this.pRODUCTTableAdapter.Update(dataSet1.PRODUCT);
            MessageBox.Show("저장이 완료되었습니다.");
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            // 필터링 버튼
            // 선택된 상품 종류 가져오기
            string selectedTypeName = listBox1.SelectedItem.ToString();

            // 추가: INVENTORY가 0 이상인 상품만 필터링
            this.pRODUCTTableAdapter.FillByTypeAndInventory(this.dataSet1.PRODUCT, selectedTypeName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 필터링 버튼
            // 선택된 상품 종류 가져오기
            string selectedTypeName = listBox1.SelectedItem.ToString();

            // 추가: INVENTORY가 0인 상품만 필터링
            this.pRODUCTTableAdapter.FillByTypeAndInventory0(this.dataSet1.PRODUCT, selectedTypeName);
        }
    }
}
