using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using Oracle.ManagedDataAccess.Client;


namespace dbpProject
{
    public partial class LoginForm : Form
    {
        OracleConnection con = new OracleConnection("USER ID = S5532960; " +
                "PASSWORD=s5532960;" +
                "Data source=localhost:1521/xepdb1");
        int i;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm fm = new RegisterForm();
            fm.Show();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            {
                i = 0;
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Users where id=:userid and password=:password";
                cmd.Parameters.Add(":userid", OracleDbType.Varchar2).Value = UserIdBox.Text;
                cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = PasswordBox.Text;

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i == 0)
                {
                    MessageBox.Show("Wrong username or password.");
                }
                else
                {
                    this.Hide();
                    Main mainForm = new Main(UserIdBox.Text);  // 아이디를 전달하여 Main 폼 생성
                    mainForm.Show();
                }

                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //판매자 로그인
                i = 0;
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from SELLER where SELLER_ID=:userid and password=:password";
                cmd.Parameters.Add(":userid", OracleDbType.Varchar2).Value = UserIdBox.Text;
                cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = PasswordBox.Text;

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i == 0)
                {
                    MessageBox.Show("Wrong username or password.");
                }
                else
                {
                    this.Hide();
                    SellerForm sellerForm = new SellerForm(UserIdBox.Text);  // 아이디를 전달하여 Main 폼 생성
                    sellerForm.Show();
                }

                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //관리자 로그인
            i = 0;
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ADMIN where ADMIN_ID=:userid and password=:password";
            cmd.Parameters.Add(":userid", OracleDbType.Varchar2).Value = UserIdBox.Text;
            cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = PasswordBox.Text;

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                MessageBox.Show("Wrong username or password.");
            }
            else
            {
                this.Hide();
                AdminForm adminForm = new AdminForm();  // 아이디를 전달하여 Main 폼 생성
                adminForm.Show();
            }

            con.Close();
        }
    }
}
