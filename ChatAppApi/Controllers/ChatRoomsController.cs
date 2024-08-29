using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAppApi.Data;
using ChatAppApi.DTOs;
using ChatAppApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using ChatAppApi.Hubs;

namespace ChatAppApi.Controllers
{
    [Route("chatapi/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularApp")]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatRoomsController(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoomDto>>> GetChatRooms()
        {
            var chatRooms = await _context.ChatRooms
                .Include(r => r.ChatMessages)
                .Select(r => new ChatRoomDto
                {
                    RoomId = r.RoomId,
                    RoomName = r.RoomName,
                    ChatMessages = r.ChatMessages.Select(m => new ChatMessageDto
                    {
                        ChatId = m.ChatId,
                        User = m.User,
                        MessageBody = m.MessageBody,
                        Timestamp = m.Timestamp
                    }).ToList()
                }).ToListAsync();

            return chatRooms;
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<ChatRoomDto>> GetChatRoom(int roomId)
        {
            var chatRoom = await _context.ChatRooms
                .Include(r => r.ChatMessages)
                .Where(r => r.RoomId == roomId)
                .Select(r => new ChatRoomDto
                {
                    RoomId = r.RoomId,
                    RoomName = r.RoomName,
                    ChatMessages = r.ChatMessages.Select(m => new ChatMessageDto
                    {
                        ChatId = m.ChatId,
                        User = m.User,
                        MessageBody = m.MessageBody,
                        Timestamp = m.Timestamp
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (chatRoom == null)
            {
                return NotFound();
            }

            return chatRoom;
        }

        /*
         Endpoints that deal with Chat messages:
            - Combined with ChatRoomsController as chats would need to be accessed in the context
              of their respective rooms and separating into different controllers would mean more
              api calls which might slow down near time chatting
        */

        [HttpPost("{roomId}/ChatMessages")]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(int roomId, [FromBody] CreateChatMessageDto newMessageDto)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(roomId);
            if (chatRoom == null)
            {
                return NotFound($"Chat room with ID {roomId} not found.");
            }

            var chatMessage = new ChatMessage
            {
                User = newMessageDto.User,
                MessageBody = newMessageDto.MessageBody,
                RoomId = roomId
            };

            try
            {
                _context.ChatMessages.Add(chatMessage);
                await _context.SaveChangesAsync();

                var chatMessageDto = new ChatMessageDto
                {
                    ChatId = chatMessage.ChatId,
                    User = chatMessage.User,
                    MessageBody = chatMessage.MessageBody,
                    Timestamp = chatMessage.Timestamp
                };

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", new { 
                    roomId = chatMessage.RoomId,
                    chatMessage.ChatId,
                    chatMessage.User,
                    chatMessage.MessageBody,
                    chatMessage.Timestamp
                });
                return CreatedAtAction(
                    nameof(GetChatMessage),
                    new { roomId = chatMessage.RoomId, chatMessageId = chatMessage.ChatId },
                    chatMessageDto);
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new chat message");
            }
        }

        [HttpGet("{roomId}/ChatMessages/{chatMessageId}")]
        public async Task<ActionResult<ChatMessageDto>> GetChatMessage(int roomId, int chatMessageId)
        {
            var chatMessage = await _context.ChatMessages
                .Where(cm => cm.RoomId == roomId && cm.ChatId == chatMessageId)
                .Select(cm => new ChatMessageDto
                {
                    ChatId = cm.ChatId,
                    User = cm.User,
                    MessageBody = cm.MessageBody,
                    Timestamp = cm.Timestamp
                }).FirstOrDefaultAsync();

            if (chatMessage == null)
            {
                return NotFound($"Chat message not found.");
            }

            return chatMessage;
        }

        [HttpDelete("{roomId}/ChatMessages/{chatMessageId}")]
        public async Task<ActionResult<ChatMessage>> DeleteChatMessage(int roomId, int chatMessageId)
        {
            var chatMessage = await _context.ChatMessages
                .FirstOrDefaultAsync(cm => cm.RoomId == roomId && cm.ChatId == chatMessageId);

            if (chatMessage == null)
            {
                return NotFound($"Chat message not found.");
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return chatMessage;
        }

    }
}
