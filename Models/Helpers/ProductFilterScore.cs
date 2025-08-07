namespace JFjewelery.Models.Helpers
{
    public class ProductFilterScore
    {
        public static readonly Dictionary<string, int> Weights = new()
        {
            ["Gender"] = 20,
            ["Style"] = 5,
            ["Manufacturer"] = 5,
            ["Description"] = 10,
            ["Metal"] = 5,
            ["MetalShape"] = 5,
            ["MetalColor"] = 5,
            ["MetalSize"] = 5,
            ["MetalType"] = 5,
            ["Purity"] = 3,
            ["Weight"] = 2,
            ["Stone"] = 5,
            ["StoneShape"] = 5,
            ["StoneColor"] = 5,
            ["StoneSize"] = 5,
            ["StoneType"] = 5,
            ["Count"] = 2
        };
    }
}
