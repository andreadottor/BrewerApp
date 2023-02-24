﻿namespace Dottor.BrewerApp.Common.Dtos;

internal record IngredientsDto
{
    public MaltDto[] Malt { get; set; }
    public HopDto[] Hops { get; set; }
    public string Yeast { get; set; }
}
