namespace DvlaInfoApi.Utils.Interfaces
{
  public interface ICommonResult<T>
  {
    T Data { get; }
    int Rows { get; }
    string Message { get; }
    bool Success { get; }
  }
}
