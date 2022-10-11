using AutoMapper;
using Cliente.API.Controllers;
using Cliente.API.Data;
using Cliente.API.Data.Mappings.Profiles;
using Cliente.API.Data.Repository;
using Cliente.API.Data.Table;
using Cliente.API.Models.Request;
using Cliente.API.Models.Response;
using Cliente.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cliente.API.Tests.Controller
{
    public class ClienteControllerTests
    {
        [Fact]
        public async Task AdicionarClienteAsync_Adiciona_O_Cliente()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new EnderecoProfile());
            });

            using (var context = new ClienteContext(options))
            {
                var clienteService = new ClienteService(new ClienteRepository(context), new Mapper(config));

                var clienteController = new ClienteController(clienteService);

                var clienteRequest = new ClienteRequest()
                {
                    Nome = "Renan Venturini",
                    CPF = "303.278.328-35",
                    Endereco = new EnderecoRequest()
                    {
                        Logradouro = "Rua Sofia Vitali",
                        Numero = "118",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-250"
                    }
                };


                //Act
                await clienteController.AdicionarClienteAsync(clienteRequest);

                //Assert
                var clienteAdicionado = await context.Clientes.FirstOrDefaultAsync(x => x.Nome == "Renan Venturini");
                
                Assert.NotNull(clienteAdicionado);
                Assert.Equal(1, clienteAdicionado.ClienteId);
                Assert.Equal("Renan Venturini", clienteAdicionado.Nome);
                Assert.Equal("303.278.328-35", clienteAdicionado.CPF);
                Assert.Equal("Rua Sofia Vitali", clienteAdicionado.Endereco.Logradouro);
                Assert.Equal("118", clienteAdicionado.Endereco.Numero);
                Assert.Equal("Casa", clienteAdicionado.Endereco.Complemento);
                Assert.Equal("Mauá", clienteAdicionado.Endereco.Cidade);
                Assert.Equal("Jd Zaira", clienteAdicionado.Endereco.Bairro);
                Assert.Equal("SP", clienteAdicionado.Endereco.UF);
                Assert.Equal("09321-250", clienteAdicionado.Endereco.CEP);
            }
        }

        [Fact]
        public async Task ListarClienteAsync_Traz_Os_Clientes_Cadastrados()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new EnderecoProfile());
            });

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Sofia Vitali",
                        Numero = "118",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-250"
                    }
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Aristides Lopes",
                        Numero = "61",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-100"
                    }
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteService = new ClienteService(new ClienteRepository(context), new Mapper(config));

                var clienteController = new ClienteController(clienteService);

                //Act
                var result = await clienteController.ListarClienteAsync();

                var okResult = result as ObjectResult;

                var clientes = okResult.Value as IEnumerable<ClienteResponse>;

                //Assert
                var cliente = clientes.FirstOrDefault(x => x.Nome == "Aline Santos");

                Assert.NotNull(clientes);
                Assert.True(clientes.Any());
                Assert.Equal(2, clientes.Count());
                Assert.NotNull(cliente);
                Assert.Equal(2, cliente.Id);
                Assert.Equal("Aline Santos", cliente.Nome);
                Assert.Equal("303.007.748-93", cliente.CPF);
                Assert.Equal("Rua Aristides Lopes", cliente.Endereco.Logradouro);
                Assert.Equal("61", cliente.Endereco.Numero);
                Assert.Equal("Casa", cliente.Endereco.Complemento);
                Assert.Equal("Mauá", cliente.Endereco.Cidade);
                Assert.Equal("Jd Zaira", cliente.Endereco.Bairro);
                Assert.Equal("SP", cliente.Endereco.UF);
                Assert.Equal("09321-100", cliente.Endereco.CEP);
            }
        }

        [Fact]
        public async Task ClientePorIdAsync_Buscar_Cliente_Especifico_Por_Id()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new EnderecoProfile());
            });

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Sofia Vitali",
                        Numero = "118",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-250"
                    }
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Aristides Lopes",
                        Numero = "61",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-100"
                    }
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteService = new ClienteService(new ClienteRepository(context), new Mapper(config));

                var clienteController = new ClienteController(clienteService);

                //Act
                var result = await clienteController.ClientePorIdAsync(2);

                var okResult = result as ObjectResult;

                var cliente = okResult.Value as ClienteResponse;

                //Assert
                Assert.NotNull(cliente);
                Assert.Equal(2, cliente.Id);
                Assert.Equal("Aline Santos", cliente.Nome);
                Assert.Equal("303.007.748-93", cliente.CPF);
                Assert.Equal("Rua Aristides Lopes", cliente.Endereco.Logradouro);
                Assert.Equal("61", cliente.Endereco.Numero);
                Assert.Equal("Casa", cliente.Endereco.Complemento);
                Assert.Equal("Mauá", cliente.Endereco.Cidade);
                Assert.Equal("Jd Zaira", cliente.Endereco.Bairro);
                Assert.Equal("SP", cliente.Endereco.UF);
                Assert.Equal("09321-100", cliente.Endereco.CEP);

            }
        }

        [Fact]
        public async Task AtualizarClienteAsync_Atualiza_Os_Clientes_No_Banco_De_Dados()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new EnderecoProfile());
            });

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Sofia Vitali",
                        Numero = "118",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-250"
                    }

                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Aristides Lopes",
                        Numero = "61",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-100"
                    }
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteService = new ClienteService(new ClienteRepository(context), new Mapper(config));

                var clienteController = new ClienteController(clienteService);

                var clienteRequest = new AtualizarClienteRequest()
                {
                    ClienteId = 2,
                    Nome = "Maria Eduarda",
                    CPF = "123.456.789-10",
                    Endereco = new AtualizarEnderecoRequest()
                    {
                        Logradouro = "Rua Estrada do Pilar",
                        Numero = "330",
                        Complemento = "Fazenda",
                        Cidade = "Ribeirão",
                        Bairro = "Jd Pilar",
                        UF = "SP",
                        CEP = "09433-485"
                    }
                };

                //Act
                var result = await clienteController.AtualizarClienteAsync(clienteRequest);

                //Assert
                var clienteAtualizado = await context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == 2);

                Assert.NotNull(clienteAtualizado);
                Assert.Equal(2, clienteAtualizado.ClienteId);
                Assert.Equal("Maria Eduarda", clienteAtualizado.Nome);
                Assert.Equal("123.456.789-10", clienteAtualizado.CPF);
                Assert.Equal("Rua Estrada do Pilar", clienteAtualizado.Endereco.Logradouro);
                Assert.Equal("330", clienteAtualizado.Endereco.Numero);
                Assert.Equal("Fazenda", clienteAtualizado.Endereco.Complemento);
                Assert.Equal("Ribeirão", clienteAtualizado.Endereco.Cidade);
                Assert.Equal("Jd Pilar", clienteAtualizado.Endereco.Bairro);
                Assert.Equal("SP", clienteAtualizado.Endereco.UF); ;
                Assert.Equal("09433-485", clienteAtualizado.Endereco.CEP);
            }
        }

        [Fact]
        public async Task DeletarClienteAsync_Deleta_O_Cliente_No_Banco_De_Dados()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new EnderecoProfile());
            });

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Sofia Vitali",
                        Numero = "118",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-250"
                    }
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                    Endereco = new TbEnderecoCliente()
                    {
                        Logradouro = "Rua Aristides Lopes",
                        Numero = "61",
                        Complemento = "Casa",
                        Cidade = "Mauá",
                        Bairro = "Jd Zaira",
                        UF = "SP",
                        CEP = "09321-100"
                    }
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteService = new ClienteService(new ClienteRepository(context), new Mapper(config));

                var clienteController = new ClienteController(clienteService);

                //Act
                await clienteController.DeletarClienteAsync(1);

                //Assert
                var clienteDeletado = await context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == 1);

                Assert.Null(clienteDeletado);
            }
        }
    }
}
