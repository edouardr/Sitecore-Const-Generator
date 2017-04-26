namespace Sitecore.Helix.ConstGenerator.Core.Entities
{
  using Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities;

  public class RequestResult : IWebApiRequestResult<Result, Item>
  {
    public int StatusCode { get; set; }

    public Result Result { get; set; }
  }
}