using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found on the page.");
                return;
            }

            // Create a new hyperlink
            Hyperlink link = new Hyperlink();
            link.Name = "LinkSameWindow";
            link.Address.Value = "https://example.com";

            // Set the target frame to an empty string so the link opens in the same window
            // Hyperlink properties are cell‑based; therefore use the .Value accessor.
            link.Frame.Value = "";

            // Add the hyperlink to the shape's Hyperlinks collection
            targetShape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
