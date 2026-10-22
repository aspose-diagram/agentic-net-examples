using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine output file path (default to "output.vsdx" if not provided)
        string outputPath = args.Length > 0 ? args[0] : "output.vsdx";

        // Ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Access the first page (a new diagram contains at least one page)
            Page page = diagram.Pages[0];

            // Define rectangle coordinates (in points)
            double left = 1.0;
            double top = 1.0;
            double right = 4.0;
            double bottom = 2.5;

            // Add a rectangle shape to the page; DrawRectangle returns the shape ID (long)
            long shapeId = page.DrawRectangle(left, top, right, bottom);

            // Retrieve the shape object using its ID
            Shape rectangle = page.Shapes.GetShape((int)shapeId);
            if (rectangle == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly created rectangle shape.");
                return;
            }

            // Add visible text to the rectangle
            string displayText = "Visit Aspose";
            rectangle.Text.Value.Add(new Txt(displayText));

            // Create a new hyperlink instance
            Hyperlink link = new Hyperlink
            {
                // Optional name for the hyperlink
                Name = "AsposeWebLink"
            };
            // Set the target URL (website address)
            link.Address.Value = "https://www.aspose.com";

            // Add the hyperlink to the rectangle's Hyperlinks collection
            rectangle.Hyperlinks.Add(link);

            // Save the diagram to the specified output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}