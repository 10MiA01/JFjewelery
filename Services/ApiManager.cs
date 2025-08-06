using System.Text.Json;
using System.Threading;
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

        public async Task<byte[]> TryOnAsync(byte[] imageData, string category, int id, CancellationToken cancellationToken = default)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageData), "file", "image.jpg");
            content.Add(new StringContent(category), "category");
            content.Add(new StringContent(id.ToString()), "id");


            var requestUri = "/virtual-fitting";
            var fullUri = new Uri(_httpClient.BaseAddress!, requestUri);
            Console.WriteLine($"[DEBUG] Sending request to: {fullUri}");
            if (!fullUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Request URI is not absolute. BaseAddress might not be set properly.");
            }
            var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using (var memoryStream = new MemoryStream())
            {
                await imageStream.CopyToAsync(memoryStream, cancellationToken);
                var result = memoryStream.ToArray();
                return result!; 
            }

        }

    }
}
