using System;
using System.Net.Http;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Path to the source Visio file (replace with actual path)
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(sourcePath);
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

            // Retrieve the shape (using the first shape's ID)
            var shape = page.Shapes.GetShape(page.Shapes[0].ID);
            if (shape == null)
            {
                Console.WriteLine("Failed to retrieve the shape.");
                return;
            }

            // Hyperlink details
            string hyperlinkAddress = "https://example.com";
            string hyperlinkDescription = "Example website";

            // Validate and add the hyperlink
            try
            {
                AddHyperlinkWithValidation(diagram, shape, hyperlinkAddress, hyperlinkDescription);
                Console.WriteLine("Hyperlink added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding hyperlink: {ex.Message}");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
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

        /// <summary>
        /// Validates the hyperlink address and adds it to the specified shape.
        /// Throws an exception if the address is malformed or unreachable.
        /// </summary>
        static void AddHyperlinkWithValidation(Diagram diagram, Shape shape, string address, string description)
        {
            // Basic URI format validation
            if (!Uri.IsWellFormedUriString(address, UriKind.Absolute))
                throw new Exception("The hyperlink address is not a well‑formed absolute URI.");

            Uri uri = new Uri(address, UriKind.Absolute);

            // For HTTP/HTTPS schemes, attempt a HEAD request to verify reachability
            if (uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
            {
                using (var httpClient = new HttpClient())
                {
                    // Set a short timeout to avoid long waits
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    HttpResponseMessage response;
                    try
                    {
                        // HEAD request is sufficient; fallback to GET if HEAD not supported
                        var request = new HttpRequestMessage(HttpMethod.Head, uri);
                        response = httpClient.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            // Try GET as a fallback
                            response = httpClient.GetAsync(uri).Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Unable to reach the hyperlink address: {ex.Message}");
                    }

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Hyperlink address returned an unsuccessful status code: {(int)response.StatusCode}");
                }
            }
            // For other schemes (file, ftp, etc.) we assume the format validation is sufficient

            // Create and configure the hyperlink
            Hyperlink link = new Hyperlink
            {
                Name = "AddedLink",
                Description = { Value = description }
            };
            link.Address.Value = address;

            // Ensure the Hyperlinks collection exists
            if (shape.Hyperlinks == null)
                throw new Exception("The shape does not support hyperlinks.");

            // Add the hyperlink to the shape
            shape.Hyperlinks.Add(link);
        }
    }