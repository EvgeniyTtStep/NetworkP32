using System.Net;
using System.Net.Sockets;
using System.Text;

class ProgramServer
{
    public static void Main(string[] args)
    {
        int port = 1111;
        //UdpClient udpClient = new UdpClient(port);
        Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Console.WriteLine("Server started...");
        
        EndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        udpClient.Bind(ipEndPoint);
        
        byte[] buffer = new byte[1024];

        while (true)
        {
            //byte[] buffer = udpClient.Receive(ref ipEndPoint);
            int receiveFrom = udpClient.ReceiveFrom(buffer, ref ipEndPoint);
            string message = Encoding.ASCII.GetString(buffer, 0, receiveFrom);
            Console.WriteLine("Отримано від клієнта: " + message);
            
            string command = "Сервер отримав: " + message;
            byte[] messageBytes = Encoding.ASCII.GetBytes(command);
            udpClient.SendTo(messageBytes, ipEndPoint);
        }
    }
}