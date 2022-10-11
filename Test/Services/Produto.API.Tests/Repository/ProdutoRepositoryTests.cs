using Microsoft.EntityFrameworkCore;
using Produto.API.Data;
using Produto.API.Data.Repository;
using Produto.API.Data.Table;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Produto.API.Tests.Repository
{
    public class ProdutoRepositoryTests
    {
        [Fact]
        public async Task AdicionaProdutoAsync_OK_Adicionar_Produto()
        {
            //Arrange

            var produto = new TbProduto();
            produto.Nome = "Bala";
            produto.Preco = 5;

            var options = new DbContextOptionsBuilder<ProdutoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new ProdutoContext(options))
            {
                var produtoRepository = new ProdutoRepository(context);

                //Act
                await produtoRepository.AdicionarProdutoAsync(produto);

                //Assert
                var produtoAdicionado = await context.Produtos.FirstOrDefaultAsync(x => x.Nome == "Bala");

                Assert.NotNull(produtoAdicionado);
                Assert.Equal(5, produtoAdicionado.Preco);
                Assert.Equal("Bala", produtoAdicionado.Nome);
            }
        }

        [Fact]
        public async Task ListaProdutoAsync_Traz_Os_Produtos_Do_Banco()
        {
            //Arrenge

            var options = new DbContextOptionsBuilder<ProdutoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

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
                var produtoRepository = new ProdutoRepository(context);

                //Act

                var result = await produtoRepository.ListarProdutoAsync();

                //Assert
                var produto = result.FirstOrDefault(x => x.Id == 1);

                Assert.NotNull(result);
                Assert.True(result.Any());
                Assert.Equal(2, result.Count());
                Assert.NotNull(produto);
                Assert.Equal(1, produto.Id);
                Assert.Equal("Bala", produto.Nome);
                Assert.Equal(2.50M, produto.Preco);
            }
        }

        [Fact]
        public async Task AtualizaProdutoAsync_Atualiza_Produto_No_Banco()
        {
            var options = new DbContextOptionsBuilder<ProdutoContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

            //Arrange
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

                var produtoNovo = new TbProduto();
                produtoNovo.Id = 1;
                produtoNovo.Nome = "Bolo";
                produtoNovo.Preco = 7.50M;

                var produtoRepository = new ProdutoRepository(context);

                //Act

                await produtoRepository.AtualizarProdutoAsync(produtoNovo);

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
            var options = new DbContextOptionsBuilder<ProdutoContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

            //Arrange
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
                var produtoRepository = new ProdutoRepository(context);

                //Act
                var produtoComId = await produtoRepository.ProdutoPorIdAsync(2);

                //Assert

                Assert.NotNull(produtoComId);
                Assert.Equal("Doce", produtoComId.Nome);
                Assert.Equal(3.50M, produtoComId.Preco);
            }
        }

        [Fact]
        public async Task DeletarProduto_Exclui_Produto_Do_Banco()
        {
            var options = new DbContextOptionsBuilder<ProdutoContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

            //Arrange
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

                var produtoRepository = new ProdutoRepository(context);

                //Act
                await produtoRepository.DeletarProduto(2);

                var produtoDeletado = await context.Produtos.FirstOrDefaultAsync(x => x.Id == 2);

                //Assert

                Assert.Null(produtoDeletado);
            }
        }
    }
}
