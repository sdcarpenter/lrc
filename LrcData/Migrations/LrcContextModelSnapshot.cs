﻿// <auto-generated />
using LrcData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LrcData.Migrations
{
    [DbContext(typeof(LrcContext))]
    partial class LrcContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("LrcData.Lobby", b =>
                {
                    b.Property<string>("LobbyName")
                        .HasColumnType("text");

                    b.HasKey("LobbyName");

                    b.ToTable("Lobbies");
                });
#pragma warning restore 612, 618
        }
    }
}