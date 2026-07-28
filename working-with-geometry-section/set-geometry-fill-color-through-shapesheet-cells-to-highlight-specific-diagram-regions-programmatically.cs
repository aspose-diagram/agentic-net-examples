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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Color to use for highlighting (bright yellow)
            string highlightColor = "#FFFF00";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Highlight shapes whose universal name is "Process"
                    if (shape.NameU != null && shape.NameU.Equals("Process", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set solid fill pattern (1 = solid)
                        shape.Fill.FillPattern.Value = 1;
                        // Apply the highlight color to the foreground fill
                        shape.Fill.FillForegnd.Value = highlightColor;
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
