using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.Settings
{
    public class AppSettings
    {
        public string RedisConnectionString { get; set; }
        public string RabbitMQConnectionString { get; set; }
    }
}
