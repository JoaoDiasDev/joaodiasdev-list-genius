using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class SanctionType : GenericBaseEntity
{
    //Many-to-one relationship with Sanction
    public List<Sanction>? Sanctions { get; set; }
}