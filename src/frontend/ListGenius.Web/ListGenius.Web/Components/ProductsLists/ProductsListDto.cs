namespace ListGenius.Web.Components.ProductsLists;

public class ProductsListDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [DisplayName("Descrição")]
    [MaxLength(200)]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("enabled")]
    [DisplayName("Ativo")]
    public bool Enabled { get; init; } = true;

    [JsonPropertyName("image")]
    [DisplayName("Imagem")]
    [MaxLength(500)]
    public byte[] Image { get; init; } = [];

    [JsonPropertyName("public")]
    [DisplayName("Público")]
    public bool Public { get; init; } = false;

    [JsonPropertyName("external_link")]
    [DisplayName("Link Externo")]
    [MaxLength(500)]
    public string ExternalLink { get; init; } = string.Empty;

    [JsonPropertyName("created_date")]
    [DisplayName("Data Criação")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "A data de criação é obrigatória")]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; init; } = DateTime.Now;

    [JsonPropertyName("updated_date")]
    [DisplayName("Data Atualizado")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "A data de atualização é obrigatória")]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedDate { get; init; } = DateTime.Now;

    [JsonPropertyName("username")]
    [DisplayName("Nome Usuário")]
    public string UserName { get; init; } = string.Empty;
}
