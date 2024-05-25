﻿using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.ProductSubGroups;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ListGenius.Api.Entities.Products;

public class ProductDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    [DisplayName("Preço")]
    [Required(ErrorMessage = "O preço é obrigatório")]
    [Column(TypeName = "decimal(10,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    public decimal Value { get; set; } = decimal.Zero;

    [JsonPropertyName("description")]
    [DisplayName("Descrição")]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("qrcode")]
    [DisplayName("QrCode")]
    [MaxLength(500)]
    public byte[] Qrcode { get; set; } = [];

    [JsonPropertyName("image")]
    [DisplayName("Imagem")]
    [MaxLength(500)]
    public byte[] Image { get; set; } = [];

    [JsonPropertyName("link")]
    [DisplayName("Link")]
    [MaxLength(500)]
    public string Link { get; set; } = string.Empty;

    [JsonPropertyName("enabled")]
    [DisplayName("Ativo")]
    public bool Enabled { get; set; } = true;

    [JsonPropertyName("created_date")]
    [DisplayName("Data Criação")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "A data de criação é obrigatória")]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [JsonPropertyName("updated_date")]
    [DisplayName("Data Atualizado")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "A data de atualização é obrigatória")]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    [JsonPropertyName("unit")]
    [DisplayName("Unidade")]
    [Required(ErrorMessage = "A unidade é obrigatória")]
    [MinLength(1)]
    [MaxLength(20)]
    public string Unit { get; set; } = string.Empty;

    [DisplayName("Nome Shopping List")]
    public string ShoppingListName { get; set; } = string.Empty;

    [DisplayName("Nome Grupo")]
    public string GroupName { get; set; } = string.Empty;

    [DisplayName("Nome Sub Grupo")]
    public string SubGroupName { get; set; } = string.Empty;

    public ProductsList ProductsList { get; set; } = new ProductsList();

    public ProductGroup ProductGroup { get; set; } = new ProductGroup();

    public ProductSubGroup ProductSubGroup { get; set; } = new ProductSubGroup();

}
