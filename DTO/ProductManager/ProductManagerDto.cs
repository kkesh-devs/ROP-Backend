namespace KKESH_ROP.DTO.ProductManager;

public class CreateProductManagerDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }

}


public class UpdateProductManagerDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
}

public class ProductManagerDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }

}
