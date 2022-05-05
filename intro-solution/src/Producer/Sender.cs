using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory{HostName = "localhost"};
            using var conncetion = factory.CreateConnection();
            using var channel = conncetion.CreateModel();
            channel.QueueDeclare("BasicTest", false, false, false, null);
            var message = "getting started";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", "BasicTest", null, body);
            Console.WriteLine($"Sent message {message}");
            Console.WriteLine("Press [enter] to exit the sender app...");
            Console.ReadLine();
        }
        
        
    }
}