using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPClientExample
{
    public static void Main(string[] args)
    {
        TcpClient client = new TcpClient("127.0.0.1", 1111);
        NetworkStream stream = client.GetStream();
        
        string message = "Hello from CLIENT!";
        byte[] msg = Encoding.ASCII.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
        
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Відповідь від сервера: " + response);
        
        Console.ReadKey();
        
        client.Close();
        stream.Close();
        
    }
}