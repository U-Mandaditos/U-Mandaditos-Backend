﻿namespace Aplication.DTOs.Locations;

public class LocationResponseDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool Active { get; set; }
}