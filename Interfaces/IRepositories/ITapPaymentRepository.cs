using KKESH_ROP.DTO.TapPayment;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface ITapPaymentRepository
{
    Task<Response<ChargeResDto>> Charge(ChargeReqDto dto);
    Task<Response<RetrieveChargeResDto>> RetrieveCharge(string chargeId);
}