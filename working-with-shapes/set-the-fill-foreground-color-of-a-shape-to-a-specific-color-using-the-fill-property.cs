using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Verify the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            Page page = diagram.Pages[0];

            // Verify the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                throw new Exception("The page contains no shapes.");
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Set the fill foreground color (hex string) and ensure solid fill pattern
            shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground color
            shape.Fill.FillPattern.Value = 1;        // Solid fill pattern

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
