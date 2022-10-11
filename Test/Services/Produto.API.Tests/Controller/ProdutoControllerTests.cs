using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produto.API.Controllers;
using Produto.API.Data;
using Produto.API.Data.Mappings.Profiles;
using Produto.API.Data.Repository;
using Produto.API.Data.Table;
using Produto.API.Models.Request;
using Produto.API.Models.Response;
using Produto.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Produto.API.Tests.Controller
{
    public class ProdutoControllerTests
    {
        [Fact]
        public async Task AdicionaProdutoAsync_OK_Adicionar_Produto()
        {
            var options = new DbContextOptionsBuilder<ProdutoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoProfile());

            });

            using (var context = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(context), new Mapper(config));

                var produtoController = new ProdutoController(produtoService);

                var produtoRequest = new ProdutoRequest()
                {
                    Nome = "Pirulito",
                    Preco = 1.50M
                };

                //Act
                await produtoController.AdicionarProdutoAsync(produtoRequest);

                var produtoAdicionado = await context.Produtos.FirstOrDefaultAsync(x => x.Nome == "Pirulito");

                Assert.NotNull(produtoAdicionado);
                Assert.Equal("Pirulito", produtoAdicionado.Nome);
                Assert.Equal(1.50M, produtoAdicionado.Preco);
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

                var produtoController = new ProdutoController(produtoService);

                //Act
                var result = await produtoController.ListarProdutoAsync();

                var okResult = result as ObjectResult;

                var produtos = okResult.Value as IEnumerable<ProdutoResponse>;

                //Assert
                var produto = produtos.FirstOrDefault(x => x.Identificacao == "Doce");

                Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
                Assert.NotNull(produtos);
                Assert.True(produtos.Any());
                Assert.Equal(2, produtos.Count());
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

                var produtoController = new ProdutoController(produtoService);

                var produtoNovo = new AtualizaProdutoRequest()
                {
                    Id = 1,
                    Nome = "Suspiro",
                    Preco = 3.65M
                };

                //Act
                await produtoController.AtualizaProdutoAsync(produtoNovo);

                var produtoAtualizado = await context.Produtos.FirstOrDefaultAsync(x => x.Nome == "Suspiro");

                //Assert
                Assert.NotNull(produtoAtualizado);
                Assert.Equal(1, produtoAtualizado.Id);
                Assert.Equal("Suspiro", produtoAtualizado.Nome);
                Assert.Equal(3.65M, produtoAtualizado.Preco);

            }
        }

        [Theory]
        [InlineData(1)]
        public async Task ProdutoPorIdAsync_Busca_Um_Produto_Especifico_Por_Id(int id)
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

            using (var contex = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(contex), new Mapper(config));

                var produtoController = new ProdutoController(produtoService);

                //Act
                var result = await produtoController.ProdutoPorIdAsync(id);

                var okResult = result as ObjectResult;

                var produtos = okResult.Value as ProdutoResponse;

                //Assert
                Assert.NotNull(produtos);
                Assert.Equal(1, produtos.Id);
                Assert.Equal("Bala", produtos.Identificacao);
                Assert.Equal(2.50M, produtos.Preco);
            }
        }

        [Theory]
        [InlineData(0, 400)]
        [InlineData(25, 404)]
        public async Task ProdutoPorIdAsync_Retornar_Exception_Por_Id(int id, int expected)
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

            using (var contex = new ProdutoContext(options))
            {
                var produtoService = new ProdutoService(new ProdutoRepository(contex), new Mapper(config));

                var produtoController = new ProdutoController(produtoService);

                //Act
                var result = await produtoController.ProdutoPorIdAsync(id);

                var okResult = result as StatusCodeResult;

                //Assert
                Assert.Equal(expected, okResult.StatusCode);

            }
        }
    }
}
