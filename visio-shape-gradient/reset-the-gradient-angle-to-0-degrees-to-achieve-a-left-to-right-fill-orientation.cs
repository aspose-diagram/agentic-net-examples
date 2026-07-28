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
            const string inputPath = "input.vsdx";
            // Path for the modified Visio file
            const string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages (optional: you can target a specific page)
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Reset gradient angle to 0 degrees (left‑to‑right fill)
                        // Ensure we access the .Value property of the cell
                        shape.Fill.GradientFill.GradientAngle.Value = 0;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Gradient angles have been reset to 0 degrees.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
