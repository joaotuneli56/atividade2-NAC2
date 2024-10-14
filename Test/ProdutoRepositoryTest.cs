using Moq;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;
using Xunit;

namespace Test
{
    public class ProdutoRepositoryTest
    {
        [Fact]
        public async Task ListarProdutos()
        {
            // Arrange
            var produtos = new List<Produto>() {
                new Produto()
                {
                    Id = 1,
                    Nome = "Produto A",
                    Preco = 10.0m
                },
                new Produto()
                {
                    Id = 2,
                    Nome = "Produto B",
                    Preco = 20.0m
                }
            };
            var produtoRepositoryMock = new Mock<IProdutoRepository>();
            produtoRepositoryMock.Setup(p => p.ListarProdutos()).ReturnsAsync(produtos);
            var produtoRepository = produtoRepositoryMock.Object;

            // Act 
            var result = await produtoRepository.ListarProdutos();

            // Assert
            Assert.Equal(produtos, result);
        }

        [Fact]
        public async Task SalvarProduto()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Produto C",
                Preco = 30.0m
            };
            var produtoRepositoryMock = new Mock<IProdutoRepository>();
            produtoRepositoryMock
                .Setup(p => p.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);
            var produtoRepository = produtoRepositoryMock.Object;

            // Act
            await produtoRepository.SalvarProduto(produto);

            // Assert
            produtoRepositoryMock
                .Verify(p => p.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }
}
