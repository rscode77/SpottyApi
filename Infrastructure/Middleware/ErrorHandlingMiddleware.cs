using Application.Exceptions;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Middleware
{
	public class ErrorHandlingMiddleware : IMiddleware
	{
		private readonly IDataLoggerService _loggers;

		public ErrorHandlingMiddleware(IDataLoggerService loggers)
		{
			_loggers = loggers;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (BadRequestException badRequestException)
			{
				context.Response.StatusCode = 400;
				context.Response.ContentType = "application/json";

				var response = new ServiceResponse<string> { Message = badRequestException.Message, Success = false };

				await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
			}
			catch (ForbiddenException forbiddenException)
			{
				context.Response.StatusCode = 403;
				context.Response.ContentType = "application/json";

				var response = new ServiceResponse<string> { Message = forbiddenException.Message, Success = false };

				await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
			}
			catch (NotFoundException notFoundException)
			{
				context.Response.StatusCode = 404;
				context.Response.ContentType = "application/json";

				var response = new ServiceResponse<string> { Message = notFoundException.Message, Success = false };

				await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
			}
			catch (Exception e)
			{
				await _loggers.Log(e, e.Message);

				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";

				var response = new ServiceResponse<string> { Message = e.Message, Success = false };

				await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
			}
		}
	}
}