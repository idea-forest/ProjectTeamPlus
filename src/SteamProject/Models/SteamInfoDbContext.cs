﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SteamProject.Models;

public partial class SteamInfoDbContext : DbContext
{
    public SteamInfoDbContext()
    {
    }

    public SteamInfoDbContext(DbContextOptions<SteamInfoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionGameAchievement> CompetitionGameAchievements { get; set; }

    public virtual DbSet<CompetitionPlayer> CompetitionPlayers { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameAchievement> GameAchievements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserGameInfo> UserGameInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SteamInfoConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC07BA57519B");

            entity.ToTable("Competition");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Game).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Competition_Fk_Game");
        });

        modelBuilder.Entity<CompetitionGameAchievement>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<CompetitionPlayer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CompetitionPlayer");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friend__3214EC07B7D0D1B4");

            entity.ToTable("Friend");

            entity.Property(e => e.AvatarFullUrl).HasMaxLength(100);
            entity.Property(e => e.AvatarUrl).HasMaxLength(100);
            entity.Property(e => e.GameExtraInfo).HasMaxLength(100);
            entity.Property(e => e.SteamId).HasMaxLength(50);
            entity.Property(e => e.SteamName).HasMaxLength(50);

            entity.HasOne(d => d.Root).WithMany(p => p.Friends)
                .HasForeignKey(d => d.RootId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Friend_Fk_User");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Game__3214EC07502D819A");

            entity.ToTable("Game");

            entity.Property(e => e.DescLong).HasMaxLength(1024);
            entity.Property(e => e.DescShort).HasMaxLength(512);
            entity.Property(e => e.IconUrl).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<GameAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameAchi__3214EC07FF515A19");

            entity.ToTable("GameAchievement");

            entity.Property(e => e.ApiName).HasMaxLength(100);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.IconAchievedUrl).HasMaxLength(256);
            entity.Property(e => e.IconHiddenUrl).HasMaxLength(256);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC071F2D8D65");

            entity.ToTable("User");

            entity.Property(e => e.AspNetUserId).HasMaxLength(450);
            entity.Property(e => e.AvatarUrl).HasMaxLength(100);
            entity.Property(e => e.ProfileUrl).HasMaxLength(100);
            entity.Property(e => e.SteamId).HasMaxLength(50);
            entity.Property(e => e.SteamName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAchi__3214EC0747B1F1FF");

            entity.ToTable("UserAchievement");

            entity.Property(e => e.UnlockTime).HasColumnType("datetime");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserAchievement_FK_Achievement");

            entity.HasOne(d => d.Owner).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserAchievement_Fk_User");
        });

        modelBuilder.Entity<UserGameInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGame__3214EC074896167B");

            entity.ToTable("UserGameInfo");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGameInfos)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserGameInfo_FK_Game");

            entity.HasOne(d => d.Owner).WithMany(p => p.UserGameInfos)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("UserGameInfo_FK_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
