using System.ComponentModel.DataAnnotations;

namespace ChatAppApi.Models
{
    public class ChatRoom
    {

        [Key]
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    }

}
