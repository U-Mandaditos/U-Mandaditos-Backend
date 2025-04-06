using Domain.Common;

namespace Domain.Entities;

public class Post: AggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public double SugestedValue { get; set; }
    
    public int? IdPosterUser { get; set; }
    public User? PosterUser { get; set; } 
    
    public int? IdPickUpLocation { get; set; }
    public Location? PickUpLocation { get; set; }
    
    public int?  IdDeliveryLocation { get; set; }
    public  Location? DeliveryLocation { get; set; }
    
    public  DateTime CreatedAt { get; set; }
    
    public bool Completed { get; set; }

    public bool Accepted { get; set; } 
    
}