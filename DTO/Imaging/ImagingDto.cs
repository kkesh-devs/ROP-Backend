namespace KKESH_ROP.DTO.Imaging;

public class PatientExamDto
{
    public string Id { get; set; }
    public string ExamId { get; set; }
    public string PatientMRN { get; set; }
    public DateTime ExamDate { get; set; }
    public string ExamType { get; set; }
    public string Status { get; set; }
    public int ImageCount { get; set; }
}

public class ExamImagesDto
{
    public string ExamId { get; set; }
    public string PatientMRN { get; set; }
    public List<ImagePathDto> Images { get; set; } = new();
}

public class ImagePathDto
{
    public string ImagePath { get; set; }
    public string ThumbnailPath { get; set; }
    public string ImageName { get; set; }
}

public class LatestExamDto
{
    public string ExamId { get; set; }
    public DateTime ExamDate { get; set; }
    public string ExamType { get; set; }
}
