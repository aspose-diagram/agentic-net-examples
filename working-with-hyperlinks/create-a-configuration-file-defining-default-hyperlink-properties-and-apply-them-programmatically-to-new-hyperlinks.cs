using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Simple class to hold default hyperlink settings
    private class HyperlinkDefaults
    {
        public string Name { get; set; } = "DefaultLink";
        public string Address { get; set; } = "https://example.com";
        public string SubAddress { get; set; } = "";
        public string Description { get; set; } = "Default hyperlink description";
    }

    static void Main(string[] args)
    {
        // Paths to the source and destination Visio files
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Use the first shape on the first page as the target for the hyperlink
            Shape targetShape = page.Shapes[0];

            // Define default hyperlink properties
            HyperlinkDefaults defaults = new HyperlinkDefaults();

            // Create a new hyperlink instance and apply default values
            Hyperlink link = new Hyperlink();
            link.Name = defaults.Name;
            link.Address.Value = defaults.Address;
            link.SubAddress.Value = defaults.SubAddress;
            link.Description.Value = defaults.Description;

            // Add the hyperlink to the shape's Hyperlinks collection
            if (targetShape.Hyperlinks != null)
            {
                targetShape.Hyperlinks.Add(link);
                Console.WriteLine("Hyperlink added to shape successfully.");
            }
            else
            {
                Console.Error.WriteLine("The shape's Hyperlinks collection is null.");
                return;
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}