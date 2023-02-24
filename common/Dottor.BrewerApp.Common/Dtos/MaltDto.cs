namespace Dottor.BrewerApp.Common.Dtos;

internal record MaltDto
{
    public string Name { get; set; }
    public AmountDto Amount { get; set; }
}
