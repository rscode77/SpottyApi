using Domain.Interfaces;

namespace Application.Services
{
    public class DataLoggerService : IDataLoggerService
    {
        private readonly IEnumerable<IDataLogger> _dataLoggers;

        public DataLoggerService(IEnumerable<IDataLogger> dataLoggers)
        {
            _dataLoggers = dataLoggers;
        }

        public async Task Log(Exception e, string message)
        {
            foreach (var dataLogger in _dataLoggers)
            {
                await dataLogger.Log(e, message);
            }
        }
    }
}
