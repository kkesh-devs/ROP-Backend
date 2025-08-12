using KKESH_ROP.DTO.Imaging;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IImagingRepository
{
    Task<Response<List<PatientExamDto>>> GetPatientExamsByMRNAsync(string patientMRN);
    Task<Response<ExamImagesDto>> GetExamImagesByIdAsync(string examId);
    Task<Response<LatestExamDto>> GetLatestExamByMRNAsync(string patientMRN);
}
