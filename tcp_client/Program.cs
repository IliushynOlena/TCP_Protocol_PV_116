using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using SharedData;

namespace tcp_client
{
   
    class Program
    {
        static int port = 8080; 
        static string address = "127.0.0.1"; 
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpClient client = new TcpClient();

            client.Connect(ipPoint);            
            try
            {
                Request request = new Request();
                do
                {
                    Console.Write("Enter A:");
                    request.A = double.Parse(Console.ReadLine());
                    Console.Write("Enter B:");
                    request.B = double.Parse(Console.ReadLine());
                    Console.Write("Enter operation [1-4]:");
                    request.Operation = (OperationType)Enum.Parse(typeof(OperationType), Console.ReadLine());

                    NetworkStream ns = client.GetStream();

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ns, request);
                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();
                    Console.WriteLine("server response: " + response);
                    // закриваємо потокі
                    //sw.Close();
                    //sr.Close();
                    //ns.Close();
                } while (request.A != 0 || request.B != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                client.Close();
            }
        }
    }
}
