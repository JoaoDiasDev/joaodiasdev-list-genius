﻿using ListGenius.Domain.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ListGenius.Domain.Model
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("value", TypeName = "decimal(18,2)")]
        public decimal Value { get; set; } = decimal.Zero;

        [Column("description")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column("qrcode", TypeName = "varbinary(max)")]
        public byte[] Qrcode { get; set; } = [];

        [Column("image", TypeName = "varbinary(max)")]
        public byte[] Image { get; set; } = [];

        [Column("link")]
        [MaxLength(500)]
        public string Link { get; set; } = string.Empty;

        [Required]
        [Column("enabled")]
        [DefaultValue(false)]
        public bool Enabled { get; set; } = true;

        [Column("unit")]
        [MaxLength(30)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("ProductsList")]
        [Column("id_products_list")]
        public long IdProductsList { get; set; }

        [Required]
        [ForeignKey("ProductGroup")]
        [Column("id_product_groups")]
        public long IdProductGroups { get; set; }

        [Required]
        [ForeignKey("ProductSubGroup")]
        [Column("id_product_sub_groups")]
        public long IdProductSubGroups { get; set; }

        [JsonIgnore]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ProductsList ProductsList { get; set; } = new ProductsList();

        [JsonIgnore]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ProductGroup ProductGroup { get; set; } = new ProductGroup();

        [JsonIgnore]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ProductSubGroup ProductSubGroup { get; set; } = new ProductSubGroup();


    }
}
