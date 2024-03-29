﻿namespace Discogs.Api.Models;

public class ReleaseDTO
{
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Label { get; set; }
    public string Format { get; set; }
    public string FormatDetail { get; set; }
    public string ImageUrl { get; set; }
    public int Year { get; set; }
}
