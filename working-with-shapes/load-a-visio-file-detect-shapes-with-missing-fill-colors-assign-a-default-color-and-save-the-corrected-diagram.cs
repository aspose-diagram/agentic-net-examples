using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_corrected.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Default fill color to apply when a shape has no fill
            const string defaultFillColor = "#FFCC00";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current fill foreground color
                    string currentFill = shape.Fill.FillForegnd.Value;

                    // If the fill color is missing or empty, assign the default color
                    if (string.IsNullOrWhiteSpace(currentFill))
                    {
                        shape.Fill.FillForegnd.Value = defaultFillColor;
                        // Ensure the fill pattern is solid (1 = solid)
                        shape.Fill.FillPattern.Value = 1;
                    }
                }
            }

            // Save the corrected diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
