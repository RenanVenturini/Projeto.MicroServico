﻿CREATE TABLE TbCliente(
	Id INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(200) NOT NULL,
	CPF VARCHAR(14) NOT NULL,
	EnderecoId int not null
	CONSTRAINT PK_TbCliente PRIMARY KEY (Id)
)

ALTER TABLE TbCliente WITH CHECK ADD CONSTRAINT FK_TbCliente_TbEndereco FOREIGN KEY(EnderecoId)
REFERENCES TbEndereco(EnderecoId)

ALTER TABLE TbCliente CHECK CONSTRAINT FK_TbCliente_TbEndereco
