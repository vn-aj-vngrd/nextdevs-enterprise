using Backend.Application.Helpers;
using Backend.Application.Interfaces;
using FluentValidation;

namespace Backend.Application.DTOs.Account.Requests;

public class ChangeUserNameRequest
{
    public string UserName { get; set; }
}

public class ChangeUserNameRequestValidator : AbstractValidator<ChangeUserNameRequest>
{
    public ChangeUserNameRequestValidator(ITranslator translator)
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(4)
            .Matches(Regexs.UserName)
            .WithName(p => translator[nameof(p.UserName)]);
    }
}