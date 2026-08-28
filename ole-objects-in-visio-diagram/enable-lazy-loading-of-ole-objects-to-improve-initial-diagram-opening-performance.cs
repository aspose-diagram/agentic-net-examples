using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram without explicit lazy‑loading option (property not available in current API)
            Diagram diagram = new Diagram(inputPath);

            // Enumerate pages and shapes to locate OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE shapes: must be a foreign shape with an embedded object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        Console.WriteLine($"Found OLE shape with ID: {shape.ID}");
                    }
                }
            }

            // Save the diagram unchanged; lazy loading (if internally supported) does not affect saving
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading, processing, or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}