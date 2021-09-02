USE [KyleFinley.net]
GO

/****** Object:  Table [dbo].[Articles]    Script Date: 6/26/2014 2:41:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Articles](
	[Id] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY,
	[PublishedDate] [datetime] NOT NULL,
    [Title] [varchar](100) NOT NULL,
    [Description] [varchar](200) NOT NULL,
    [Content] [text] NOT NULL,
    [Author] [varchar](50) NOT NULL

) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


