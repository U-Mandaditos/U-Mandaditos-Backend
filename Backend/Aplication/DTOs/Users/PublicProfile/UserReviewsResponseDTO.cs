namespace Aplication.DTOs.Users.PublicProfile
{
    public class UserReviewsResponseDTO
    {
        public int Id { set; get; }
        public UserInfoForReviewDTO User { set; get; }
        public string Comment { set; get; }
        public string CommentDate { set; get; }   
        public bool IsPosted { set; get; }
 
    }
}
