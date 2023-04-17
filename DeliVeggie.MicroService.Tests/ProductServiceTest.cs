using DeliVeggie.MicroService.Abstract;
using DeliVeggie.Persistance.MongoDb.Abstract;
using DeliVeggie.Shared.Dto;
using Moq;

namespace DeliVeggie.MicroService.Tests;

public class ProductServiceTest
{

    private Mock<IProductRepository> _mockRepository = new Mock<IProductRepository>();

    [Theory]
    [InlineData(30, DayOfWeek.Sunday, 27)]
    [InlineData(30, DayOfWeek.Monday, 30)]
    [InlineData(30, DayOfWeek.Tuesday, 15)]
    [InlineData(30, DayOfWeek.Wednesday, 24)]
    [InlineData(30, DayOfWeek.Thursday, 21)]
    [InlineData(30, DayOfWeek.Friday, 30)]
    [InlineData(30, DayOfWeek.Saturday, 30)]
    public void CalculatePriceWithReduction_MatchPrice(double actualPrice, DayOfWeek dayOfWeek, double expectedPrice)
    {

        var product = GetProductDto(actualPrice);

        var productRepository = new ProductService(_mockRepository.Object);
        var priceWithReduction = productRepository.CalculatePriceWithReduction(product, (int)dayOfWeek);
        Assert.Equal(expectedPrice, priceWithReduction);
    }

    [Theory]
    [InlineData(30, DayOfWeek.Sunday, 20)]
    public void CalculatePriceWithReduction_DoesNotMatchPrice(double actualPrice, DayOfWeek dayOfWeek, double expectedPrice)
    {

        var product = GetProductDto(actualPrice);

        var productRepository = new ProductService(_mockRepository.Object);
        var priceWithReduction = productRepository.CalculatePriceWithReduction(product, (int)dayOfWeek);
        Assert.NotEqual(expectedPrice, priceWithReduction);
    }

    [Theory]
    [InlineData(30, DayOfWeek.Sunday, 30)]
    public void CalculatePriceWithReduction_EmptyPriceReduction_MatchPrice(double actualPrice, DayOfWeek dayOfWeek, double expectedPrice)
    {

        var product = GetProductDtoWithEmptyPriceReduction(actualPrice);

        var productRepository = new ProductService(_mockRepository.Object);
        var priceWithReduction = productRepository.CalculatePriceWithReduction(product, (int)dayOfWeek);
        Assert.Equal(expectedPrice, priceWithReduction);
    }


    private ProductDto GetProductDto(double price)
    {
        return new ProductDto()
        {
            Id = "1",
            Name = "Dummy",
            EntryDate = DateTime.UtcNow,
            Price = price,
            PriceReductions = new List<PriceReductionDto>()
            {
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Sunday,
                    Reduction = 0.1
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Monday,
                    Reduction = 0
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Tuesday,
                    Reduction = 0.5
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Wednesday,
                    Reduction = 0.2
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Thursday,
                    Reduction = 0.3
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Friday,
                    Reduction = 0
                },
                new PriceReductionDto()
                {
                    DayOfWeek = (int)DayOfWeek.Saturday,
                    Reduction = 0
                }
            }
        };
    }

    private ProductDto GetProductDtoWithEmptyPriceReduction(double price)
    {
        return new ProductDto()
        {
            Id = "1",
            Name = "Dummy",
            EntryDate = DateTime.UtcNow,
            Price = price
        };
    }

}