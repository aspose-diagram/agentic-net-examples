using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (created by default)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, masterName, pageIndex
            long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);

            // Retrieve the shape instance using the returned ID (cast to int if required)
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Create a new hyperlink instance and set its properties
            Hyperlink hyperlink = new Hyperlink();
            hyperlink.Name = "MyLink";                         // Visible label for the hyperlink
            hyperlink.Address.Value = "https://example.com";   // External URL
            hyperlink.SubAddress.Value = "Page-1";             // Target within the document
            hyperlink.Description.Value = "Open example website"; // Tooltip description

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(hyperlink);

            // Save the diagram to a VSDX file using the correct SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}