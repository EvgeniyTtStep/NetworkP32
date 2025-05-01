using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

class AsyncClient
{
    static void ConnectCallback(IAsyncResult ar)
    {
        Socket client = (Socket)ar.AsyncState;
        client.EndConnect(ar);
        
        Console.WriteLine("Підключення до сервера");
        
        byte[] bytes = new byte[1024];
        client.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), new Tuple<Socket, byte[]>(client, bytes));
    }

    static void ReceiveCallback(IAsyncResult ar)
    {
        var arAsyncState = (Tuple<Socket, byte[]>)ar.AsyncState;
        var client = arAsyncState.Item1;
        var bytes = arAsyncState.Item2;


        int receive = client.EndReceive(ar);
        string text = Encoding.ASCII.GetString(bytes, 0, receive);
        
        Console.WriteLine("Отримано від сервера: " + text);
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    public static void Main(string[] args)
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        client.BeginConnect(ipep, new AsyncCallback(ConnectCallback), client);
        
        Console.Read();
        
    }
}