using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Entry point of the console application
        static async Task Main(string[] args)
        {
            try
            {

                // Path to the output Visio file
                const string outputPath = "ValidatedDiagram.vsdx";

                // Create a new empty diagram
                using Diagram diagram = new Diagram();

                // Ensure there is at least one page to work with
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Reference to the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (master name "Rectangle" is built‑in)
                // Parameters: PinX, PinY, master name, page index
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Define the hyperlink address to be added
                const string hyperlinkAddress = "https://example.com";

                // Validate the hyperlink before adding it
                if (!IsValidUri(hyperlinkAddress))
                {
                    Console.WriteLine($"The address '{hyperlinkAddress}' is not a well‑formed absolute URI.");
                    return;
                }

                bool reachable = await IsReachableAsync(hyperlinkAddress);
                if (!reachable)
                {
                    Console.WriteLine($"The address '{hyperlinkAddress}' could not be reached.");
                    return;
                }

                // Create and configure the hyperlink
                Hyperlink link = new Hyperlink
                {
                    Name = "WebLink",
                    Description = { Value = "Example website" }
                };
                link.Address.Value = hyperlinkAddress;

                // Add the hyperlink to the shape's collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Checks whether a string is a well‑formed absolute URI with http/https scheme
        private static bool IsValidUri(string uriString)
        {
            if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }

        // Attempts a HEAD request to determine if the URI is reachable
        private static async Task<bool> IsReachableAsync(string uri)
        {
            try
            {
                using HttpClient client = new HttpClient();
                // Set a short timeout to avoid long waits on unreachable hosts
                client.Timeout = TimeSpan.FromSeconds(5);
                using HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Any exception (e.g., timeout, DNS failure) is treated as unreachable
                return false;
            }
        }
    }