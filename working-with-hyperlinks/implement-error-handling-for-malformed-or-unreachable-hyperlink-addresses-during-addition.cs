using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                // Load an existing diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Get the first page and first shape for demonstration
                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                Shape shape = page.Shapes[0];

                // Example hyperlink data
                string url = "https://example.com";
                string description = "Example website";

                // Attempt to add a validated hyperlink
                await AddValidatedHyperlinkAsync(shape, url, description);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Validates the URL format and reachability before adding it as a hyperlink to the shape.
        /// </summary>
        /// <param name="shape">The target shape.</param>
        /// <param name="url">The hyperlink address.</param>
        /// <param name="description">Optional description for the hyperlink.</param>
        private static async Task AddValidatedHyperlinkAsync(Shape shape, string url, string description)
        {
            if (shape == null)
            {
                Console.WriteLine("Shape is null.");
                return;
            }

            // Ensure the Hyperlinks collection exists
            if (shape.Hyperlinks == null)
            {
                Console.WriteLine("Hyperlinks collection is null.");
                return;
            }

            // Validate URL format
            if (!IsValidUrl(url))
            {
                Console.WriteLine($"Malformed URL: '{url}'. Hyperlink not added.");
                return;
            }

            // Check if the URL is reachable
            if (!await IsUrlReachableAsync(url))
            {
                Console.WriteLine($"Unreachable URL: '{url}'. Hyperlink not added.");
                return;
            }

            // Create and configure the hyperlink
            Hyperlink link = new Hyperlink
            {
                Name = "Link_" + Guid.NewGuid().ToString("N")
            };
            link.Address.Value = url;
            link.Description.Value = description;

            // Add the hyperlink to the shape
            shape.Hyperlinks.Add(link);
            Console.WriteLine($"Hyperlink added to shape ID {shape.ID}: {url}");
        }

        /// <summary>
        /// Checks whether the provided string is a well‑formed absolute HTTP/HTTPS URL.
        /// </summary>
        private static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Attempts a simple GET request to determine if the URL is reachable.
        /// </summary>
        private static async Task<bool> IsUrlReachableAsync(string url)
        {
            try
            {
                using HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                using HttpResponseMessage response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking URL reachability: {ex.Message}");
                return false;
            }
        }
    }