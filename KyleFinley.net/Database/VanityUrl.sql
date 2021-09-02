USE [KyleFinley.net]
GO

/****** Object:  Table [dbo].[VanityUrl]    Script Date: 6/26/2014 2:41:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[VanityUrl](
	[Id] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY,
	[Url] [varchar](50) NOT NULL,
	[EntityId] [uniqueidentifier] NOT NULL,
	[EntityType] [int] NOT NULL

) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


