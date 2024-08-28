﻿// <auto-generated />
using System;
using ChatAppApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatAppApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240824203409_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ChatAppApi.Models.ChatMessage", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ChatId");

                    b.HasIndex("RoomId");

                    b.ToTable("ChatMessages");

                    b.HasData(
                        new
                        {
                            ChatId = 1,
                            MessageBody = "Hey guys",
                            RoomId = 1,
                            Timestamp = new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(377),
                            User = "Alice"
                        },
                        new
                        {
                            ChatId = 2,
                            MessageBody = "Any1 on?",
                            RoomId = 1,
                            Timestamp = new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(378),
                            User = "Alice"
                        },
                        new
                        {
                            ChatId = 3,
                            MessageBody = "TESSSST",
                            RoomId = 2,
                            Timestamp = new DateTime(2024, 8, 24, 20, 34, 9, 494, DateTimeKind.Utc).AddTicks(379),
                            User = "Bob"
                        });
                });

            modelBuilder.Entity("ChatAppApi.Models.ChatRoom", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RoomId");

                    b.ToTable("ChatRooms");

                    b.HasData(
                        new
                        {
                            RoomId = 1,
                            RoomName = "General"
                        },
                        new
                        {
                            RoomId = 2,
                            RoomName = "Test 1"
                        });
                });

            modelBuilder.Entity("ChatAppApi.Models.ChatMessage", b =>
                {
                    b.HasOne("ChatAppApi.Models.ChatRoom", "ChatRoom")
                        .WithMany("ChatMessages")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("ChatAppApi.Models.ChatRoom", b =>
                {
                    b.Navigation("ChatMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
