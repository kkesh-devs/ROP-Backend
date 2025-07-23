using System.Text.Json.Serialization;

namespace KKESH_ROP.DTO.TapPayment;

public class RetrieveChargeResDto
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
    public int Amount { get; set; }

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
    public Metadata Metadata { get; set; }

    [JsonPropertyName("order")]
    public object Order { get; set; }

    [JsonPropertyName("transaction")]
    public Transaction Transaction { get; set; }

    [JsonPropertyName("reference")]
    public Reference Reference { get; set; }

    [JsonPropertyName("response")]
    public ResponseInfo Response { get; set; }

    [JsonPropertyName("acquirer")]
    public Acquirer Acquirer { get; set; }

    [JsonPropertyName("receipt")]
    public Receipt Receipt { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; }

    [JsonPropertyName("merchant")]
    public Merchant Merchant { get; set; }

    [JsonPropertyName("source")]
    public Source Source { get; set; }

    [JsonPropertyName("redirect")]
    public Redirect Redirect { get; set; }

    [JsonPropertyName("post")]
    public Post Post { get; set; }

    [JsonPropertyName("activities")]
    public List<Activity> Activities { get; set; }

    [JsonPropertyName("auto_reversed")]
    public bool AutoReversed { get; set; }
}

public class Metadata
{
    [JsonPropertyName("udf1")]
    public string Udf1 { get; set; }
}

public class Transaction
{
    [JsonPropertyName("authorization_id")]
    public string AuthorizationId { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("expiry")]
    public Expiry Expiry { get; set; }

    [JsonPropertyName("asynchronous")]
    public bool Asynchronous { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("date")]
    public TransactionDate Date { get; set; }
}

public class Expiry
{
    [JsonPropertyName("period")]
    public int Period { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class TransactionDate
{
    [JsonPropertyName("created")]
    public long  Created { get; set; }

    [JsonPropertyName("transaction")]
    public long  Transaction { get; set; }
}

public class Reference
{
    [JsonPropertyName("track")]
    public string Track { get; set; }

    [JsonPropertyName("payment")]
    public string Payment { get; set; }

    [JsonPropertyName("transaction")]
    public string Transaction { get; set; }

    [JsonPropertyName("order")]
    public string Order { get; set; }

    [JsonPropertyName("acquirer")]
    public string Acquirer { get; set; }

    [JsonPropertyName("gateway")]
    public string Gateway { get; set; }
}

public class ResponseInfo
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

public class Acquirer
{
    [JsonPropertyName("response")]
    public ResponseInfo Response { get; set; }
}

public class Receipt
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("email")]
    public bool Email { get; set; }

    [JsonPropertyName("sms")]
    public bool Sms { get; set; }
}

public class Customer
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public Phone Phone { get; set; }
}

public class Phone
{
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; }
}

public class Merchant
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class Source
{
    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("payment_type")]
    public string PaymentType { get; set; }

    [JsonPropertyName("channel")]
    public string Channel { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("on_file")]
    public bool OnFile { get; set; }

    [JsonPropertyName("payment_method")]
    public string PaymentMethod { get; set; }
}

public class Redirect
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Post
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Activity
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
    public int Amount { get; set; }

    [JsonPropertyName("remarks")]
    public string Remarks { get; set; }

    [JsonPropertyName("txn_id")]
    public string TxnId { get; set; }
}