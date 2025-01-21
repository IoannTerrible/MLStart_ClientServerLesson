class Program
{
    static async Task Main()
    {
        using var client = new HttpClient();
        var response = await client.GetStringAsync("https://localhost:7266/api/hello");
        Console.WriteLine(response);
        Console.ReadKey();
    }
}
