using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;
using Xunit;

namespace Test
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ListarProdutosOk()
        {
            // Arrange 
            var produtos = new List<Produto>() {
                new Produto()
                {
                    Id = 1,
                    Nome = "Produto A",
                    Preco = 10.0m
                }
            };
            _produtoRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);

            // Act
            var result = await _controller.GetProduto();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornaNotFound()
        {
            _produtoRepositoryMock.Setup(p => p.ListarProdutos()).ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProduto();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Produto B",
                Preco = 20.0m
            };
            _produtoRepositoryMock.Setup(p => p.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(produto);

            // Assert
            _produtoRepositoryMock
                .Verify(p => p.SalvarProduto(It.IsAny<Produto>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
