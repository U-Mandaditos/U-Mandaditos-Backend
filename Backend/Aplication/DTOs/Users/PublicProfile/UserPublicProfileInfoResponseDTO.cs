using Aplication.DTOs.Locations;
using Aplication.DTOs.Media;

namespace Aplication.DTOs.Users.PublicProfile
{
    public class UserPublicProfileInfoResponseDTO
    {

        public UserProfileResponseDTO? User { get; set; }

        public UserStatsResponseDTO? Stats { get; set; }

        public List<UserReviewsResponseDTO> Reviews { get; set; }

    }
}
