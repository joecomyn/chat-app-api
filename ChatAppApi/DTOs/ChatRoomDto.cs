namespace ChatAppApi.DTOs
{
    public class ChatRoomDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public List<ChatMessageDto> ChatMessages { get; set; }
    }

}
