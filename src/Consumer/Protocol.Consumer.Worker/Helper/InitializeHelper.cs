using System.Reflection;

namespace Protocol.Consumer.Worker.Helper
{
    public static class InitializeHelper
    {
        public static IConfiguration PrintConfiguration(this IConfiguration configuration)
        {
            Console.WriteLine($"Initializing {Assembly.GetExecutingAssembly().GetName().Name}. {Environment.NewLine}");

            if (configuration.AsEnumerable() != null)
                foreach(var conf in configuration.AsEnumerable())
                    Console.WriteLine($"{conf.Key} => {conf.Value}");

            return configuration;
        }
    }
}
