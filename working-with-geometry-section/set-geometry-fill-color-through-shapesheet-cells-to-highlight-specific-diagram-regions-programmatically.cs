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

            // Color to use for highlighting (hexadecimal RGB)
            string highlightColor = "#FFFF00"; // Yellow

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example condition: highlight shapes whose universal name contains "Process"
                    if (shape.NameU != null && shape.NameU.IndexOf("Process", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Set solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // 1 = solid fill
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
