using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Produto.API.Data;
using Produto.API.Data.Mappings.Profiles;
using Produto.API.Data.Repository;
using Produto.API.Data.Table;
using Produto.API.Models.Request;
using Produto.API.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Produto.API.Tests.Services
{
    public class ProdutoServicesTests
    {
        [Fact]
        public async Task AdicionaProdutoAsync_OK_Adicionar_Produto()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());

            });



            using (var context = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                var produtoRequest = new ProdutoRequest()
                {
                    Nome = "Pirulito",
                    Preco = 1.50M
                };

                //Act
                await produtoService.AdicionarProdutoAsync(produtoRequest);

                //Assert
                var produtoAdicionado = await context.Produtos.FirstOrDefaultAsync(x => x.Nome == "Pirulito");

                Assert.NotNull(produtoAdicionado);
                Assert.Equal(1.50M, produtoAdicionado.Preco);
                Assert.Equal("Pirulito", produtoAdicionado.Nome);
            }
        }

        [Fact]
        public async Task ListaProdutoAsync_Traz_Os_Produtos_Do_Banco()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());
            });

            using (var context = new ProdutoContext(options))
            {

                context.Produtos.Add(new TbProduto
                {
                    Id = 1,
                    Nome = "Bala",
                    Preco = 2.50M
                });

                context.Produtos.Add(new TbProduto
                {
                    Id = 2,
                    Nome = "Doce",
                    Preco = 3.50M
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ProdutoContext(options))
            {

                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                //Act
                var result = await produtoService.ListarProdutoAsync();

                var produto = result.FirstOrDefault(x => x.Id == 2);

                Assert.NotNull(result);
                Assert.True(result.Any());
                Assert.Equal(2, result.Count());
                Assert.NotNull(produto);
                Assert.Equal(2, produto.Id);
                Assert.Equal("Doce", produto.Identificacao);
                Assert.Equal(3.50M, produto.Preco);
            }

        }

        [Fact]
        public async Task AtualizaProdutoAsync_Atualiza_Produto_No_Banco()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());
            });

            using (var context = new ProdutoContext(options))
            {

                context.Produtos.Add(new TbProduto
                {
                    Id = 1,
                    Nome = "Bala",
                    Preco = 2.50M
                });

                context.Produtos.Add(new TbProduto
                {
                    Id = 2,
                    Nome = "Doce",
                    Preco = 3.50M
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                var produtoNovo = new AtualizaProdutoRequest();
                produtoNovo.Id = 1;
                produtoNovo.Nome = "Bolo";
                produtoNovo.Preco = 7.50M;

                //Act
                await produtoService.AtualizarProdutoAsync(produtoNovo);

                var produtoAtualizado = await context.Produtos.FirstOrDefaultAsync(x => x.Id == produtoNovo.Id);

                //Assert
                Assert.NotNull(produtoAtualizado);
                Assert.Equal(1, produtoAtualizado.Id);
                Assert.Equal("Bolo", produtoAtualizado.Nome);
                Assert.Equal(7.50M, produtoAtualizado.Preco);
            }
        }

        [Fact]
        public async Task ProdutoPorIdAsync_Busca_Um_Produto_Especifico_Por_Id()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());
            });

            using (var context = new ProdutoContext(options))
            {

                context.Produtos.Add(new TbProduto
                {
                    Id = 1,
                    Nome = "Bala",
                    Preco = 2.50M
                });

                context.Produtos.Add(new TbProduto
                {
                    Id = 2,
                    Nome = "Doce",
                    Preco = 3.50M
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                //Act

                var produtoComId = await produtoService.ProdutoPorIdAsync(2);

                //Assert

                Assert.NotNull(produtoComId);
                Assert.Equal("Doce", produtoComId.Identificacao);
                Assert.Equal(3.50M, produtoComId.Preco);
            }
        }

        [Fact]
        public async Task DeletarProduto_Exclui_Produto_Do_Banco()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());
            });

            using (var context = new ProdutoContext(options))
            {

                context.Produtos.Add(new TbProduto
                {
                    Id = 1,
                    Nome = "Bala",
                    Preco = 2.50M
                });

                context.Produtos.Add(new TbProduto
                {
                    Id = 2,
                    Nome = "Doce",
                    Preco = 3.50M
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                //Act

                await produtoService.DeletarProduto(2);

                var produtoDeletado = await context.Produtos.FirstOrDefaultAsync(x => x.Id == 2);

                //Assert

                Assert.Null(produtoDeletado);
            }
        }
    }
}
