using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioDiagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's name (use NameU; fallback if null).
                    string shapeName = shape.NameU ?? "(no name)";

                    // Ensure the Hyperlinks collection exists.
                    if (shape.Hyperlinks != null)
                    {
                        bool hasAny = false;

                        // Iterate through each hyperlink attached to the shape.
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Retrieve the hyperlink address (URL). Guard against null.
                            string url = link.Address?.Value ?? "(no address)";

                            // Print shape name once, then each hyperlink.
                            if (!hasAny)
                            {
                                Console.WriteLine($"Shape: {shapeName}");
                                hasAny = true;
                            }

                            Console.WriteLine($"  Hyperlink: {url}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}