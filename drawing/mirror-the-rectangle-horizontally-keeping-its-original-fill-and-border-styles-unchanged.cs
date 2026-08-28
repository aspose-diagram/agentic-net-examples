using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (or adjust index as needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify rectangle shapes by their master name
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    // Mirror the shape horizontally.
                    // FlipX is a BoolValue; set its .Value to BOOL.True.
                    shape.XForm.FlipX.Value = BOOL.True;

                    // Fill and line styles are unchanged automatically.
                }
            }

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
