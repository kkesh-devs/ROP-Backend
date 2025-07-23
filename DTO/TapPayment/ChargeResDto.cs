namespace KKESH_ROP.DTO.TapPayment;

using System.Text.Json.Serialization;

public class ChargeResDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("live_mode")]
    public bool LiveMode { get; set; }

    [JsonPropertyName("customer_initiated")]
    public bool CustomerInitiated { get; set; }

    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("threeDSecure")]
    public bool ThreeDSecure { get; set; }

    [JsonPropertyName("card_threeDSecure")]
    public bool CardThreeDSecure { get; set; }

    [JsonPropertyName("save_card")]
    public bool SaveCard { get; set; }

    [JsonPropertyName("product")]
    public string Product { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("metadata")]
    public MetadataDto Metadata { get; set; }

    [JsonPropertyName("order")]
    public object Order { get; set; } // Could be refined if you have the schema

    [JsonPropertyName("transaction")]
    public TransactionDto Transaction { get; set; }

    [JsonPropertyName("reference")]
    public ReferenceDto Reference { get; set; }

    [JsonPropertyName("response")]
    public ResponseStatusDto Response { get; set; }

    [JsonPropertyName("receipt")]
    public ReceiptDto Receipt { get; set; }

    [JsonPropertyName("customer")]
    public CustomerDto Customer { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantDto Merchant { get; set; }

    [JsonPropertyName("source")]
    public SourceDto Source { get; set; }

    [JsonPropertyName("redirect")]
    public UrlStatusDto Redirect { get; set; }

    [JsonPropertyName("post")]
    public UrlStatusDto Post { get; set; }

    [JsonPropertyName("activities")]
    public List<ActivityDto> Activities { get; set; }

    [JsonPropertyName("auto_reversed")]
    public bool AutoReversed { get; set; }
}


public class TransactionDto
{
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("expiry")]
    public ExpiryDto Expiry { get; set; }

    [JsonPropertyName("asynchronous")]
    public bool Asynchronous { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("date")]
    public TransactionDateDto Date { get; set; }
}

public class ExpiryDto
{
    [JsonPropertyName("period")]
    public int Period { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class TransactionDateDto
{
    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("transaction")]
    public long Transaction { get; set; }
}


public class ResponseStatusDto
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}


public class UrlStatusDto
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class ActivityDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("remarks")]
    public string Remarks { get; set; }

    [JsonPropertyName("txn_id")]
    public string TxnId { get; set; }
}
