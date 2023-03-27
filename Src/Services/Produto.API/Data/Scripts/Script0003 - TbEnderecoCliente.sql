USE [Erp_Desenv]
GO

/** Object:  Table [dbo].[TbEnderecoCliente]    Script Date: 27/03/2023 11:02:27 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TbEnderecoCliente](
	[EnderecoId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Logradouro] [varchar](100) NOT NULL,
	[Numero] [varchar](20) NOT NULL,
	[Complemento] [varchar](50) NOT NULL,
	[Cidade] [varchar](50) NOT NULL,
	[Bairro] [varchar](50) NOT NULL,
	[UF] [varchar](2) NOT NULL,
	[CEP] [varchar](9) NOT NULL,
 CONSTRAINT [PK_TbEnderecoCliente] PRIMARY KEY CLUSTERED 
(
	[EnderecoId] ASC
)ON [PRIMARY]
)


ALTER TABLE [dbo].[TbEnderecoCliente]  WITH CHECK ADD  CONSTRAINT [FK_TbEnderecoCliente_TbCliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[TbCliente] ([ClienteId])

ALTER TABLE [dbo].[TbEnderecoCliente] CHECK CONSTRAINT [FK_TbEnderecoCliente_TbCliente]
