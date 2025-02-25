using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Nome é obrigatório");
        RuleFor(request => request.Email).EmailAddress().WithMessage("Email inválido");
        RuleFor(request => request.Password).NotEmpty().WithMessage("Senha é obrigatória");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Senha deve ter pelo menos 6 caracteres");
        });
    }
}
