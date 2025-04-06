namespace Aplication.DTOs.Users.Profile
{
    public class UserPublicProfileInfoResponseDTO
    {

        public UserProfileResponseDTO? User { get; set; }

        public UserStatsResponseDTO? Stats { get; set; }

        public List<UserReviewsResponseDTO> Reviews { get; set; }

    }
}
