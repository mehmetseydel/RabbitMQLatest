using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyRabbitMQWeb.ExcelCreate.Services
{
    public class RabbitMQClientService
    {
        //constructorda 1 kez set etcez dahada setedilmesin
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ExcelDirectExchange";
        public static string RoutingExcel = "excel-route-file";
        public static string QueueName = "queue-excel-file";

        private readonly ILogger<RabbitMQClientService> _logger;


        //const kur
        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            // Connect(); gerek kalmadı producede var

        }
        public IModel Connect()

        {
            //kanal yok 
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            //kanal aç
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            //kuyruk kanala map'le!
            _channel.QueueDeclare(QueueName, true, false, false, null);
            //bind ve route
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingExcel);
            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

            return _channel;


        }

        public void Dispose()
        {
            //kanal var ise kapat
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");

        }

    }
}
