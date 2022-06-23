using DvlaInfoApi.Framework.Enums;

namespace DvlaInfoApi.Framework.Models
{
  public class SearchQuery
  {
    public string Input { get; private set; }
    public SearchType SearchType { get; private set; }
    public SearchQuery(string input, SearchType searchType)
    {
      this.Input = input;
      this.SearchType = searchType;
    }
  }
}
