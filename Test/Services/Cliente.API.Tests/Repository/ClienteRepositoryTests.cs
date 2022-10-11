using Bogus;
using Cliente.API.Data;
using Cliente.API.Data.Repository;
using Cliente.API.Data.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cliente.API.Tests.Repository
{
    public class ClienteRepositoryTests
    {
        [Fact]
        public async Task AdicionarClienteAsync_Adiciona_O_Cliente()
        {
            //Arrange
            var fakeEndereco = new Faker<TbEnderecoCliente>()
                .RuleFor(fake => fake.Logradouro, "Rua Sofia Vitali")
                .RuleFor(fake => fake.Numero, "118")
                .RuleFor(fake => fake.Complemento, "Casa")
                .RuleFor(fake => fake.Cidade, "Mauá")
                .RuleFor(fake => fake.Bairro, "Jd Zaira")
                .RuleFor(fake => fake.UF, "SP")
                .RuleFor(fake => fake.CEP, "09321-250")
                .Generate();

            var fakeCliente = new Faker<TbCliente>()
                .RuleFor(fake => fake.ClienteId, 0)
                .RuleFor(fake => fake.Nome, "Renan Venturini")
                .RuleFor(fake => fake.CPF, "303.278.328-35")
                .RuleFor(fake => fake.Endereco, () => fakeEndereco)
                .Generate();

            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new ClienteContext(options))
            {
                var clienteRepository = new ClienteRepository(context);
                
                //Act
                await clienteRepository.AdicionarClienteAsync(fakeCliente);

                //Assert
                var clienteAdicionado = await context.Clientes
                    .Include(x => x.Endereco)
                    .FirstOrDefaultAsync(x => x.Nome == "Renan Venturini");

                Assert.NotNull(clienteAdicionado);
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

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                      ClienteId = 1,
                      Nome = "Renan Venturini",
                      CPF = "303.278.328.35"
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 1,
                    Logradouro = "Rua Sofia Vitali",
                    Numero = "118",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-250"
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93"
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 2,
                    Logradouro = "Rua Aristides Lopes",
                    Numero = "61",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-100"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {

                var clienteRepository = new ClienteRepository(context);

                //Act
                var result = await clienteRepository.ListarClienteAsync();

                //Assert
                var cliente = result.FirstOrDefault(x => x.ClienteId == 1);

                Assert.NotNull(result);
                Assert.True(result.Any());
                Assert.Equal(2, result.Count());
                Assert.NotNull(cliente);
                Assert.Equal(1, cliente.ClienteId);
                Assert.Equal("Renan Venturini", cliente.Nome);
                Assert.Equal("303.278.328.35", cliente.CPF);
                Assert.Equal("Rua Sofia Vitali", cliente.Endereco.Logradouro);
                Assert.Equal("118", cliente.Endereco.Numero);
                Assert.Equal("Casa", cliente.Endereco.Complemento);
                Assert.Equal("Mauá", cliente.Endereco.Cidade);
                Assert.Equal("Jd Zaira", cliente.Endereco.Bairro);
                Assert.Equal("SP", cliente.Endereco.UF);
                Assert.Equal("09321-250", cliente.Endereco.CEP);
            }
        }

        [Fact]
        public async Task ClientePorIdAsync_Buscar_Cliente_Especifico_Por_Id()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35"
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 1,
                    Logradouro = "Rua Sofia Vitali",
                    Numero = "118",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-250"
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 2,
                    Logradouro = "Rua Aristides Lopes",
                    Numero = "61",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-100"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteRepository = new ClienteRepository(context);

                //Act
                var result = await clienteRepository.ClientePorIdAsync(1);

                //Assert
                Assert.NotNull(result);
                Assert.Equal("Renan Venturini", result.Nome);
                Assert.Equal("303.278.328.35", result.CPF);
                Assert.Equal("Rua Sofia Vitali", result.Endereco.Logradouro);
                Assert.Equal("118", result.Endereco.Numero);
                Assert.Equal("Casa", result.Endereco.Complemento);
                Assert.Equal("Mauá", result.Endereco.Cidade);
                Assert.Equal("Jd Zaira", result.Endereco.Bairro);
                Assert.Equal("SP", result.Endereco.UF);
                Assert.Equal("09321-250", result.Endereco.CEP);
            }
        }

        [Fact]
        public async Task AtualizarClienteAsync_Atualiza_Os_Clientes_No_Banco_De_Dados()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ClienteContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    EnderecoId = 1,
                    ClienteId = 1,
                    Logradouro = "Rua Sofia Vitali",
                    Numero = "118",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-250"
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93",
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    EnderecoId = 2,
                    ClienteId = 2,
                    Logradouro = "Rua Aristides Lopes",
                    Numero = "61",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-100"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteNovo = new TbCliente()
                {
                    ClienteId = 1,
                    Nome = "Maria Eduarda",
                    CPF = "123.456.789-10",
                    Endereco = new TbEnderecoCliente()
                    {
                        EnderecoId = 1,
                        ClienteId = 1,
                        Logradouro = "Rua Estrada do Pilar",
                        Numero = "330",
                        Complemento = "Fazenda",
                        Cidade = "Ribeirão",
                        Bairro = "Jd Pilar",
                        UF = "SP",
                        CEP = "09433-485"
                    }
                };

                var clienteRepository = new ClienteRepository(context);

                //Act
                await clienteRepository.AtualizarClienteAsync(clienteNovo);

                //Assert
                var clienteAtualizado = await context.Clientes
                    .Include(x => x.Endereco)
                    .FirstOrDefaultAsync(x => x.ClienteId == clienteNovo.ClienteId);

                Assert.NotNull(clienteAtualizado);
                Assert.Equal(1, clienteAtualizado.ClienteId);
                Assert.Equal("Maria Eduarda", clienteAtualizado.Nome);
                Assert.Equal("123.456.789-10", clienteAtualizado.CPF);
                Assert.Equal("Rua Estrada do Pilar", clienteAtualizado.Endereco.Logradouro);
                Assert.Equal("330", clienteAtualizado.Endereco.Numero);
                Assert.Equal("Fazenda", clienteAtualizado.Endereco.Complemento);
                Assert.Equal("Ribeirão", clienteAtualizado.Endereco.Cidade);
                Assert.Equal("Jd Pilar", clienteAtualizado.Endereco.Bairro);
                Assert.Equal("SP", clienteAtualizado.Endereco.UF); 
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

            using (var context = new ClienteContext(options))
            {
                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 1,
                    Nome = "Renan Venturini",
                    CPF = "303.278.328.35",
               
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 1,
                    Logradouro = "Rua Sofia Vitali",
                    Numero = "118",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-250"
                });

                context.Clientes.Add(new TbCliente
                {
                    ClienteId = 2,
                    Nome = "Aline Santos",
                    CPF = "303.007.748-93"
                });

                context.Enderecos.Add(new TbEnderecoCliente
                {
                    ClienteId = 2,
                    Logradouro = "Rua Aristides Lopes",
                    Numero = "61",
                    Complemento = "Casa",
                    Cidade = "Mauá",
                    Bairro = "Jd Zaira",
                    UF = "SP",
                    CEP = "09321-100"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ClienteContext(options))
            {
                var clienteRepository = new ClienteRepository(context);

                //Act
                var id = await clienteRepository.ClientePorIdAsync(2);
                await clienteRepository.DeletarClienteAsync(id);

                //Assert
                var clienteDeletado = await context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == 2);

                Assert.Null(clienteDeletado);
            }
        }
    }
}
