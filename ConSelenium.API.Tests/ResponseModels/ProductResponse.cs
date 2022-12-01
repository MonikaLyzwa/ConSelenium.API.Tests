namespace ConSelenium.API.Tests.ResponseModels
{
    internal class ProductResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        public int? Stock { get; set; }
    }
}
