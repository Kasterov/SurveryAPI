namespace Application.DTOs.FileEntities;

public class UploadFileDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Base64 { get; set; }
    public string ContentType { get; set; }
    public string Expression { get; set; }

}
