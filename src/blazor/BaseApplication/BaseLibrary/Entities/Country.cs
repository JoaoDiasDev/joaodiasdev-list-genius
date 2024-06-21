using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Country : GenericBaseEntity
{
    //One-to-many relationship with City
    [JsonIgnore]
    public List<City>? Cities { get; set; }

}