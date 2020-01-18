using System;
using Microsoft.Azure.Cosmos.Table;

namespace Husyoudaddy.Data
{
  public class Scenario : TableEntity
  {
    public string name { get; set; }
    public string scenario_trigger { get; set; }
    public DateTime updated { get; set; }
    public string description { get; set; }

  }
}
