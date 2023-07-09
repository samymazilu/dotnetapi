using Moq;
using MyAPI.Entities;
using MyAPI.Exceptions;
using MyAPI.Services;
using NUnit.Framework;
namespace MyAPI.Tests;
[TestFixture]
public class TransactionServiceTests
{
    private Mock<IArticleRepository> _mockArticleRepository;
    private Mock<IPaymentRepository> _mockPaymentRepository;
    private Mock<ITransactionRepository> _mockTransactionRepository;
    private Mock<MyDbContext> _mockDbContext;
    private TransactionService _service;

    [SetUp]
    public void SetUp()
    {
        _mockArticleRepository = new Mock<IArticleRepository>();

        _mockPaymentRepository = new Mock<IPaymentRepository>();
       _mockTransactionRepository = new Mock<ITransactionRepository>();

        _service = new TransactionService(_mockArticleRepository.Object, _mockPaymentRepository.Object, _mockTransactionRepository.Object);
    }

    [Test]
    public async Task ProcessTransaction_ArticleNotFound_ThrowsException()
    {
        // Arrange
        var transaction = new Transaction
        {
            Articles = new List<Article> { new Article { ArticleId = 1 } }
        };

        _mockArticleRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Article)null);

        // Act & Assert
        Assert.ThrowsAsync<ItemNotFoundException<Article>>(async () => await _service.ProcessTransaction(transaction));
    }

    [Test]
    public async Task ProcessTransaction_ArticleNotInStock_ThrowsException()
    {
        // Arrange
        var transaction = new Transaction
        {
            Articles = new List<Article> { new Article { ArticleId = 1 } }
        };

        _mockArticleRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Article { Quantity = 0 });

        // Act & Assert
        Assert.ThrowsAsync<ArticleNotInStockException>(async () => await _service.ProcessTransaction(transaction));
    }

    [Test]
    public async Task ProcessTransaction_PaymentNotFound_ThrowsException()
    {
        // Arrange
        var transaction = new Transaction
        {
            Articles = new List<Article> { new Article { ArticleId = 1 } },
            Payments = new List<Payment> { new Payment { PaymentId = 1 } }
        };

        _mockArticleRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Article { Quantity = 1 });
        _mockPaymentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Payment)null);

        // Act & Assert
        Assert.ThrowsAsync<ItemNotFoundException<Payment>>(async () => await _service.ProcessTransaction(transaction));
    }

    [Test]
    public async Task ProcessTransaction_PaymentNotPending_ThrowsException()
    {
        // Arrange
        var transaction = new Transaction
        {
            Articles = new List<Article> { new Article { ArticleId = 1 } },
            Payments = new List<Payment> { new Payment { PaymentId = 1 } }
        };

        _mockArticleRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Article { Quantity = 1 });
        _mockPaymentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Payment { Status = PaymentStatus.Processed });

        // Act & Assert
        Assert.ThrowsAsync<PaymentProcessingError>(async () => await _service.ProcessTransaction(transaction));
    }

    [Test]
    public async Task ProcessTransaction_SumsMismatch_ThrowsException()
    {
        // Arrange
        var transaction = new Transaction
        {
            Articles = new List<Article> { new Article { ArticleId = 1, Price = 100 } },
            Payments = new List<Payment> { new Payment { PaymentId = 1, Amount = 50 } }
        };

        _mockArticleRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Article {ArticleId =1,  Quantity = 1, Price = 100 });
        _mockPaymentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Payment {PaymentId =1,  Status = PaymentStatus.Pending,  Amount = 50 });

        // Act & Assert
        Assert.ThrowsAsync<SumMismatchError>(async () => await _service.ProcessTransaction(transaction));
    }
}
