namespace DvlaInfoApi.Utils.Interfaces
{
  public interface ICommandResultHandler<T, U> 
    where T : class
    where U : class, ICommonResult<U>
  {
    U Execute(T command);
  }
}
