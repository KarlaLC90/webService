using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using RestSharp;
using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySql.Data.EntityFramework;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity;
using System.Windows;
using System.Data;

namespace Person
{
    class Program
    {
        public static void Main(string[] args)
        {

            MySqlConnection conn = new MySqlConnection("server=74.208.244.101;port=32006;database=hfsecurity;uid=root;password=Preasyst2016;pooling = false; convert zero datetime=True");
            
            conn.Open();
            MessageBox.Show("ServerVersion: " + conn.ServerVersion +
            "\nState: " + conn.State.ToString());
            Console.WriteLine("El estado de la conexión es: " + conn.State);

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");

            pa_check_in_outs(1, DateTime.Now, 1, 1);
            SelectCommand();
            GetDeviceKey();
            FindPerson();
            SetCallback();
            FindRecords();


        }

        public static void SelectCommand()
        {
            MySqlConnection conn = new MySqlConnection("server=74.208.244.101;port=32006;database=hfsecurity;uid=root;password=Preasyst2016;;pooling = false; convert zero datetime=True");
            string Query = "select * from pa_check_in_outs;";
            MySqlCommand MyCommand2 = new MySqlCommand(Query, conn);

            //For offline connection we weill use  MySqlDataAdapter class.  
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
        }

        public static void pa_check_in_outs(int enroll_id, DateTime check_dt, int org_company_id, int status)
        {

            string connectionString = @"server=74.208.244.101;port=32006;database=hfsecurity;uid=root;password=Preasyst2016;pooling = false; convert zero datetime=True";

            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO pa_check_in_outs(enroll_id, check_dt, org_company_id, status) VALUES(@enroll_id, '@check_dt', @org_company_id, @status)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@enroll_id", enroll_id);
                cmd.Parameters.AddWithValue("@check_dt", check_dt.ToString("YYYY-MM-DD"));
                cmd.Parameters.AddWithValue("@org_company_id", org_company_id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT * FROM pa_check_in_outs";
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

           
        }


        public class DeviceKey
        {
            public string data { get; set; }
            public int result { get; set; }
            public bool success { get; set; }
        }
        public static void GetDeviceKey()
        {

            try
            {
                Console.WriteLine("Proyecto de prueba in C#\r");
                Console.WriteLine("------------------------\n");

                Console.WriteLine("Obteniendo el numero del dispositivo como test para validar la conexion");

                var client = new RestClient("http://192.168.15.27:8090/getDeviceKey");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                Console.WriteLine("El numero del dispositivo es: " + response.Content);
                DeviceKey myDeserializedClass = JsonConvert.DeserializeObject<DeviceKey>(response.Content);

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

        }
        public static void FindPerson()
        {
            Console.WriteLine("Buscando a un empleado en el dispositivo");
            
            try
            {
                var client = new RestClient("http://192.168.15.27:8090/person/find");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("pass", "87654321");
                request.AddParameter("id", "3");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

            }
           
            catch (Exception e)
            {
               
                Console.WriteLine(e);
            }
            
        }
        public static void SetCallback()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("El dispositivo proporciona 2 formas de obtener registros.\n");
            Console.WriteLine("1. Establecer la dirección del callback.");
            Console.WriteLine("Después de configurar, el registro generado por el reconocimiento exitoso del dispositivo " +
                "se enviará a la dirección en tiempo real. Los registros de reconocimiento se envían correctamente a la PC..\n");

            var client = new RestClient("http://192.168.15.27:8090/setIdentifyCallBack");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("pass", "87654321");
            request.AddParameter("callbackUrl", "C:\\Users\\garde\\Desktop\\callback");
            request.AddParameter("ip", "192.168.15.27");
            request.AddParameter("imgType", "1");
            
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");
        
        }

        public class Record
        {
            public double temperature { get; set; }
            public object time { get; set; }
            public int id { get; set; }
            public string path { get; set; }
            public int state { get; set; }
            public int type { get; set; }
            public string personId { get; set; }
            public int model { get; set; }
        }

        public class PageInfo
        {
            internal readonly string records;

            public int index { get; set; }
            public int size { get; set; }
            public int total { get; set; }
            public int length { get; set; }
        }

        public class Data
        {
            public List<Record> records { get; set; }
            public PageInfo pageInfo { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public int result { get; set; }
            public bool success { get; set; }

        }

        

    public static void FindRecords() 
        { 
            Console.WriteLine("2. Extraer registros de identificación: consulta de registros de identificación, " +
                "según el período de tiempo, el personal y otras condiciones de filtrado, extraer directamente los " +
                "registros de identificación del dispositivo..\n");


        var client = new RestClient("http://192.168.15.27:8090/findRecords");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("pass", "87654321");
            request.AddParameter("personId", "-1");
            request.AddParameter("length", "-1");
            request.AddParameter("index", "0");
            request.AddParameter("startTime", "0");
            request.AddParameter("endTime", "0");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);

            /*
            for (int i = 0; i < Root; i++)
            {
                Console.WriteLine(Root.data.pageInfo.records[i].ToString());
            }
            */
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");

            Console.WriteLine("Si no hay red, o no se inicia el servicio de recepción en la PC, el dispositivo se cargará nuevamente" +
                " en 10 minutos, y así sucesivamente hasta que el registro de reconocimiento se envíe correctamente a la PC. ");

            Console.ReadKey();
        }

    }
}
