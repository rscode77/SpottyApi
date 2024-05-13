namespace Domain.Interfaces
{
	public interface IDataLoggerService
	{
		Task Log(Exception e, string message);
	}
}