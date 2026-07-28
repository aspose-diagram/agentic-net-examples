using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // Connector shape
                    {
                        // Enable line inheritance by copying inherited line values
                        shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                        shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                        shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                    }
                    else
                    {
                        // For non‑connector shapes, set an explicit line color (example: black)
                        shape.Line.LineColor.Value = "#000000";
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
