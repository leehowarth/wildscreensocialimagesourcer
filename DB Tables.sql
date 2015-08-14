USE [WildscreenAnimalsAPI_db]
GO

/****** Object:  Table [dbo].[AnimalImages]    Script Date: 14/08/2015 17:48:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AnimalImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Animal_Id] [int] NULL,
	[Name] [nchar](1024) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[SourceSystem] [nchar](1024) NOT NULL,
	[Upvotes] [int] NULL,
	[Downvotes] [int] NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF__CreatedOn]  DEFAULT (getdate()),
 CONSTRAINT [PK_AnimalImages] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[AnimalImages]  WITH CHECK ADD FOREIGN KEY([Animal_Id])
REFERENCES [dbo].[Animals] ([Id])
GO

ALTER TABLE [dbo].[AnimalImages]  WITH CHECK ADD FOREIGN KEY([Animal_Id])
REFERENCES [dbo].[Animals] ([Id])
GO






USE [WildscreenAnimalsAPI_db]
GO

/****** Object:  Table [dbo].[Animals]    Script Date: 14/08/2015 17:48:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Animals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](1024) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_CreatedOn]  DEFAULT (getdate()),
 CONSTRAINT [PK_Animals] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO


