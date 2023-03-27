USE [Erp_Desenv]
GO

/** Object:  Table [dbo].[TbProduto]    Script Date: 27/03/2023 11:12:16 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TbProduto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Preco] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TbProduto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
)