-- Highlight then ctrl+k, ctrl+c to uncomment / recomment

--CREATE DATABASE [SteamInfo];

USE [SteamInfo];

CREATE TABLE [User]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[AspNetUserId]		NVARCHAR(450)	NOT NULL
	,[SteamId]			NVARCHAR(50)
	,[SteamName]		NVARCHAR(50)
	,[ProfileUrl]		NVARCHAR(100)
	,[AvatarUrl]		NVARCHAR(100)
	,[PersonaState]		INT
	,[PlayerLevel]		INT
);

CREATE TABLE [Friend]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[RootId]			INT				NOT NULL
	,[SteamId]			NVARCHAR(50)	NOT NULL
	,[SteamName]		NVARCHAR(50)	
	,[PersonaState]		INT				
	,[AvatarUrl]		NVARCHAR(100)	
	,[AvatarFullUrl]	NVARCHAR(100)	
	,[LastLogOff]		INT				
	,[GameExtraInfo]	NVARCHAR(100)
	,[GameId]			INT
);

CREATE TABLE [Game]
(
    [Id]                INT                NOT NULL IDENTITY(1,1) PRIMARY KEY
    ,[AppId]            INT                NOT NULL
    ,[Name]             NVARCHAR(512)      NOT NULL
    ,[DescShort]        NVARCHAR(512)      
    ,[DescLong]         NVARCHAR(1024)     
    ,[PlayTime]         INT                
    ,[IconUrl]          NVARCHAR(512)      
    ,[LastPlayed]       INT                
);

CREATE TABLE [UserGameInfo]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[OwnerId]			INT				NOT NULL
	,[GameId]			INT				NOT NULL
	,[PlayTime]			INT				
	,[LastPlayed]		INT			 
	,[Hidden]			BIT				NOT NULL
	,[Followed]			BIT				NOT NULL
)

CREATE TABLE [GameAchievement]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[GameId]			INT				NOT NULL
	,[ApiName]			NVARCHAR(100)	NOT NULL
	,[DisplayName]		NVARCHAR(50)	
	,[IconAchievedUrl]	NVARCHAR(256)	
	,[IconHiddenUrl]	NVARCHAR(256)	
	,[HiddenFromUsers]	BIT				NOT NULL
);

CREATE TABLE [UserAchievement]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[OwnerId]			INT				NOT NULL
	,[AchievementId]	INT				NOT NULL
	,[Achieved]			BIT				NOT NULL
	,[UnlockTime]		DATETIME			
);

CREATE TABLE [Competition]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[GameId]			INT				NOT NULL
	,[StartDate]		DATETIME		NOT NULL
	,[EndDate]			DATETIME		NOT NULL
);

CREATE TABLE [CompetitionPlayer]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[CompetitionId]	INT				NOT NULL
	,[SteamId]			NVARCHAR(50)	NOT NULL

);

CREATE TABLE [CompetitionGameAchievement]
(
	[Id]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[CompetitionId]	INT				NOT NULL,
	[GameAchievementId]	INT				NOT NULL
);



ALTER TABLE [Friend]			ADD CONSTRAINT [Friend_Fk_User]					FOREIGN KEY ([RootId])			REFERENCES [User] ([Id])			ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE	[UserGameInfo]		ADD CONSTRAINT [UserGameInfo_FK_User]			FOREIGN KEY ([OwnerId])			REFERENCES [User] ([Id])			ON DELETE CASCADE   ON UPDATE NO ACTION;
ALTER TABLE	[UserGameInfo]		ADD CONSTRAINT [UserGameInfo_FK_Game]			FOREIGN KEY ([GameId])			REFERENCES [Game] ([Id])			ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [UserAchievement]	ADD CONSTRAINT [UserAchievement_Fk_User]		FOREIGN KEY ([OwnerId])			REFERENCES [User] ([Id])			ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE	[UserAchievement]	ADD CONSTRAINT [UserAchievement_FK_Achievement] FOREIGN KEY ([AchievementId])	REFERENCES [GameAchievement] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Competition]		ADD CONSTRAINT [Competition_Fk_Game]			FOREIGN KEY ([GameId])			REFERENCES [Game] ([Id])			ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [CompetitionPlayer] ADD CONSTRAINT [CompetitionPlayer_Fk_Competition] FOREIGN KEY ([CompetitionId]) REFERENCES [Competition] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [CompetitionGameAchievement] ADD CONSTRAINT [CompetitionGameAchievement_Fk_Competition] FOREIGN KEY ([CompetitionId]) REFERENCES [Competition] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;