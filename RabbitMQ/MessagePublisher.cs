using AuthenticationService.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.RabbitMQ
{
    public class MessagePublisher
    {
        private static IConnection _connection;
        private static IModel _channel;

        public MessagePublisher()
        {
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "stable-rabbitmq.mg.svc", UserName = "user", Password = "wHa0QFbZlX" };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("user.exchange", ExchangeType.Fanout);
        }

        public void PublishUserCreation(UserCreationDTO user)
        {
            string json = JsonConvert.SerializeObject(user);
            byte[] body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "user.exchange",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
