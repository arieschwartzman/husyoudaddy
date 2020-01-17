using System;
using Microsoft.Azure.Cosmos.Table;

namespace Husyoudaddy.Data
{
  public class Scenario : TableEntity
  {
    public string name { get; set; }
  }
}
