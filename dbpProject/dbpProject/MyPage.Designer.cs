
namespace dbpProject
{
    partial class MyPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.uSERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRODUCTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRICEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qUANTITYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sELLERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cARTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet11 = new dbpProject.DataSet1();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.oRDERDETAILIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oRDERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRODUCTIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qUANTITYDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bOOLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sELLERIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oRDERDETAILSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.내용작성 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataSet1 = new dbpProject.DataSet1();
            this.productTableAdapter1 = new dbpProject.DataSet1TableAdapters.PRODUCTTableAdapter();
            this.cartTableAdapter = new dbpProject.DataSet1TableAdapters.CARTTableAdapter();
            this.orderlistTableAdapter1 = new dbpProject.DataSet1TableAdapters.ORDERLISTTableAdapter();
            this.ordeR_DETAILSTableAdapter1 = new dbpProject.DataSet1TableAdapters.ORDER_DETAILSTableAdapter();
            this.oracleConnection1 = new Oracle.ManagedDataAccess.Client.OracleConnection();
            this.memO_TABLETableAdapter1 = new dbpProject.DataSet1TableAdapters.MEMO_TABLETableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oRDERDETAILSBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(990, 453);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Linen;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(982, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "장바구니";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11F);
            this.label3.Location = new System.Drawing.Point(101, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "장바구니 목록";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uSERIDDataGridViewTextBoxColumn,
            this.pRODUCTIDDataGridViewTextBoxColumn,
            this.pRICEDataGridViewTextBoxColumn,
            this.qUANTITYDataGridViewTextBoxColumn,
            this.sELLERIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.cARTBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(75, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(720, 374);
            this.dataGridView1.TabIndex = 2;
            // 
            // uSERIDDataGridViewTextBoxColumn
            // 
            this.uSERIDDataGridViewTextBoxColumn.DataPropertyName = "USER_ID";
            this.uSERIDDataGridViewTextBoxColumn.HeaderText = "유저ID";
            this.uSERIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.uSERIDDataGridViewTextBoxColumn.Name = "uSERIDDataGridViewTextBoxColumn";
            this.uSERIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // pRODUCTIDDataGridViewTextBoxColumn
            // 
            this.pRODUCTIDDataGridViewTextBoxColumn.DataPropertyName = "PRODUCT_ID";
            this.pRODUCTIDDataGridViewTextBoxColumn.HeaderText = "제품번호";
            this.pRODUCTIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pRODUCTIDDataGridViewTextBoxColumn.Name = "pRODUCTIDDataGridViewTextBoxColumn";
            this.pRODUCTIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // pRICEDataGridViewTextBoxColumn
            // 
            this.pRICEDataGridViewTextBoxColumn.DataPropertyName = "PRICE";
            this.pRICEDataGridViewTextBoxColumn.HeaderText = "가격";
            this.pRICEDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pRICEDataGridViewTextBoxColumn.Name = "pRICEDataGridViewTextBoxColumn";
            this.pRICEDataGridViewTextBoxColumn.Width = 125;
            // 
            // qUANTITYDataGridViewTextBoxColumn
            // 
            this.qUANTITYDataGridViewTextBoxColumn.DataPropertyName = "QUANTITY";
            this.qUANTITYDataGridViewTextBoxColumn.HeaderText = "수량";
            this.qUANTITYDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.qUANTITYDataGridViewTextBoxColumn.Name = "qUANTITYDataGridViewTextBoxColumn";
            this.qUANTITYDataGridViewTextBoxColumn.Width = 125;
            // 
            // sELLERIDDataGridViewTextBoxColumn
            // 
            this.sELLERIDDataGridViewTextBoxColumn.DataPropertyName = "SELLER_ID";
            this.sELLERIDDataGridViewTextBoxColumn.HeaderText = "판매자ID";
            this.sELLERIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.sELLERIDDataGridViewTextBoxColumn.Name = "sELLERIDDataGridViewTextBoxColumn";
            this.sELLERIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // cARTBindingSource
            // 
            this.cARTBindingSource.DataMember = "CART";
            this.cARTBindingSource.DataSource = this.dataSet11;
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.button1.Location = new System.Drawing.Point(824, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 59);
            this.button1.TabIndex = 1;
            this.button1.Text = "구매하기";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Linen;
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(982, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "구매목록";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Bisque;
            this.button3.Location = new System.Drawing.Point(28, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 37);
            this.button3.TabIndex = 2;
            this.button3.Text = "환불하기";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(857, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "새로고침";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oRDERDETAILIDDataGridViewTextBoxColumn,
            this.oRDERIDDataGridViewTextBoxColumn,
            this.pRODUCTIDDataGridViewTextBoxColumn1,
            this.qUANTITYDataGridViewTextBoxColumn1,
            this.uSERIDDataGridViewTextBoxColumn1,
            this.bOOLDataGridViewTextBoxColumn,
            this.sELLERIDDataGridViewTextBoxColumn1});
            this.dataGridView2.DataSource = this.oRDERDETAILSBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(28, 57);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 27;
            this.dataGridView2.Size = new System.Drawing.Size(926, 356);
            this.dataGridView2.TabIndex = 0;
            // 
            // oRDERDETAILIDDataGridViewTextBoxColumn
            // 
            this.oRDERDETAILIDDataGridViewTextBoxColumn.DataPropertyName = "ORDER_DETAIL_ID";
            this.oRDERDETAILIDDataGridViewTextBoxColumn.HeaderText = "주문상세번호";
            this.oRDERDETAILIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oRDERDETAILIDDataGridViewTextBoxColumn.Name = "oRDERDETAILIDDataGridViewTextBoxColumn";
            this.oRDERDETAILIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // oRDERIDDataGridViewTextBoxColumn
            // 
            this.oRDERIDDataGridViewTextBoxColumn.DataPropertyName = "ORDER_ID";
            this.oRDERIDDataGridViewTextBoxColumn.HeaderText = "주문번호";
            this.oRDERIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oRDERIDDataGridViewTextBoxColumn.Name = "oRDERIDDataGridViewTextBoxColumn";
            this.oRDERIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // pRODUCTIDDataGridViewTextBoxColumn1
            // 
            this.pRODUCTIDDataGridViewTextBoxColumn1.DataPropertyName = "PRODUCT_ID";
            this.pRODUCTIDDataGridViewTextBoxColumn1.HeaderText = "제품번호";
            this.pRODUCTIDDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.pRODUCTIDDataGridViewTextBoxColumn1.Name = "pRODUCTIDDataGridViewTextBoxColumn1";
            this.pRODUCTIDDataGridViewTextBoxColumn1.Width = 125;
            // 
            // qUANTITYDataGridViewTextBoxColumn1
            // 
            this.qUANTITYDataGridViewTextBoxColumn1.DataPropertyName = "QUANTITY";
            this.qUANTITYDataGridViewTextBoxColumn1.HeaderText = "수량";
            this.qUANTITYDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.qUANTITYDataGridViewTextBoxColumn1.Name = "qUANTITYDataGridViewTextBoxColumn1";
            this.qUANTITYDataGridViewTextBoxColumn1.Width = 125;
            // 
            // uSERIDDataGridViewTextBoxColumn1
            // 
            this.uSERIDDataGridViewTextBoxColumn1.DataPropertyName = "USER_ID";
            this.uSERIDDataGridViewTextBoxColumn1.HeaderText = "유저ID";
            this.uSERIDDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.uSERIDDataGridViewTextBoxColumn1.Name = "uSERIDDataGridViewTextBoxColumn1";
            this.uSERIDDataGridViewTextBoxColumn1.Width = 125;
            // 
            // bOOLDataGridViewTextBoxColumn
            // 
            this.bOOLDataGridViewTextBoxColumn.DataPropertyName = "BOOL";
            this.bOOLDataGridViewTextBoxColumn.HeaderText = "구매확정";
            this.bOOLDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.bOOLDataGridViewTextBoxColumn.Name = "bOOLDataGridViewTextBoxColumn";
            this.bOOLDataGridViewTextBoxColumn.Width = 125;
            // 
            // sELLERIDDataGridViewTextBoxColumn1
            // 
            this.sELLERIDDataGridViewTextBoxColumn1.DataPropertyName = "SELLER_ID";
            this.sELLERIDDataGridViewTextBoxColumn1.HeaderText = "판매자ID";
            this.sELLERIDDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.sELLERIDDataGridViewTextBoxColumn1.Name = "sELLERIDDataGridViewTextBoxColumn1";
            this.sELLERIDDataGridViewTextBoxColumn1.Width = 125;
            // 
            // oRDERDETAILSBindingSource
            // 
            this.oRDERDETAILSBindingSource.DataMember = "ORDER_DETAILS";
            this.oRDERDETAILSBindingSource.DataSource = this.dataSet11;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Linen;
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.내용작성);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(982, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "후기작성";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("굴림", 10F);
            this.label2.Location = new System.Drawing.Point(25, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "구매목록을 선택 후 저장";
            // 
            // 내용작성
            // 
            this.내용작성.AutoSize = true;
            this.내용작성.Location = new System.Drawing.Point(183, 91);
            this.내용작성.Name = "내용작성";
            this.내용작성.Size = new System.Drawing.Size(67, 15);
            this.내용작성.TabIndex = 9;
            this.내용작성.Text = "내용작성";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(677, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "후기제목";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(747, 85);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 25);
            this.textBox1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Bisque;
            this.button4.Location = new System.Drawing.Point(866, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 49);
            this.button4.TabIndex = 5;
            this.button4.Text = "후기저장";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(177, 117);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(778, 275);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(26, 57);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(125, 334);
            this.listBox1.TabIndex = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productTableAdapter1
            // 
            this.productTableAdapter1.ClearBeforeFill = true;
            // 
            // cartTableAdapter
            // 
            this.cartTableAdapter.ClearBeforeFill = true;
            // 
            // orderlistTableAdapter1
            // 
            this.orderlistTableAdapter1.ClearBeforeFill = true;
            // 
            // ordeR_DETAILSTableAdapter1
            // 
            this.ordeR_DETAILSTableAdapter1.ClearBeforeFill = true;
            // 
            // oracleConnection1
            // 
            this.oracleConnection1.ChunkMigrationConnectionTimeout = "120";
            // 
            // memO_TABLETableAdapter1
            // 
            this.memO_TABLETableAdapter1.ClearBeforeFill = true;
            // 
            // MyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(991, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MyPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MyPage_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oRDERDETAILSBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.PRODUCTTableAdapter productTableAdapter1;
        private DataSet1TableAdapters.CARTTableAdapter cartTableAdapter;
        private DataSet1TableAdapters.ORDERLISTTableAdapter orderlistTableAdapter1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet1 dataSet11;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource cARTBindingSource;
        private System.Windows.Forms.BindingSource oRDERDETAILSBindingSource;
        private DataSet1TableAdapters.ORDER_DETAILSTableAdapter ordeR_DETAILSTableAdapter1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRODUCTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRICEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qUANTITYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sELLERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERDETAILIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRODUCTIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn qUANTITYDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bOOLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sELLERIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label 내용작성;
        private DataSet1TableAdapters.MEMO_TABLETableAdapter memO_TABLETableAdapter1;
        private System.Windows.Forms.Button button3;
    }
}