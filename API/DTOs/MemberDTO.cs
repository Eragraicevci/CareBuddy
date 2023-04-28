using API.DTOs;
using API.Entities;

public class MemberDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public String Photo { get; set; }

    public String AnalysisResultFile { get; set; }

    public int Age { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastActive { get; set; } 

    public string Gender { get; set; }

    public string Description { get; set; }

    public string LanguageSpoken { get; set; }
    public List<PhotoDto> Photos { get; set; }
    public List<AddressDTO> Addresses { get; set; }
    public List<AnalysisResultFileDTO> AnalysisResultFiles { get; set; }
    public List<MedicalExpertiseDTO> MedicalExpertises { get; set; }
}
