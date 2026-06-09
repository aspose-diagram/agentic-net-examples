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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape's line pattern is solid
                    // LinePatternValue.Solid represents a solid line
                    if (shape.Line.LinePattern.Value == LinePatternValue.Solid)
                    {
                        // Example customization: change the line color to red
                        shape.Line.LineColor.Value = "#FF0000";

                        // Additional custom logic can be placed here
                        Console.WriteLine($"Processed shape ID {shape.ID} with solid line.");
                    }
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
