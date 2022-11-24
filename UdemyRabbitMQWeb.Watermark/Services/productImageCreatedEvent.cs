using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyRabbitMQWeb.Watermark.Services
{
    public class productImageCreatedEvent
    {
        //resmin yolunu tutcez
        public string ImageName { get; set; }
    }
}
