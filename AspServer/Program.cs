namespace AspServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/api/hello", () => "Hello, API!");

        app.Run();
    }
}
