using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.MessageQueue
{
    public interface IMessageQueue
    {
        Task Publish<T>(T message, string queueName);
    }
}
