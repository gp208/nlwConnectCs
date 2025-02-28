using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Books.Filter;

public class FilterBookUseCase
{
    private const int PAGE_SIZE = 10;

    public ResponseBooksJson Execute(RequestFilterBooksJson request)
    {
        var dbContext = new TechLibraryDbContext();
        var books = dbContext
            .Books // table name
            .OrderBy(book => book.Title).ThenBy(book => book.Author)
            .Skip((request.PageNumber - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToList();
        return new ResponseBooksJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = dbContext.Books.Count() // table name
            },
            Books = books.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            }).ToList()
        };
    }
}
