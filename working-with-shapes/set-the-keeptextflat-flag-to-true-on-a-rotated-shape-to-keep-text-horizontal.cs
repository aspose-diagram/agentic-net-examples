using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                throw new Exception("The first page contains no shapes.");
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // Rotate the shape (angle in degrees)
            shape.XForm.Angle.Value = 45; // Rotate 45 degrees

            // Keep the text horizontal despite rotation
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
