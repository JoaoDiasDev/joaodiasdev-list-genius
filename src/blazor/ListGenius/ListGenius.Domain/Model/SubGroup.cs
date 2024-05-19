using ListGenius.Domain.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListGenius.Domain.Model
{
    [Table("sub_groups")]
    public class SubGroup : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("image")]
        public byte[] Image { get; set; } = [];

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Column("id_groups")]
        public Group IdGroups { get; set; } = new Group();
    }
}
