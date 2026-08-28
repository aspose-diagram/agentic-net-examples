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
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses the "Pentagon" master
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        // Set line pattern to dashed
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                        // Set line thickness (weight) to a specific value (e.g., 0.03 inches)
                        shape.Line.LineWeight.Value = 0.03;
                    }
                }
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
