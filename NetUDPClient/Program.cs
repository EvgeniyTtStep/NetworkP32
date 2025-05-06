using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class ProgramClient
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting UDP client");
        UdpClient udpClient = new UdpClient("127.0.0.1", 1111);
        string message = "Hello from UDP client!";
        byte[] data = Encoding.ASCII.GetBytes(message);
        
        udpClient.Send(data, data.Length);

        IPEndPoint remoteEndPoint = null;
        byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
        string messageFromServer = Encoding.ASCII.GetString(receivedData);
        Console.WriteLine("Відповідь від сервера: " + messageFromServer);
        
        udpClient.Close();
    }
}