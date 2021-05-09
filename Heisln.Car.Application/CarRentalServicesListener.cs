using Heisln.Car.Contract;
using Heisln.Car.Domain;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Heisln.Car.Infrastructure
{
    public class CarRentalServicesListener : BackgroundService
    {

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CarRentalServicesListener(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName="test", Password="user" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            channel.QueueDeclare(queue: "rpc_queue", durable: false,
                  exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "rpc_queue",
              autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");

            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    var result = HandleRequest(message);
                    Console.WriteLine(response);
                    response = result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                      basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                      multiple: false);
                }
            };

            Console.WriteLine(" Press [enter] to exit.");
            return Task.CompletedTask;
        }

        private string HandleRequest(string message)
        {
            string id = message.Split(':')[1];
            var userId = Guid.Parse(id);
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                var user = userRepository.Get(userId);
                return JsonConvert.SerializeObject(user);
            }
        }
    }
}
