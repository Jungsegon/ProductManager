using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace dbpProject
{
    public partial class RegisterForm : Form
    {
        OracleConnection con = new OracleConnection("USER ID = S5532960; " +
                "PASSWORD=s5532960;" +
                "Data source=localhost:1521/xepdb1");

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm fm = new LoginForm();
            fm.Show();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Users (id, password, name, email, regisdate) VALUES (:id, :password, :name, :email, :regisdate)";
            cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = IdBox.Text;
            cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = PasswordBox.Text;
            cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = UsernameBox.Text;
            cmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = EmailBox.Text;
            cmd.Parameters.Add(":regisdate", OracleDbType.Date).Value = DateTime.Now;

            DataTable de = new DataTable();
            OracleDataAdapter dp = new OracleDataAdapter(cmd);
            dp.Fill(de);
            con.Close();

            MessageBox.Show("회원가입이 완료되었습니다.");
        }
    }
}

