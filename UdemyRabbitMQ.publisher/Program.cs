using RabbitMQ.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UdemyRabbitMQ.publisher
{

    public enum LogNames 
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
    class Program
    {
        static void Main(string[] args)
        {
           
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://fxumfxbs:5LB0xRPojDDjeZoXQHo-nbz7qyjxIicE@hawk.rmq.cloudamqp.com/fxumfxbs");

            
            using var connection = factory.CreateConnection();

           

           
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("header-exchange",type:ExchangeType.Headers,durable:true);

            Dictionary<string, object> headers = new Dictionary<string, object>();
            headers.Add("format","pdf");
            headers.Add("shape2","a4");

            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;
            properties.Persistent = true;


            var product = new Product
            {
                Id = 1,
                Name = "Kalem",
                Price = 100,
                Stock = 10
            };

            var productJsonString = JsonSerializer.Serialize(product);

            channel.BasicPublish("header-exchange",string.Empty,properties, Encoding.UTF8.GetBytes(productJsonString));

            Console.Write("mesaj gönderilmiştir");
            Console.ReadLine();



        }
    }
}
