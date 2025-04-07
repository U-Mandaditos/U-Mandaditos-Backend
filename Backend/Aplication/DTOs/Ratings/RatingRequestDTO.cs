namespace Aplication.DTOs
{
    public class RatingRequestDTO
    {
        public int IdMandadito { get; set; }
        public int IdRater { get; set; }

        public int IdRatedUser { get; set; }

        public int RatingNum { get; set; }

        public string Review { get; set; } = string.Empty;

        public bool IsOwner { get; set; }


    }
}