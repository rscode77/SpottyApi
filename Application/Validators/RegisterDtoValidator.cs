using Application.Exceptions;
using FluentValidation;
using Infrastructure.Persistance;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(SpottyDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .Custom(
                    (value, context) =>
                    {
                        if (!value.Contains("@") || !value.Contains("."))
                            throw new BadRequestException("Wrong email address.");

                        if (value.Length < 6 || value.IsNullOrEmpty())
                            throw new BadRequestException("Email must be at least 6 characters.");

                        var emailInUse = dbContext.Users.Any(x => x.Email == value);
                        if (emailInUse)
                            throw new BadRequestException("That email is taken.");
                    }
                );

            RuleFor(x => x.Password)
                .Custom(
                    (value, context) =>
                    {
                        if (value.Length < 6 || value.IsNullOrEmpty())
                            throw new BadRequestException("Username must be at least 6 characters.");
                    }
                );

            RuleFor(x => x.Username)
                .Custom(
                    (value, context) =>
                    {
                        if (value.Length < 6 || value.IsNullOrEmpty())
                            throw new BadRequestException("Username must be at least 6 characters.");

                        var usernameInUse = dbContext.Users.Any(x => x.Username == value);
                        if (usernameInUse)
                            throw new BadRequestException("That username is taken.");
                    }
                );
        }
    }
}