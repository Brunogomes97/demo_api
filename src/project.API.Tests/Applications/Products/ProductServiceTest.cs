public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repoMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repoMock = new Mock<IProductRepository>();
        _service = new ProductService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProduct_WhenDataIsValid()
    {
        var userId = Guid.NewGuid();

        var dto = new CreateProductDto
        {
            Name = "Notebook",
            Price = 5000,
            Category = "electronics",
            Description = "Dell XPS"
        };

        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<ProductEntity>()))
            .ReturnsAsync((ProductEntity p) => p);

        var result = await _service.CreateAsync(userId, dto);

        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Price, result.Price);
        Assert.Equal(dto.Category, result.Category);
        Assert.Equal(dto.Description, result.Description);
        Assert.Equal(userId, result.UserId);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<ProductEntity>()), Times.Once);
    }
    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenProductDoesNotExist()
    {
        _repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ProductEntity?)null);

        var dto = new UpdateProductDto
        {
            Name = "Updated name"
        };

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateAsync(Guid.NewGuid(), Guid.NewGuid(), dto)
        );
  }

    [Fact]
    public async Task UpdateAsync_ShouldThrowUnauthorized_WhenUserIsNotOwner()
    {
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Product",
            Price = 100,
            Category = "test",
            UserId = Guid.NewGuid() 
        };

        _repoMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var dto = new UpdateProductDto
        {
            Name = "Hacked"
        };

        await Assert.ThrowsAsync<UnauthorizedException>(() =>
            _service.UpdateAsync(Guid.NewGuid(), product.Id, dto)
        );
  }

    [Fact]
    public async Task DeleteAsync_ShouldDelete_WhenUserIsOwner()
    {
        var userId = Guid.NewGuid();
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId
        };

        _repoMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _repoMock
            .Setup(r => r.DeleteAsync(product))
            .Returns(Task.CompletedTask);

        await _service.DeleteAsync(userId, product.Id);

        _repoMock.Verify(r => r.DeleteAsync(product), Times.Once);
  }

    [Fact]
    public async Task GetByUserPagedAsync_ShouldThrow_WhenPaginationIsInvalid()
    {
        var query = new ProductPaginationQueryDto
        {
            Offset = -1,
            Limit = 10
        };

        await Assert.ThrowsAsync<InvalidPaginationException>(() =>
            _service.GetByUserPagedAsync(Guid.NewGuid(), query)
        );
    }
}
