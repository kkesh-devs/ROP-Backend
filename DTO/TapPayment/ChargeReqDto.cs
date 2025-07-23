namespace KKESH_ROP.DTO.TapPayment;

using System.Text.Json.Serialization;

public class ChargeReqDto
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("customer_initiated")]
    public bool CustomerInitiated { get; set; }

    [JsonPropertyName("threeDSecure")]
    public bool ThreeDSecure { get; set; }

    [JsonPropertyName("save_card")]
    public bool SaveCard { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("metadata")]
    public MetadataDto Metadata { get; set; }

    [JsonPropertyName("receipt")]
    public ReceiptDto Receipt { get; set; }

    [JsonPropertyName("reference")]
    public ReferenceDto Reference { get; set; }

    [JsonPropertyName("customer")]
    public CustomerDto Customer { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantDto Merchant { get; set; }

    [JsonPropertyName("source")]
    public SourceDto Source { get; set; }

    [JsonPropertyName("post")]
    public UrlDto Post { get; set; }

    [JsonPropertyName("redirect")]
    public UrlDto Redirect { get; set; }
}



public class UrlDto
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}

