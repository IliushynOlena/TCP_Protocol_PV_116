using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp_server
{
    class Program
    {
        static int port = 8080;
        static void Main(string[] args)
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");//Dns.GetHostEntry("localhost").AddressList[1]; //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

            TcpListener listener = new TcpListener(ipPoint); // bind

            try
            {
                listener.Start(10);

                Console.WriteLine("Server started! Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                while (client.Connected)
                {

                    NetworkStream ns = client.GetStream();

                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();

                    Console.WriteLine($"{client.Client.RemoteEndPoint} - {response} at {DateTime.Now.ToShortTimeString()}");

                    string message = "Message was send!";               

                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(message);

                    sw.Flush();

                    // закриваємо потокі
                    //sr.Close();
                    //sw.Close();
                    //ns.Close();
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            listener.Stop();
        }
    }
}
