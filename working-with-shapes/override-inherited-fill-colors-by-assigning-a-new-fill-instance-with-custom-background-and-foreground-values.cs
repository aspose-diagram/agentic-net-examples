using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Verify the diagram contains at least one page
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            // Access the first page
            Page page = diagram.Pages[0];

            // Verify the page contains at least one shape
            if (page.Shapes.Count == 0)
                throw new Exception("The first page contains no shapes.");

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Override inherited fill colors by setting the shape's Fill cells directly
            shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground
            shape.Fill.FillBkgnd.Value = "#00FF00";   // Green background

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Save the modified diagram using the appropriate overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}