using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OST_Inventory.Models
{
    public class BaseAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }

       public bool verifyLogin()
        {
            DataTable dt = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            
            SqlConnection connection= new SqlConnection(ConnString);

            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "spOst_LstMember";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter ("@Vusername", this.Username));
            cmd.Parameters.Add(new SqlParameter("@Vpassword", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);   
            cmd.Dispose();

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            //Linq
            //var pdata = (from p in dt.AsEnumerable()
            //             where p.Field<string>("Name") == this.Username && p.Field<string>("Password") == this.Password
            //             select new
            //             {
            //                 Username = p.Field<string>("Name")
            //             }
            //             ).SingleOrDefault();

            //if (pdata!=null)
            //{
            //    return true;
            //}
            return false;
        }
    }
}