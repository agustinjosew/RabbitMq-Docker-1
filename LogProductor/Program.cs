using System;
using System.Text;
using RabbitMQ.Client;

namespace LogProductor
{
    internal class Program
    {
        private static void Main()
        {
            // entry point to the rabbitMQ AMQP (Advanced Message Queuing Protocol) Client
            ConnectionFactory factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            Console.WriteLine("Please type your message.");
            Console.WriteLine("Type 'exit' to exit.");

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "exit")
                {
                    connection.Close();
                    break;
                }
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"\t[x] Sent {message}");
            }

        }
    }
}
