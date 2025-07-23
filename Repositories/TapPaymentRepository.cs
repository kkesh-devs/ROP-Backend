using System.Text.Json;
using KKESH_ROP.DTO.TapPayment;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Interfaces.IServices;

namespace KKESH_ROP.Repositories;

public class TapPaymentRepository (ITapPaymentService  tapPaymentService) : ITapPaymentRepository
{
    
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ChargeResDto>> Charge(ChargeReqDto dto)
    {

        try
        {
            var response = await tapPaymentService.Charge(dto);
            var jsonResponse = JsonSerializer.Deserialize<ChargeResDto>(response.Data);
            return new Response<ChargeResDto>(true, "Data retrieved successfully", jsonResponse);
        }
        catch (Exception exception)
        {
            return new Response<ChargeResDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<RetrieveChargeResDto>> RetrieveCharge(string chargeId)
    {
        try
        {
            var response = await tapPaymentService.RetrieveCharge(chargeId);
            var jsonResponse = JsonSerializer.Deserialize<RetrieveChargeResDto>(response.Data);
            
            return new Response<RetrieveChargeResDto>(true, "Data retrieved successfully", jsonResponse);
        }
        catch (Exception exception)
        {
            return new Response<RetrieveChargeResDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}