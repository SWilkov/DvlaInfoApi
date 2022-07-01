namespace DvlaInfoApi.Utils.Interfaces
{
  public interface ICommandResultHandlerAsync<T, U>
    where T : class
    where U : class, ICommonResult<U>
  {
    Task<U> ExecuteAsync(T command);
  }
}
