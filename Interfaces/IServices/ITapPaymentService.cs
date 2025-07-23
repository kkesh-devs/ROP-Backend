using KKESH_ROP.DTO.TapPayment;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IServices;

public interface ITapPaymentService
{
    Task<Response<string>> Charge(ChargeReqDto dto);
    Task<Response<string>> RetrieveCharge(string chargeId);
}