using System.Net;
using System.Text;


class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://sinoptik.ua/pohoda/chernivtsi");
        request.Headers.Add("User-Agent", "C# App");

        HttpResponseMessage response = await client.SendAsync(request);
        string result = await response.Content.ReadAsStringAsync();

        Console.WriteLine(result);
    }
}