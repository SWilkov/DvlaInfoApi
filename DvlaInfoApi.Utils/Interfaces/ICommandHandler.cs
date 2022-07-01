namespace DvlaInfoApi.Utils.Interfaces
{
  public interface ICommandHandler<T> where T : class
  {
    void Execute(T command);
  }
}
