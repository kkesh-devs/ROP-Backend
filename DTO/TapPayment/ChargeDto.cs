using System.Text.Json.Serialization;

namespace KKESH_ROP.DTO.TapPayment;

public class MetadataDto
{
    [JsonPropertyName("udf1")]
    public string Udf1 { get; set; }
}

public class ReceiptDto
{
    [JsonPropertyName("email")]
    public bool Email { get; set; }

    [JsonPropertyName("sms")]
    public bool Sms { get; set; }
}

public class ReferenceDto
{
    [JsonPropertyName("transaction")]
    public string Transaction { get; set; }

    [JsonPropertyName("order")]
    public string Order { get; set; }
}

public class CustomerDto
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public PhoneDto Phone { get; set; }
}

public class PhoneDto
{
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; }
}

public class MerchantDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class SourceDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}