using FluentValidation;

namespace Business.Core.Contracts;

public class AuthLoginRequest
{
    public string? code { get; set; }
    public string? password { get; set; }
    
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.code).NotEmpty();
        }
    }
}