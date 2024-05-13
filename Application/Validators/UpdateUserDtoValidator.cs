using Application.ApplicationUser;
using FluentValidation;
using Infrastructure.Persistance;

namespace Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(SpottyDbContext dbContext)
        {
            RuleFor(x => x.Username)
                .MinimumLength(6);

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.Email)
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "That email is taken");
                });
        }
    }
}