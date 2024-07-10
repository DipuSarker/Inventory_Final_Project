using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OST_Inventory.Models
{
    public class BaseCustomer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }


        public static List<BaseCustomer> LstCustomerData()
        {
            List<BaseCustomer> plsCust = new List<BaseCustomer>();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "dbo.spOST_LstCustomer";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader mrd = cmd.ExecuteReader();

            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseCustomer obj = new BaseCustomer();

                    obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
                    obj.CustomerName = mrd["CustomerName"].ToString();
                    obj.CustomerMobile = mrd["CustomerMobile"].ToString();

                    plsCust.Add(obj);
                }
            }

            cmd.Dispose();
            connection.Close();

            return plsCust;
        }


        public static DataTable ListCustomerEquipment()
        {
            DataTable dt = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "dbo.spOST_LstCustomerEqiAssignment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            cmd.Dispose();
            connection.Close();

            return dt;
        }
      
    }
}