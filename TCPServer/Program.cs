using System.Net;
using System.Net.Sockets;

class TCPServer
{
    public static void Main(string[] args)
    {
        Console.WriteLine("TCP Server");

        TcpListener server = new TcpListener(IPAddress.Any, 1111);
        server.Start();
        Console.WriteLine("Server Started");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client Connected");

            NetworkStream stream = client.GetStream();

            byte[] bytes = new byte[1024];
            int read = stream.Read(bytes, 0, bytes.Length);
            string received = System.Text.Encoding.UTF8.GetString(bytes, 0, read);
            Console.WriteLine("Отримано: " + received);
            
            string msg = "Hello from SERVER!!!";
            byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(msg);
            stream.Write(msgBytes, 0, msgBytes.Length);
            
            Console.ReadKey();
            
            client.Close();
        }
        
        server.Stop();
    }
}