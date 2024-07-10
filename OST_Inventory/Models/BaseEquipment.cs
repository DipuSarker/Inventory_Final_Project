using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OST_Inventory.Models
{
    [Serializable]
    public class BaseEquipment
    {
        [DataMember]
        public int EquipmentId {  get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int EcCount { get; set; }

        [DataMember]
        public DateTime EntryDate { get; set; }

        public List<BaseEquipment> ListEquipment { get; set; }

        public BaseEquipment()
        {
            ListEquipment = new List<BaseEquipment>();
        }


        public static List<BaseEquipment> ListEqipmentDate()
        {
            List<BaseEquipment> plsData = new List<BaseEquipment>();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;    
            cmd.CommandText = "spOST_LstEquipment";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseEquipment obj = new BaseEquipment();

                    obj.EquipmentId = Convert.ToInt32(reader["EquipmentId"].ToString());
                    obj.Name = reader["Name"].ToString();
                    obj.EcCount = Convert.ToInt32(reader["Quantity"].ToString());
                    obj.EntryDate = Convert.ToDateTime(reader["EntryDate"].ToString());

                    plsData.Add(obj);
                }
            }

            cmd.Dispose();
            connection.Close();


            //for (int i = 0; i < 50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop_"+i.ToString();
            //    baseEquipment.EcCount = 5 + i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;
            //    plsData.Add(baseEquipment);
            //}

            return plsData;
            
        }

        
        public int SaveEquipment()
        {
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(ConnString);

            cmd.Connection = connection;
            cmd.CommandText = "spOST_InsEquipment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Name", this.Name);
            cmd.Parameters.Add("@EcCount", this.EcCount);
            cmd.Parameters.Add("@EntryDate", this.EntryDate);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            int returnRessult = cmd.ExecuteNonQuery();

            cmd.Dispose();
            connection.Close();

            return returnRessult;


        }
    }
}