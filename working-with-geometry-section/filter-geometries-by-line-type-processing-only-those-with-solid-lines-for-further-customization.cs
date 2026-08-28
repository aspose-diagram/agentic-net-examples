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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a line definition
                    if (shape.Line == null || shape.Line.LinePattern == null)
                        continue;

                    // Process only shapes with a solid line pattern
                    // In Visio, a solid line corresponds to LinePatternValue.Solid
                    if (shape.Line.LinePattern.Value == LinePatternValue.Solid)
                    {
                        // Example customization: change the line color to red
                        shape.Line.LineColor.Value = "#FF0000";

                        // Additional customizations can be added here
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
