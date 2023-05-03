namespace Dottor.BrewerApp.Dtos;

internal record MaltDto
{
    public string Name { get; set; }
    public AmountDto Amount { get; set; }
}
