using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define dark gray color (hex) and line weight (1.5 points = 0.0208333 inches)
            const string darkGrayHex = "#A9A9A9";
            const double lineWeightInInches = 1.5 / 72.0; // 1 point = 1/72 inch

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Set line color
                        shape.Line.LineColor.Value = darkGrayHex;
                        // Set line thickness
                        shape.Line.LineWeight.Value = lineWeightInInches;
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
