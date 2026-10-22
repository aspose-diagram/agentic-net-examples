using System;
using System.IO;
using System.Net.Http;
using Aspose.Diagram;

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

        // Validate hyperlink format
        if (!Uri.IsWellFormedUriString(hyperlinkAddress, UriKind.Absolute))
        {
            Console.Error.WriteLine($"Malformed hyperlink address: {hyperlinkAddress}");
            return;
        }

        // Check reachability of the hyperlink (simple HTTP GET)
        bool reachable = false;
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Set a short timeout to avoid long waits
                client.Timeout = TimeSpan.FromSeconds(5);
                HttpResponseMessage response = client.GetAsync(hyperlinkAddress).Result;
                reachable = response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while checking hyperlink reachability: {ex.Message}");
        }

        if (!reachable)
        {
            Console.Error.WriteLine($"Hyperlink address is unreachable: {hyperlinkAddress}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the first non-deleted shape on the first page
            Shape targetShape = null;
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to add a hyperlink.");
                return;
            }

            // Create and configure the hyperlink
            Hyperlink link = new Hyperlink();
            link.Name = "ExternalLink";
            link.Address.Value = hyperlinkAddress;
            link.Description.Value = "Added via Aspose.Diagram";

            // Add the hyperlink to the shape's collection
            targetShape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}