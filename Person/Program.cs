using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using RestSharp;
using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace Person
{
    class Program
    {
        //Conecction string
        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        
        public static void Main(string[] args)
        {
            GetDeviceKey();
            FindPerson();
            SetCallback();
            FindRecords();

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

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");

            Console.WriteLine("Si no hay red, o no se inicia el servicio de recepción en la PC, el dispositivo se cargará nuevamente" +
                " en 10 minutos, y así sucesivamente hasta que el registro de reconocimiento se envíe correctamente a la PC. ");

            Console.ReadKey();
        }


    }
}
