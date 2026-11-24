using System;
using System.IO;
using System.Net.Http;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, hyperlink address
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <hyperlinkAddress>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string hyperlinkAddress = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Validate hyperlink format using Uri.TryCreate
        if (!Uri.TryCreate(hyperlinkAddress, UriKind.Absolute, out Uri uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps && uriResult.Scheme != Uri.UriSchemeFtp && uriResult.Scheme != Uri.UriSchemeMailto))
        {
            Console.Error.WriteLine($"The hyperlink address is not a valid absolute URI: {hyperlinkAddress}");
            return;
        }

        // Optional: check reachability for http/https/ftp schemes
        if (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeFtp)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Set a short timeout for the check
                    client.Timeout = TimeSpan.FromSeconds(5);
                    // Perform a HEAD request; fallback to GET if HEAD not supported
                    HttpResponseMessage response = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uriResult)).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        // Try GET as a fallback
                        response = client.GetAsync(uriResult).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.Error.WriteLine($"Unable to reach the hyperlink address (status code {response.StatusCode}): {hyperlinkAddress}");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while checking hyperlink reachability: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the first non-deleted shape to attach the hyperlink
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    targetShape = shape;
                    break;
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found in the diagram to add a hyperlink.");
                return;
            }

            // Ensure the Hyperlinks collection is initialized
            if (targetShape.Hyperlinks == null)
            {
                // The Hyperlinks collection is read‑only; however, adding a new Hyperlink will
                // automatically create the collection if it is null in supported versions.
                // If null, we simply proceed to add; Aspose will handle it.
            }

            // Create and configure the hyperlink
            Hyperlink link = new Hyperlink();
            link.Name = "AddedLink";
            link.Address.Value = hyperlinkAddress;
            link.Description.Value = "Added via Aspose.Diagram";

            // Add the hyperlink to the shape
            targetShape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Hyperlink added successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}