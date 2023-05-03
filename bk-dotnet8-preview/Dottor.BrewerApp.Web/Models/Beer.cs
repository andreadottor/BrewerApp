namespace Dottor.BrewerApp.Common.Models
{
    public record Beer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string FirstBrewed { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// Alcohol by Volume
        /// </summary>
        public float Abv { get; set; }

        public string[] FoodPairing { get; set; }
        public string BrewersTips { get; set; }
    }
}
