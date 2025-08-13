using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagingController(IImagingRepository imagingRepository) : ControllerBase
{
    //____________________________________________________________________________________________________________________________________________________

    [HttpGet("patient/{mrn}/exams")]
    public async Task<IActionResult> GetPatientExams(string mrn)
    {
        if (string.IsNullOrWhiteSpace(mrn))
            return BadRequest(new Response<string>(false, "Patient MRN is required", null));

        var response = await imagingRepository.GetPatientExamsByMRNAsync(mrn);
        return Ok(response);
    }

    //____________________________________________________________________________________________________________________________________________________

    [HttpGet("exam/{examId}/images")]
    public async Task<IActionResult> GetExamImages(string examId)
    {
        if (string.IsNullOrWhiteSpace(examId))
            return BadRequest(new Response<string>(false, "Exam ID is required", null));

        var response = await imagingRepository.GetExamImagesByIdAsync(examId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    //____________________________________________________________________________________________________________________________________________________

    [HttpGet("patient/{mrn}/latest-exam")]
    public async Task<IActionResult> GetLatestExam(string mrn)
    {
        if (string.IsNullOrWhiteSpace(mrn))
            return BadRequest(new Response<string>(false, "Patient MRN is required", null));

        // Dummy response for testing
        var dummyData = new
        {
            ExamId = "74F5BC86-1131-4CDC",
            ExamType = ""
        };
        
        var response = new Response<object>(true, "Success", dummyData);
        return Ok(response);
        
        // Original code commented out for testing
        // var response = await imagingRepository.GetLatestExamByMRNAsync(mrn);
        // if (!response.Success) return NotFound(response);
        // return Ok(response);
    }

    //____________________________________________________________________________________________________________________________________________________
}
