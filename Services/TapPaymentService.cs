using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using KKESH_ROP.Configuration;
using KKESH_ROP.DTO.TapPayment;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IServices;
using Microsoft.Extensions.Options;

namespace KKESH_ROP.Services;

public class TapPaymentService(HttpClient httpClient, IOptions<TapPaymentSettings> options) : ITapPaymentService
{
    private readonly TapPaymentSettings settings = options.Value;


//____________________________________________________________________________________________________________________________________________________
    public async Task<Response<string>> Charge(ChargeReqDto dto)
    {
        try
        {
            var url = $"{settings.Url}/charges/";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"{settings.ProductionKey}");
            request.Headers.Add("Accept", "application/json");

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Response<string>(false, $"Tap API Error: {content}", null);
            }

            return new Response<string>(true, "Charge initiated successfully", content);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, $"Exception: {ex.Message}", null);
        }
    }

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<string>> RetrieveCharge(string chargeId)
    {
        try
        {
            var url = $"{settings.Url}/charges/{chargeId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"{settings.ProductionKey}");
            request.Headers.Add("Accept", "application/json");

            var response = await httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Response<string>(false, $"Tap API Error: {content}", null);
            }

            return new Response<string>(true, "Charge initiated successfully", content);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, $"Exception: {ex.Message}", null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}