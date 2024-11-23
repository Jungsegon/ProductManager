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
using System.Windows.Forms.DataVisualization.Charting;

namespace dbpProject
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            DataTable refundData = GetTop5RefundedProducts();

            // 차트에 데이터 바인딩
            chart2.DataSource = refundData;
            chart2.Series["Series1"].XValueMember = "ProductName";
            chart2.Series["Series1"].YValueMembers = "Recount";
            chart2.DataBind();

            // 차트 설정
            chart2.ChartAreas[0].AxisX.Title = "상품명";
            chart2.ChartAreas[0].AxisY.Title = "환불량";
            chart2.Titles.Add("Top 5 환불 상품");

            chart1.ChartAreas[0].AxisY.Title = "판매액";
            chart1.Titles.Add("기간 별 Top 5 판매액 상품");

            chart3.DataSource = GetTop5InventoryProducts();
            chart3.Series["Series1"].XValueMember = "ProductName";
            chart3.Series["Series1"].YValueMembers = "inventory";
            chart3.DataBind();
            // 차트 설정
            chart3.ChartAreas[0].AxisX.Title = "상품명";
            chart3.ChartAreas[0].AxisY.Title = "재고량";
            chart3.Titles.Add("Top 5 재고량 상품");

            chart1.Series["Series1"].IsVisibleInLegend = false;
            chart2.Series["Series1"].IsVisibleInLegend = false;
            chart3.Series["Series1"].IsVisibleInLegend = false;

        }
        private DataTable GetTop5InventoryProducts()
        {
            DataTable dataTable = new DataTable();
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = @"
            SELECT P_NAME AS ProductName, INVENTORY
            FROM PRODUCT
            ORDER BY INVENTORY DESC
            FETCH FIRST 5 ROWS ONLY";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }


        private DataTable GetTop5RefundedProducts()
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();

                string query = @"
            SELECT P_NAME AS ProductName, RECOUNT
            FROM PRODUCT
            ORDER BY RECOUNT DESC
            FETCH FIRST 5 ROWS ONLY";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        

        
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            // 판매 데이터와 제품 가격을 가져옴
            DataTable salesData = GetSalesDataWithoutPrice(startDate, endDate);
            Dictionary<int, decimal> productPrices = GetProductPrices();

            // 판매액 계산
            foreach (DataRow row in salesData.Rows)
            {
                int productId = Convert.ToInt32(row["PRODUCT_ID"]);
                int quantity = Convert.ToInt32(row["QUANTITY"]);

                if (productPrices.ContainsKey(productId))
                {
                    decimal price = productPrices[productId];
                    decimal totalSale = price * quantity;
                    row["TotalSales"] = totalSale;
                }
                else
                {
                    row["TotalSales"] = 0; // 가격 정보가 없는 경우
                }
            }

            // 차트에 데이터 바인딩
            chart1.DataSource = salesData;
            chart1.Series["Series1"].XValueMember = "ProductName";
            chart1.Series["Series1"].YValueMembers = "TotalSales";
            chart1.DataBind();
            
        }

        private Dictionary<int, decimal> GetProductPrices()
        {
            Dictionary<int, decimal> prices = new Dictionary<int, decimal>();
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = "SELECT P_ID, PRICE FROM PRODUCT";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = reader.GetInt32(0);
                            decimal price = reader.GetDecimal(1); // PRICE 열을 decimal로 직접 읽음
                            prices[productId] = price;
                        }
                    }
                }
            }
            return prices;
        }

        private DataTable GetSalesDataWithoutPrice(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();
            using (OracleConnection connection = new OracleConnection("USER ID=S5532960; PASSWORD=s5532960; Data source=localhost:1521/xepdb1"))
            {
                connection.Open();
                string query = @"
            SELECT D.PRODUCT_ID, P.P_NAME AS ProductName, SUM(D.QUANTITY) AS QUANTITY
            FROM ORDER_DETAILS D
            JOIN PRODUCT P ON D.PRODUCT_ID = P.P_ID
            WHERE D.BOOL_DATE BETWEEN :startDate AND :endDate AND D.BOOL = '확정'
            GROUP BY D.PRODUCT_ID, P.P_NAME
            ORDER BY QUANTITY DESC
            FETCH FIRST 5 ROWS ONLY";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(":startDate", OracleDbType.Date).Value = startDate;
                    command.Parameters.Add(":endDate", OracleDbType.Date).Value = endDate;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            dataTable.Columns.Add("TotalSales", typeof(decimal));
            return dataTable;
        }


    }

}
