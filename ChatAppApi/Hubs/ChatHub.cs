using Microsoft.AspNetCore.SignalR;
using ChatAppApi.DTOs;

namespace ChatAppApi.Hubs
{
    public class ChatHub: Hub
    {
        public async Task JoinRoom(int roomId)
        {
            string groupName = $"Room_{roomId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task LeaveRoom(int roomId)
        {
            string groupName = $"Room_{roomId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessage(ChatMessageDto ReceivedMessage, int RoomId)
        {
            await Clients.Group($"Room_{RoomId}")
                .SendAsync("ReceiveMessage", ReceivedMessage);
        }

        public async Task SendMessageUpdates(ChatMessageDto updatedMessage, int RoomId)
        {
            await Clients.Group($"Room_{RoomId}")
                         .SendAsync("ReceiveUpdatedMessage", updatedMessage);
        }

        public async Task SendMessageDeleted(ChatMessageDto deletedMessage, int RoomId)
        {
            await Clients.Group($"Room_{RoomId}")
                         .SendAsync("ReceiveDeletedMessage", deletedMessage);
        }
    }
}
