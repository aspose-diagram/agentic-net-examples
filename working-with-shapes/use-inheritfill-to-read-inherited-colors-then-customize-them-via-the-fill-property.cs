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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Read inherited fill properties
                    string inheritedFore = shape.InheritFill.FillForegnd.Value;
                    string inheritedBack = shape.InheritFill.FillBkgnd.Value;
                    int inheritedPattern = shape.InheritFill.FillPattern.Value;

                    Console.WriteLine($"Shape ID {shape.ID}: Inherited Foreground={inheritedFore}, Background={inheritedBack}, Pattern={inheritedPattern}");

                    // Customize the shape's fill
                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                    shape.Fill.FillForegnd.Value = "#FF0000";       // Red foreground
                    shape.Fill.FillBkgnd.Value = "#00FF00";         // Green background
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
