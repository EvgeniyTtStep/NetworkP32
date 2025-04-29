using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1111);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        clientSocket.Connect(endPoint);
        Console.WriteLine("Client connected to server");
        string message = "Hello P32!";
        byte[] messageBites = Encoding.Default.GetBytes(message);
        clientSocket.Send(messageBites);

        byte[] buffer = new byte[1024];
        int receive = clientSocket.Receive(buffer);
        string response = Encoding.Default.GetString(buffer, 0, receive);
        
        Console.WriteLine("Відповідь від сервера: " + response);
        
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}