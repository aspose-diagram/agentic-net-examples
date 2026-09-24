using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define a palette of solid fill colors (hex strings)
            string[] palette = new string[]
            {
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF", // Blue
                "#FFFF00", // Yellow
                "#FF00FF", // Magenta
                "#00FFFF"  // Cyan
            };

            int colorIndex = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set fill pattern to solid (value 1)
                    shape.Fill.FillPattern.Value = 1;

                    // Assign a solid fill color from the palette (cycle through)
                    shape.Fill.FillForegnd.Value = palette[colorIndex % palette.Length];
                    colorIndex++;
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
