using JFjewelery.Models.Enums;

namespace JFjewelery.Models.Helpers
{
    public class CategoryInstructions
    {

        public static readonly Dictionary<CategoryTryOnInstructions, string> Instructions = new()
        {
            { CategoryTryOnInstructions.Rings, "Please, send a photo with your hand visible." },
            { CategoryTryOnInstructions.Earrings, "Please, send a photo with your ears clearly visible." },
            { CategoryTryOnInstructions.Necklaces, "Please, send a photo of your neck and upper chest area." },
            { CategoryTryOnInstructions.Bracelets, "Please, send a photo with your wrist visible." },
            { CategoryTryOnInstructions.Pendants, "Please, send a photo of your neck area, like for necklaces." },
            { CategoryTryOnInstructions.Brooches, "Please, send a photo of your chest or clothing area where a brooch would be worn." },
            { CategoryTryOnInstructions.Chains, "Please, send a photo of your neck area." },
            { CategoryTryOnInstructions.Ear_Cuffs, "Please, send a close-up photo of your ear." },
            { CategoryTryOnInstructions.Hair_Accessories, "Please, send a photo showing the back or side of your head." },
            { CategoryTryOnInstructions.Chokers, "Please, send a photo of your neck area." },
            { CategoryTryOnInstructions.Pins, "Please, send a photo of your jacket or shirt where a pin might be worn." },
        };
    }
}
