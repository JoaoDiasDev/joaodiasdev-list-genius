using ListGenius.Api.Entities.ProductGroups;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ListGenius.Api.Entities.ProductSubGroups;

public class ProductSubGroupDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    [DisplayName("Imagem")]
    [MaxLength(500)]
    public byte[] Image { get; set; } = [];

    [JsonPropertyName("description")]
    [DisplayName("Descrição")]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

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

    [JsonPropertyName("group_name")]
    [DisplayName("Nome Grupo")]
    public string GroupName { get; set; } = string.Empty;
    public ProductGroup ProductGroup { get; set; } = new ProductGroup();


}
