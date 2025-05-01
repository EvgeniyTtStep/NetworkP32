using System.Net;
using System.Net.Sockets;
using System.Text;

class AsyncServer
{
    private IPEndPoint endPoint;
    private Socket socket;

    public AsyncServer(string ip, int port)
    {
        endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    void AcceptCallback(IAsyncResult ar)
    {
        //отримує силку на наступний сокет
        Socket handler = (Socket)ar.AsyncState;
        //клієнтський сокет
        Socket socket = handler.EndAccept(ar);
        //info про підключення 
        Console.WriteLine(socket.RemoteEndPoint.ToString());
        byte[] bytes = new byte[1024];
        bytes = Encoding.ASCII.GetBytes("Hello from Server " + DateTime.Now);
        socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None,new AsyncCallback(SendCallback), socket);
        handler.BeginAccept(new AsyncCallback(AcceptCallback), socket);
    }

    void SendCallback(IAsyncResult ar)
    {
        Socket handler = (Socket)ar.AsyncState;
        ((Socket)ar.AsyncState).EndSend(ar);
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }

    public void StartServer()
    {
        if (socket != null)
        {
            return;
        }
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        socket.Bind(endPoint);
        socket.Listen(10);
        socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
    }
}

class Program
{
    public static void Main(string[] args)
    {
        AsyncServer server = new AsyncServer("127.0.0.1", 80);
        server.StartServer();
        Console.Read();
    }
}