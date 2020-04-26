using System;

namespace UprootTools.Models
{
  public class Move
  {
    public int Id { get; set; }
    public string Destination { get; set; }
    public bool IsCurrent { get; set; }
    public DateTime TargetDate { get; set; }
  }
}