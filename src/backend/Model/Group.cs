using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ListGenius.Model
{
    [Table("groups")]
    public class Group
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("image")]
        public byte[] Image { get; set; } = [];

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;
    }
}
