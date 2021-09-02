USE [KyleFinley.net]
GO

/****** Object:  Table [dbo].[Redirects]    Script Date: 6/26/2014 2:41:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Redirects](
	[Id] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY,
	[OldPath] [varchar](500) NOT NULL,
	[NewPath] [varchar](500) NOT NULL

) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


