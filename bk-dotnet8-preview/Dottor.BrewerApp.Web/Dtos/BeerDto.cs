namespace Dottor.BrewerApp.Dtos;
internal record BeerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tagline { get; set; }
    public string First_brewed { get; set; }
    public string Description { get; set; }
    public string Image_url { get; set; }
    public float Abv { get; set; }
    public float? Ibu { get; set; }
    public int Target_fg { get; set; }
    public float Target_og { get; set; }
    public int? Ebc { get; set; }
    public float? Srm { get; set; }
    public float? Ph { get; set; }
    public float Attenuation_level { get; set; }
    public VolumeDto Volume { get; set; }
    public BoilVolumeDto Boil_volume { get; set; }
    public MethodDto Method { get; set; }
    public IngredientsDto Ingredients { get; set; }
    public string[] Food_pairing { get; set; }
    public string Brewers_tips { get; set; }
    public string Contributed_by { get; set; }
}
