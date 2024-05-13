namespace Domain.Interfaces
{
	public interface IDataLogger
	{
		Task Log(Exception e, string message);
	}
}