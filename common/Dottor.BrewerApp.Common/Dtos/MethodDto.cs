namespace Dottor.BrewerApp.Common.Dtos;

internal record MethodDto
{
    public MashTempDto[] Mash_temp { get; set; }
    public FermentationDto Fermentation { get; set; }
    public string Twist { get; set; }
}
