using Aplication.DTOs.Locations;
using Aplication.DTOs.Media;

namespace Aplication.DTOs.Users
{
    public class UserProfileResponseDTO
    {
        public string? Name { get; set; } = string.Empty;
        public string? Dni { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string BirthDay { get; set; }

        public int Edad { get; set; }
        public int Score { get; set; }

        public MediaResponseDTO? ProfilePic { get; set; }

        public LastLocationUserDTO? LastLocation { get; set; }

        public CareerResponseDTO? Career { get; set; }
    }
}
