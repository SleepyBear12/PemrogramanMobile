using System.ComponentModel.DataAnnotations;

namespace BlazingPizzaApp.Model
{
    public enum PizzaSize
    {
        Small,
        Medium,
        Large
    }

    public class Pizza
    {
        // Represent pizza size in inches for finer control
        public const int MinimumSize = 10; // 10 inches
        public const int MaximumSize = 20; // 20 inches

        [Range(1, int.MaxValue, ErrorMessage = "You must select a pizza.")]
        public int SpecialId { get; set; }

        [Range(MinimumSize, MaximumSize, ErrorMessage = "Size must be within allowed inches range.")]
        public int Size { get; set; } = 12; // default 12 inches

        public PizzaSpecial? Special { get; set; }

        public string SizeDisplay => $"{Size} in";

        // Now Size stores inches directly
        public int SizeInches => Size;

        public string SpecialName => Special?.Name ?? "No pizza selected";

        public decimal BasePrice => Special?.BasePrice ?? 0m;

        // Calculate total price scaled by diameter in inches.
        // Assume BasePrice is the price for the minimum size (10 inches).
        // Scale linearly by inches so a 12" pizza is BasePrice * 12/10, etc.
        public decimal GetTotalPrice() => BasePrice * SizeInches / 10m;

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}
