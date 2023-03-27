USE [Erp_Desenv]
GO

/** Object:  Table [dbo].[TbCliente]    Script Date: 27/03/2023 10:55:07 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TbCliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[CPF] [varchar](14) NOT NULL,
 CONSTRAINT [PK_TbCliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id da tabela' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TbCliente', @level2type=N'COLUMN',@level2name=N'ClienteId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nome do Cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TbCliente', @level2type=N'COLUMN',@level2name=N'Nome'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPF do Cliente.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TbCliente', @level2type=N'COLUMN',@level2name=N'CPF'
GO