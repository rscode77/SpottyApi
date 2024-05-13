using Application.ApplicationUser;
using Application.Exceptions;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Password)
                .Custom(
                    (value, context) =>
                    {
                        if (value.Length < 6 || value.IsNullOrEmpty())
                            throw new BadRequestException(
                                "Password must be at least 6 characters."
                            );
                    }
                );

            RuleFor(x => x.Username)
                .Custom(
                    (value, context) =>
                    {
                        if (value.Length < 6 || value.IsNullOrEmpty())
                            throw new BadRequestException(
                                "Username must be at least 6 characters."
                            );
                    }
                );
        }
    }
}