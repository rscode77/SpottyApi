using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Helpers
{
	public class FileDataLogger : IDataLogger
	{
		private readonly ILogger _logger;

		public FileDataLogger(ILogger logger)
		{
			_logger = logger;
		}

		public async Task Log(Exception e, string message) => _logger.LogError(e, message);
	}
}