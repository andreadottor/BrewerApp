﻿namespace Dottor.BrewerApp.Common.Utilities;

using Dottor.BrewerApp.Common.Dtos;
using Dottor.BrewerApp.Common.Models;

internal class BeerMapper
{

    public static Beer Map(BeerDto dto)
    {
        var beer = new Beer();
        beer.Id = dto.Id;
        beer.Name = dto.Name;
        beer.Tagline = dto.Tagline;
        beer.Description = dto.Description;
        beer.FirstBrewed = dto.First_brewed;
        beer.ImageUrl = dto.Image_url;
        beer.Abv = dto.Abv;
        beer.FoodPairing = dto.Food_pairing;
        beer.BrewersTips = dto.Brewers_tips;


        return beer;
    }
}
