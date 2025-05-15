using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class UDPProgram
{
    public static void Main(string[] args)
    {
        UdpClient server = new UdpClient(8888);
        Console.WriteLine("Server running...");
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        
        Thread thread = new Thread(() =>
            {
                while (true)
                {
                    byte[] data = server.Receive(ref endPoint);
                    string text = Encoding.UTF8.GetString(data);
                    Console.WriteLine("Client: " + text);
                }
            }
        );
        thread.Start();

        while (true)
        {
            string msg = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            server.Send(data, data.Length, endPoint);
        }
    }
}