using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class ProgramClient
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting UDP client");
        //UdpClient udpClient = new UdpClient("127.0.0.1", 1111);
        Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        string message = "Hello from UDP client!";
        byte[] data = Encoding.ASCII.GetBytes(message);
        
        //udpClient.Send(data, data.Length);
        EndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1111);
        udpClient.SendTo(data, endPoint);

        // IPEndPoint remoteEndPoint = null;
        // byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
        // string messageFromServer = Encoding.ASCII.GetString(receivedData);
        
        byte[] buffer = new byte[1024];
        EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 1111);
        int receivedData = udpClient.ReceiveFrom(buffer, ref clientEndPoint);
        string messageFromServer = Encoding.ASCII.GetString(buffer, 0, receivedData);
        Console.WriteLine("Відповідь від сервера: " + messageFromServer);
        
        udpClient.Close();
    }
}