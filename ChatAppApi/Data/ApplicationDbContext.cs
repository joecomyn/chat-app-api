using Microsoft.EntityFrameworkCore;
using ChatAppApi.Models;

namespace ChatAppApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatRoom> ChatRooms { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        //Testing seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>().HasData(
                new ChatRoom { RoomId = 1, RoomName = "General" },
                new ChatRoom { RoomId = 2, RoomName = "Test 1" }
            );

            modelBuilder.Entity<ChatMessage>().HasData(
                new ChatMessage { ChatId = 1, RoomId = 1, User = "Alice", MessageBody = "Hey guys", Timestamp = DateTime.UtcNow },
                new ChatMessage { ChatId = 2, RoomId = 1, User = "Alice", MessageBody = "Any1 on?", Timestamp = DateTime.UtcNow },
                new ChatMessage { ChatId = 3, RoomId = 2, User = "Bob", MessageBody = "TESSSST", Timestamp = DateTime.UtcNow }
            );
        }

    }

}
