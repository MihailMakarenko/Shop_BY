using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IRabbitMqService
    {
        Task SendMessage(object obj, string queue);
        Task SendMessage(string message, string queue);
    }
}
