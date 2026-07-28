using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram using the provided constructor (load rule)
            Diagram diagram = new Diagram(inputPath);

            // Access the second page (index 1 because collection is zero‑based)
            Page pageTwo = diagram.Pages[1];

            // Iterate through all shapes on page two
            foreach (Shape shape in pageTwo.Shapes)
            {
                // Replace every occurrence of "Draft" with "Final"
                shape.ReplaceText("Draft", "Final");

                // Refresh shape data after text modification
                shape.RefreshData();
            }

            // Save the updated diagram using the provided save rule
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
