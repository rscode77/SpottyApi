using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization
{
	public class ResourceOperationRequirement : IAuthorizationRequirement
	{
		public ResourceOperationRequirement(ResourceOperation resourceOperation)
		{
			ResourceOperation = resourceOperation;
		}

		public ResourceOperation ResourceOperation { get; set; }
	}
}