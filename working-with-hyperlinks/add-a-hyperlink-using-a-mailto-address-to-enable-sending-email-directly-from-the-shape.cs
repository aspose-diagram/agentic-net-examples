using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Locate the first shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    targetShape = shp;
                    break; // only need the first shape
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No shape found on the first page.");
                    return;
                }

                // Ensure the shape can hold hyperlinks (collection should be non‑null)
                if (targetShape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("The shape does not support hyperlinks.");
                    return;
                }

                // Create a new hyperlink that uses a mailto: address
                Hyperlink link = new Hyperlink();
                link.Name = "EmailLink";
                link.Address.Value = "mailto:someone@example.com";
                link.Description.Value = "Send email";

                // Add the hyperlink to the shape's collection
                targetShape.Hyperlinks.Add(link);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Hyperlink added successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}