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

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Retrieve the first shape
            Shape shape = page.Shapes[0];

            // Rotate the shape 45 degrees (convert to radians)
            double angleDeg = 45;
            double angleRad = Math.PI * angleDeg / 180.0;
            shape.SetAngle(angleRad);

            // Set KeepTextFlat to true so the text stays horizontal
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with KeepTextFlat enabled.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
