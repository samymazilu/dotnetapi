using MyAPI.Entities;
using MyAPI.Exceptions;

namespace MyAPI.Services;

public interface ITransactionService
{
    Task<Transaction> ProcessTransaction(Transaction transaction);
}

public class TransactionService : ITransactionService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly ITransactionRepository _transactionRepository;


    public TransactionService(IArticleRepository articleRepository, IPaymentRepository paymentRepository, ITransactionRepository transactionRepository)
    {
        _articleRepository = articleRepository;
        _paymentRepository = paymentRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> ProcessTransaction(Transaction transaction)
    {
        List<Article> dbArticles = new List<Article>();
        foreach (var article in transaction.Articles)
        {
            var dbArticle = await _articleRepository.GetByIdAsync(article.ArticleId);
            if (dbArticle == null)
            {
                throw new ItemNotFoundException<Article>(article.ArticleId);
            }
            if (dbArticle.Quantity < 1)
            {
                throw new ArticleNotInStockException(article.ArticleId);
            }
            dbArticles.Add(dbArticle);
        }
        List<Payment> dbPayments = new List<Payment>();
        foreach (var payment in transaction.Payments)
        {
            var dbPayment = await _paymentRepository.GetByIdAsync(payment.PaymentId);
            if (dbPayment == null)
            {
                throw new ItemNotFoundException<Payment>(payment.PaymentId);
            }
            if (dbPayment.Status != PaymentStatus.Pending)
            {
                throw new PaymentProcessingError(payment.PaymentId);
            }
            dbPayments.Add(dbPayment);
        }
        var totalItemsSum = dbArticles.Sum(x => x.Price);
        var totalPaymentSum = dbPayments.Sum(x => x.Amount);
        if (totalItemsSum != totalPaymentSum)
        {
            throw new SumMismatchError(transaction.TransactionId);
        }
        foreach (var dbArticle in dbArticles)
        {
            dbArticle.Quantity--;
        }
        foreach (var dbPayment in dbPayments)
        {
            dbPayment.Status = PaymentStatus.Processed;
        }
         transaction.Articles = dbArticles;
         transaction.Payments = dbPayments;

        _transactionRepository.Add(transaction);
        return transaction;
    }
}
