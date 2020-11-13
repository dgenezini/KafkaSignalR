using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KafkaSample.SignalRConsumer.Hubs
{
    public class KafkaHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
