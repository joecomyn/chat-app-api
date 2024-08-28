using Microsoft.AspNetCore.SignalR;
using ChatAppApi.DTOs;

namespace ChatAppApi.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(ChatMessageDto ReceivedMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", ReceivedMessage);
        }
    }
}
