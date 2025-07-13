using System.Text.Json;
using JFjewelery.Models.DTO;
using JFjewelery.Services.Interfaces;


namespace JFjewelery.Services
{
    public class ApiManager : IApiManager
    {
        private readonly HttpClient _httpClient;
        public ApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductFilterCriteria> AnalyzeImageAsync(byte[] imageData, CancellationToken cancellationToken = default)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageData), "file", "image.jpg");

            // Check before sending
            var requestUri = "/analyze-image";
            var fullUri = new Uri(_httpClient.BaseAddress!, requestUri);

            Console.WriteLine($"[DEBUG] Sending request to: {fullUri}");
            if (!fullUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Request URI is not absolute. BaseAddress might not be set properly.");
            }

            var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<ProductFilterCriteria>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return result!;  // result! => null-forgiving
            

        }

    }
}
