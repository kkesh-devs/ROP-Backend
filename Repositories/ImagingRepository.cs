using AutoMapper;
using KKESH_ROP.DTO.Imaging;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class ImagingRepository : IImagingRepository
{
    private readonly IMongoCollection<PatientExam> _patientExams;
    private readonly IMapper _mapper;

    public ImagingRepository(IMongoDatabase database, IMapper mapper)
    {
        _patientExams = database.GetCollection<PatientExam>("PatientExams");
        _mapper = mapper;
    }

    public async Task<Response<List<PatientExamDto>>> GetPatientExamsByMRNAsync(string patientMRN)
    {
        try
        {
            var exams = await _patientExams
                .Find(x => x.PatientMRN == patientMRN)
                .SortByDescending(x => x.ExamDate)
                .ToListAsync();

            var dtos = exams.Select(exam => new PatientExamDto
            {
                Id = exam._id.ToString(),
                ExamId = exam.ExamId,
                PatientMRN = exam.PatientMRN,
                ExamDate = exam.ExamDate,
                ExamType = exam.ExamType,
                Status = exam.Status,
                ImageCount = exam.Images?.Count ?? 0
            }).ToList();

            return new Response<List<PatientExamDto>>(true, "Patient exams retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<PatientExamDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<ExamImagesDto>> GetExamImagesByIdAsync(string examId)
    {
        try
        {
            var exam = await _patientExams
                .Find(x => x.ExamId == examId)
                .FirstOrDefaultAsync();

            if (exam == null)
                return new Response<ExamImagesDto>(false, "Exam not found", null);

            var dto = new ExamImagesDto
            {
                ExamId = exam.ExamId,
                PatientMRN = exam.PatientMRN,
                Images = exam.Images?.Select(img => new ImagePathDto
                {
                    ImagePath = img.ImagePath,
                    ThumbnailPath = img.ThumbnailPath,
                    ImageName = img.ImageName
                }).ToList() ?? new List<ImagePathDto>()
            };

            return new Response<ExamImagesDto>(true, "Exam images retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<ExamImagesDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<LatestExamDto>> GetLatestExamByMRNAsync(string patientMRN)
    {
        try
        {
            var latestExam = await _patientExams
                .Find(x => x.PatientMRN == patientMRN)
                .SortByDescending(x => x.ExamDate)
                .FirstOrDefaultAsync();

            if (latestExam == null)
                return new Response<LatestExamDto>(false, "No exams found for this patient", null);

            var dto = new LatestExamDto
            {
                ExamId = latestExam.ExamId,
                ExamDate = latestExam.ExamDate,
                ExamType = latestExam.ExamType
            };

            return new Response<LatestExamDto>(true, "Latest exam retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<LatestExamDto>(false, ex.Message, null);
        }
    }
}
