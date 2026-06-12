using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path where the modified diagram will be saved
            string outputPath = "output.vsdx";

            // Load the diagram from file (lifecycle: load)
            Diagram diagram = new Diagram(inputPath);

            // Access page two (Visio pages are 1‑based indexed)
            Page pageTwo = diagram.Pages[2];

            // Iterate through all shapes on page two
            foreach (Shape shape in pageTwo.Shapes)
            {
                // Replace occurrences of "Draft" with "Final"
                shape.ReplaceText("Draft", "Final");

                // Refresh shape data after text change
                shape.RefreshData();
            }

            // Save the updated diagram (lifecycle: save)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
