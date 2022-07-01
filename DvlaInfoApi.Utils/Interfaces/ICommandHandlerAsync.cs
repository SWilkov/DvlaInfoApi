namespace DvlaInfoApi.Utils.Interfaces
{
  public interface ICommandHandlerAsync<T> where T : class
  {
    Task ExecuteAsync(T command);
  }
}
