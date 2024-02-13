CREATE DATABASE [EclipseWorksChallengerDb]
GO
USE [EclipseWorksChallengerDb]
GO
CREATE TABLE [dbo].[Owner](
	[IdOwner] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IdPosition] [int] NOT NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[IdOwner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Project](
	[IdProject] [int] IDENTITY(1,1) NOT NULL,
	[NameProject] [varchar](50) NOT NULL,
	[IdOwner] [int] NOT NULL,
	[Observation] [varchar](50) NOT NULL,
	[CreatedAt] [date] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[IdProject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Owner] FOREIGN KEY([IdOwner])
REFERENCES [dbo].[Owner] ([IdOwner])
GO

ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Owner]
GO

CREATE TABLE [dbo].[Task](
	[IdTask] [int] IDENTITY(1,1) NOT NULL,
	[IdPriority] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IdProject] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[IdOwner] [int] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[IdTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Owner] FOREIGN KEY([IdOwner])
REFERENCES [dbo].[Owner] ([IdOwner])
GO

ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Owner]
GO

ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Project] FOREIGN KEY([IdProject])
REFERENCES [dbo].[Project] ([IdProject])
GO

ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Project]
GO

CREATE TABLE [dbo].[Comment](
	[IdComment] [int] IDENTITY(1,1) NOT NULL,
	[IdTask] [int] NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[IdComment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Task] FOREIGN KEY([IdTask])
REFERENCES [dbo].[Task] ([IdTask])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Task]
GO

CREATE TABLE [dbo].[HistoryTask](
	[IdHistoryTask] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[DescriptionTask] [varchar](250) NULL,
	[IdOwner] [int] NOT NULL,
	[IdProject] [int] NOT NULL,
	[IdTask] [int] NULL,
	[IdComment] [int] NULL,
	[DescriptionComment] [varchar](250) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_HistoryTask] PRIMARY KEY CLUSTERED 
(
	[IdHistoryTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Owner]
           ([Name]
           ,[IdPosition])
     VALUES
           ('Marcilio Gomes'
           ,1)
GO
INSERT INTO [dbo].[Owner]
           ([Name]
           ,[IdPosition])
     VALUES
           ('Paulo Castro'
           ,2)
GO
INSERT INTO [dbo].[Owner]
           ([Name]
           ,[IdPosition])
     VALUES
           ('Luiza Sousa'
           ,3)
GO