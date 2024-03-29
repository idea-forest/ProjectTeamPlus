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

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<BlackList> BlackLists { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionGameAchievement> CompetitionGameAchievements { get; set; }

    public virtual DbSet<CompetitionPlayer> CompetitionPlayers { get; set; }

    public virtual DbSet<CompetitionVote> CompetitionVotes { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameAchievement> GameAchievements { get; set; }

    public virtual DbSet<GameVote> GameVotes { get; set; }

    public virtual DbSet<Igdbgenre> Igdbgenres { get; set; }

    public virtual DbSet<InboxMessage> InboxMessages { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserBadge> UserBadges { get; set; }

    public virtual DbSet<UserGameInfo> UserGameInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SteamInfoConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminUse__3214EC27888529AA");

            entity.ToTable("AdminUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .HasMaxLength(450)
                .HasColumnName("ASPNetIdentityId");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Badge__3214EC07EDD14BDF");

            entity.ToTable("Badge");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<BlackList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlackLis__3214EC0719BC2BE5");

            entity.ToTable("BlackList");

            entity.Property(e => e.SteamId).HasMaxLength(50);
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC0759C3A4E2");

            entity.ToTable("Competition");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Competition_Fk_User");

            entity.HasOne(d => d.Game).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Competition_Fk_Game");

            entity.HasOne(d => d.Status).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Competition_Fk_Status");
        });

        modelBuilder.Entity<CompetitionGameAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC077DC18BA3");

            entity.ToTable("CompetitionGameAchievement");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionGameAchievements)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("CompetitionGameAchievement_Fk_Competition");
        });

        modelBuilder.Entity<CompetitionPlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC07D984ECCF");

            entity.ToTable("CompetitionPlayer");

            entity.Property(e => e.SteamId).HasMaxLength(50);

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionPlayers)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("CompetitionPlayer_Fk_Competition");
        });

        modelBuilder.Entity<CompetitionVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC0703135BBE");

            entity.ToTable("CompetitionVote");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionVotes)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("CompetitionVote_Fk_Competition");

            entity.HasOne(d => d.User).WithMany(p => p.CompetitionVotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("CompetitionVote_Fk_User");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friend__3214EC0797F84742");

            entity.ToTable("Friend");

            entity.Property(e => e.AvatarFullUrl).HasMaxLength(100);
            entity.Property(e => e.AvatarUrl).HasMaxLength(100);
            entity.Property(e => e.GameExtraInfo).HasMaxLength(100);
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.SteamId).HasMaxLength(50);
            entity.Property(e => e.SteamName).HasMaxLength(50);

            entity.HasOne(d => d.Root).WithMany(p => p.Friends)
                .HasForeignKey(d => d.RootId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Friend_Fk_User");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Game__3214EC07DDFB349B");

            entity.ToTable("Game");

            entity.Property(e => e.DescLong).HasMaxLength(1024);
            entity.Property(e => e.DescShort).HasMaxLength(512);
            entity.Property(e => e.Genres).HasMaxLength(1024);
            entity.Property(e => e.IconUrl).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<GameAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameAchi__3214EC07341BB293");

            entity.ToTable("GameAchievement");

            entity.Property(e => e.ApiName).HasMaxLength(100);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.IconAchievedUrl).HasMaxLength(256);
            entity.Property(e => e.IconHiddenUrl).HasMaxLength(256);
        });

        modelBuilder.Entity<GameVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameVote__3214EC07E45E3C39");

            entity.ToTable("GameVote");

            entity.HasOne(d => d.Game).WithMany(p => p.GameVotes)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("GameVote_Fk_Game");

            entity.HasOne(d => d.User).WithMany(p => p.GameVotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("GameVote_Fk_User");
        });

        modelBuilder.Entity<Igdbgenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IGDBGenr__3214EC075DD32ED1");

            entity.ToTable("IGDBGenres");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<InboxMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InboxMes__3214EC07597D7312");

            entity.ToTable("InboxMessage");

            entity.Property(e => e.Content).HasMaxLength(128);
            entity.Property(e => e.Sender).HasMaxLength(50);
            entity.Property(e => e.Subject).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");

            entity.HasOne(d => d.Recipient).WithMany(p => p.InboxMessages)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InboxMessage_Fk_User");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC07C7EE4060");

            entity.ToTable("Status");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0761BB6D0B");

            entity.ToTable("User");

            entity.Property(e => e.AspNetUserId).HasMaxLength(450);
            entity.Property(e => e.AvatarUrl).HasMaxLength(100);
            entity.Property(e => e.ProfileUrl).HasMaxLength(100);
            entity.Property(e => e.SteamId).HasMaxLength(50);
            entity.Property(e => e.SteamName).HasMaxLength(50);
            entity.Property(e => e.Theme).HasMaxLength(10);
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAchi__3214EC076928D835");

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

        modelBuilder.Entity<UserBadge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserBadg__3214EC07D68B9855");

            entity.ToTable("UserBadge");

            entity.HasOne(d => d.Badge).WithMany(p => p.UserBadges)
                .HasForeignKey(d => d.BadgeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBadge_Fk_Badge");

            entity.HasOne(d => d.User).WithMany(p => p.UserBadges)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBadge_Fk_User");
        });

        modelBuilder.Entity<UserGameInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGame__3214EC073C0D35AE");

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
