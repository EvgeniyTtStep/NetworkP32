using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPProgram
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting UDP client");

        UdpClient udpClient = new UdpClient();

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 8888);
        udpClient.Connect(endPoint);


        Thread thread = new Thread(() =>
            {
                while (true)
                {
                    IPEndPoint remoteEndPoint = null;
                    byte[] buffer = udpClient.Receive(ref remoteEndPoint);
                    string message = Encoding.ASCII.GetString(buffer);
                    Console.WriteLine("Server: " + message);
                }
            }
        );
        thread.Start();

        while (true)
        {
            string msg = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            udpClient.Send(data, data.Length);
        }
    }
}