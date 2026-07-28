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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

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

                    // Read inherited fill properties
                    string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
                    string inheritedBackColor = shape.InheritFill.FillBkgnd.Value;
                    int inheritedPattern = shape.InheritFill.FillPattern.Value;

                    Console.WriteLine($"Shape ID {shape.ID} inherited fill - Foreground: {inheritedForeColor}, Background: {inheritedBackColor}, Pattern: {inheritedPattern}");

                    // Customize the shape's fill:
                    // 1. Set a solid fill pattern (value 1)
                    // 2. Assign new foreground and background colors
                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                    shape.Fill.FillForegnd.Value = "#00FF00";       // Green foreground
                    shape.Fill.FillBkgnd.Value = "#0000FF";         // Blue background
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
