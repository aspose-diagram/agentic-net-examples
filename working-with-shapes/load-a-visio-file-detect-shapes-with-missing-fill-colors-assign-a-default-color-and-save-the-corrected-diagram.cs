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

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_corrected.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Default fill color to apply (hex string)
            const string defaultColor = "#FFCC00";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape lacks a fill color
                    bool missingFill = shape.Fill.FillPattern.Value == 0 ||
                                       string.IsNullOrWhiteSpace(shape.Fill.FillForegnd.Value);

                    if (missingFill)
                    {
                        // Set a solid fill pattern and assign the default color
                        shape.Fill.FillPattern.Value = 1; // Solid fill
                        shape.Fill.FillForegnd.Value = defaultColor;
                    }
                }
            }

            // Save the corrected diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
