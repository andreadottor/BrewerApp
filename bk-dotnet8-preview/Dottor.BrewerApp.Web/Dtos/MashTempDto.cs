namespace Dottor.BrewerApp.Common.Dtos;


internal record MashTempDto
{
    public TempDto Temp { get; set; }
    public int? Duration { get; set; }
}
