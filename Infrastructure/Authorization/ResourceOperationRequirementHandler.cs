using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirementHandler
        : AuthorizationHandler<ResourceOperationRequirement, object>
    {
        private readonly IUserContextService _userContextService;

        public ResourceOperationRequirementHandler(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement,
            object o
        )
        {
            if (
                requirement.ResourceOperation == ResourceOperation.Read
                || requirement.ResourceOperation == ResourceOperation.Create
            )
            {
                context.Succeed(requirement);
            }
            else if (IsUserCreator(o))
            {
                context.Succeed(requirement);
            }
            else if (IsUserInRole())
            {
                context.Succeed(requirement);
            }
            else if (IsUserActive(o))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

        private bool IsUserActive(object o)
        {
            if (o is User userObject)
            {
                string userConfirmed = userObject.UserConfirmed ? "1" : "0";
                return userConfirmed == _userContextService.GetUserConfirmation;
            }

            return false;
        }

        private bool IsUserCreator(object o)
        {
            if (o is User userObject)
            {
                return userObject.Id.ToString() == _userContextService.GetUserId;
            }

            return false;
        }

        private bool IsUserInRole()
        {
            Enum.TryParse(_userContextService.GetUserRole, out RoleEnum parsedEnum);

            return parsedEnum == RoleEnum.Admin ? false : true;
        }
    }
}