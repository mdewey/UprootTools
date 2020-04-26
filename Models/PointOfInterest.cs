using System.Text.Json.Serialization;

namespace UprootTools.Models
{
  public class PointOfInterest
  {
    public int Id { get; set; }
    public int MoveId { get; set; }
    [JsonIgnore]
    public Move Move { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; }
    public string Name { get; set; }
  }
}