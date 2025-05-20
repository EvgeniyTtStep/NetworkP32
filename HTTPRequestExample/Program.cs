using System.Net;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        HttpWebRequest reqw =
            (HttpWebRequest)HttpWebRequest.Create("https://meteofor.com.ua/weather-chernivtsi-4972/month/");
        HttpWebResponse resp = (HttpWebResponse)reqw.GetResponse(); // створюємо об'єкт відгуку
        StreamReader
            sr = new StreamReader(resp.GetResponseStream(), Encoding.Default); // створюємо потік для читання відгуку
        string msg = sr.ReadToEnd();
        Console.WriteLine(msg); // вивести на екран все, що читається sr.Close();
        
        int index = msg.IndexOf("is-past");
        Console.WriteLine("index =" + index);
        string res = msg.Substring(index, 500);
        int indexRes = res.IndexOf("value=");
        Console.WriteLine("res = " + res);
        string degree = res.Substring(indexRes+6, 4);
        
       Console.WriteLine("degree = " + degree);
       
    }
}