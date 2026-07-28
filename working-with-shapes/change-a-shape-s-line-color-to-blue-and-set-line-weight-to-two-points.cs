using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count > 0)
            {
                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Set the line color to blue (hex format)
                shape.Line.LineColor.Value = "#0000FF";

                // Set the line weight to two points (2 pt = 2/72 inches)
                shape.Line.LineWeight.Value = 2.0 / 72.0;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
