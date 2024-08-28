namespace ChatAppApi.DTOs
{
    public class ChatMessageDto
    {
        public int ChatId { get; set; }
        public string? User { get; set; }
        public string? MessageBody { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
