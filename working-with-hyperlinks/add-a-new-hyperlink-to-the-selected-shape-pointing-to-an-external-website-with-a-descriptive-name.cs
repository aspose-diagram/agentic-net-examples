using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source and the resulting diagram
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Select the page (first page in this example)
            Page page = diagram.Pages[0];

            // Identify the shape to which the hyperlink will be added.
            // Here we assume the shape ID is known (e.g., 1). Adjust as needed.
            long targetShapeId = 1;
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Create a new hyperlink instance
            Hyperlink link = new Hyperlink();
            link.Name = "ExternalSite";                     // Internal identifier (optional)
            link.Address.Value = "https://www.example.com"; // External URL
            link.Description.Value = "Open Example Website"; // Tooltip / descriptive text

            // Add the hyperlink to the shape's collection
            shape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
