﻿using Microsoft.EntityFrameworkCore;
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
        var query = dbContext.Books.AsQueryable(); // table name
        if (string.IsNullOrWhiteSpace(request.Title) == false)
            query = query.Where(book => EF.Functions.Like(book.Title, $"%{request.Title}%"));

        var books = query
            .OrderBy(book => book.Title).ThenBy(book => book.Author)
            .Skip((request.PageNumber - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToList();

        var totalCount = 0;
        if (string.IsNullOrWhiteSpace(request.Title))
            totalCount = dbContext.Books.Count(); // table name
        else // table name
            totalCount = dbContext.Books.Count(book => EF.Functions.Like(book.Title, $"%{request.Title}%"));

        return new ResponseBooksJson
            {
                Pagination = new ResponsePaginationJson
                {
                    PageNumber = request.PageNumber,
                    TotalCount = totalCount
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
