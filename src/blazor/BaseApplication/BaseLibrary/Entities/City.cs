using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class City : GenericBaseEntity
{
    //Relationship: Many To One with Country
    public int CountryId { get; set; }
    public Country? Country { get; set; }
    //Relationship: One To Many with Town
    [JsonIgnore]
    public List<Town>? Towns { get; set; }
}