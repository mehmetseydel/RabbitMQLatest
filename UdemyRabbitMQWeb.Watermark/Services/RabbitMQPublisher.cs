using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UdemyRabbitMQWeb.Watermark.Services
{
    public class RabbitMQPublisher
    {
        //burdan kanal geliyi
        private readonly RabbitMQClientService _rabbitMQClientService;

        //const ekle
        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }
        public void Publish(productImageCreatedEvent productImageCreatedEvent)
        {
            //kanalı al aynı nesne örneği singleton diye geld,
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            //byte'a cevir
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);

        }

        }
}
