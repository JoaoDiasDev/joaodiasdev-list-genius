using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ListGenius.Model
{
    [Table("sub_groups")]
    public class SubGroup
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("image")]
        public byte[] Image { get; set; } = [];

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("id_groups")]
        public Group IdGroups { get; set; } = new Group();
    }
}
