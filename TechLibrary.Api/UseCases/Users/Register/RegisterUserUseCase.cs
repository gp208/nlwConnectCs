using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Api.Infrastructure.Security.Cryptography;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        var dbContext = new TechLibraryDbContext();
        Validate(request, dbContext);

        var cryptography = new BCryptAlgorithm();
        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password)
        };

        dbContext.Users.Add(entity); // table name
        dbContext.SaveChanges();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name
        };
    }
    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        var existsUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email)); // table name

        if (existsUserWithEmail)
            result.Errors.Add(new ValidationFailure("Email", "Email já existe"));

        if (result.IsValid==false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
