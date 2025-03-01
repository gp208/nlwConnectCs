using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Checkouts;

public class RegisterBookCheckoutUseCase
{
    private const int MAX_LOAN_DAYS = 7;
    private readonly LoggedUserService _loggedUser;

    public RegisterBookCheckoutUseCase(LoggedUserService loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public void Execute(Guid bookId)
    {
        var dbContext = new TechLibraryDbContext();
        Validate(dbContext, bookId);
        var user = _loggedUser.GetUser(dbContext);
        dbContext.Checkouts.Add(new Domain.Entities.Checkout // table name
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS),
        });
        dbContext.SaveChanges();
    }

    private void Validate(TechLibraryDbContext dbContext, Guid bookId)
    {
        var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId); // table name
        if (book is null)
            throw new NotFoundException("Livro não encontrado");
        var amountBooksNotReturned =  dbContext
            .Checkouts // table name
            .Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);
        if (amountBooksNotReturned == book.Amount)
            throw new ConflictException("Livro não disponível");
    }
}
