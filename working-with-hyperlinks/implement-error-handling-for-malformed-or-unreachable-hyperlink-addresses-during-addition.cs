using System.IO;
using System;
using System.Net.Http;
using Aspose.Diagram;

class Program
{
    // Validates that the address is a well‑formed absolute URI with http/https scheme
    // and that the resource is reachable (HEAD request). Returns true if both checks pass.
    static bool IsValidAndReachable(string address)
    {
        // Check URI format
        if (!Uri.TryCreate(address, UriKind.Absolute, out Uri? uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            Console.WriteLine($"Invalid URI format: {address}");
            return false;
        }

        // Attempt to reach the resource
        try
        {
            using var httpClient = new HttpClient();
            // Use a short timeout to avoid long waits on unreachable hosts
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            // Send a HEAD request; if not supported, fall back to GET
            var request = new HttpRequestMessage(HttpMethod.Head, uriResult);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                // Some servers may reject HEAD; try GET as a fallback
                response = httpClient.GetAsync(uriResult).Result;
            }

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Unreachable URL (status {response.StatusCode}): {address}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reaching URL '{address}': {ex.Message}");
            return false;
        }
    }

    static void Main()
    {
        // Path to the source Visio diagram
        string inputPath = "input.vsdx";
        // Path for the output diagram
        string outputPath = "output.vsdx";

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Example: add a hyperlink to the first shape on the first page
        if (diagram.Pages.Count == 0)
        {
            Console.WriteLine("Diagram contains no pages.");
            return;
        }

        var page = diagram.Pages[0];
        if (page.Shapes.Count == 0)
        {
            Console.WriteLine("First page contains no shapes.");
            return;
        }

        // Retrieve the first shape
        var shape = page.Shapes[0];

        // Desired hyperlink address
        string hyperlinkAddress = "https://example.com";

        // Validate the address before adding
        if (IsValidAndReachable(hyperlinkAddress))
        {
            // Create and configure the hyperlink
            Hyperlink link = new Hyperlink();
            link.Name = "ExternalLink";
            link.Address.Value = hyperlinkAddress;
            link.Description.Value = "Visit Example.com";

            // Ensure the Hyperlinks collection exists
            if (shape.Hyperlinks == null)
            {
                // The collection is always instantiated by Aspose.Diagram,
                // but guard against unexpected nulls.
                Console.WriteLine("Shape's Hyperlinks collection is null; cannot add hyperlink.");
            }
            else
            {
                shape.Hyperlinks.Add(link);
                Console.WriteLine($"Hyperlink added to shape ID {shape.ID}.");
            }
        }
        else
        {
            Console.WriteLine("Hyperlink not added due to validation failure.");
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
