using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the modified Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the file system
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Override inherited fill colors by setting the shape's Fill cells directly
                    shape.Fill.FillBkgnd.Value = "#FF0000";   // Red background
                    shape.Fill.FillForegnd.Value = "#00FF00"; // Green foreground
                }
            }

            // Save the modified diagram using a valid overload (SaveFileFormat enum)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}