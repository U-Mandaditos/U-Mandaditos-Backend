namespace Aplication.DTOs.Posts;

public class PostResponseDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; } 
    public string Description { get; set; } 
    public double SuggestedValue { get; set; }
    public string PosterUserName { get; set; } 
    public string CreatedAt { get; set; }
    
    public string PickUpLocation { get; set; }
    public string DeliveryLocation { get; set; }
    public bool Completed { get; set; }
    public bool Accepted { get; set; } = false;
}