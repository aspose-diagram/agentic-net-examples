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

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply a global line style to the shape
                    shape.Line.LineColor.Value = "#FF0000";          // Red line color
                    shape.Line.LineWeight.Value = 0.02;             // Line weight (in inches)
                    shape.Line.LinePattern.Value = LinePatternValue.Dash; // Dashed line pattern
                }
            }

            // Path for the updated Visio file
            string outputPath = "output.vsdx";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
