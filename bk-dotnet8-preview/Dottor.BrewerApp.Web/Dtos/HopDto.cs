namespace Dottor.BrewerApp.Dtos;

internal record HopDto
{
    public string Name { get; set; }
    public AmountDto Amount { get; set; }
    public string Add { get; set; }
    public string Attribute { get; set; }
}
