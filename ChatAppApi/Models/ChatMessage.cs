using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppApi.Models
{

    public class ChatMessage
    {

        [Key]
        public int ChatId { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string MessageBody { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("ChatRoom")]
        public int RoomId { get; set; }

        public ChatRoom ChatRoom { get; set; }

    }

}
