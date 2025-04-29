using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint ipep = new IPEndPoint(ip, 1111);
        
        sock.Bind(ipep);
        sock.Listen(10);
        
        Console.WriteLine("Listening for connections...");

        try
        {
            while (true)
            {
                Socket s = sock.Accept();
                
                byte[] buffer = new byte[1024];
                int recived = s.Receive(buffer);
                string recivedText = Encoding.ASCII.GetString(buffer, 0, recived);
                Console.WriteLine("Отриано від клієнта " + recivedText);
                
                
                Console.WriteLine("Connection accepted клієнт підключився");
                Console.WriteLine(s.RemoteEndPoint);
                s.Send(Encoding.ASCII.GetBytes("Hello from Server " + DateTime.Now));
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}