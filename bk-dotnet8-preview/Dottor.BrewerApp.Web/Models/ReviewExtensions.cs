namespace Dottor.BrewerApp.Web.Models;

public static class ReviewExtensions
{
    public static double AverageRating(this IList<Review> reviews)
        => Math.Round(reviews.Select(review => review.Rating).DefaultIfEmpty(0).Average(), 1);
}
