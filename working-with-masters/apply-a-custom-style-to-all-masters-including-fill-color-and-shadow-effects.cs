using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (must exist)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the desired fill color and shadow settings
            string fillColor = "#FFCC00";          // Example solid fill color
            string shadowColor = "#000000";        // Shadow color (black)
            double shadowTransparency = 0.3;       // 30% transparent
            double shadowOffsetX = 0.1;            // Horizontal offset
            double shadowOffsetY = 0.1;            // Vertical offset

            // Iterate over all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Apply the style to each shape contained in the master
                foreach (Shape shape in master.Shapes)
                {
                    // Set a solid fill pattern
                    shape.Fill.FillPattern.Value = 1;               // 1 = solid fill
                    shape.Fill.FillForegnd.Value = fillColor;       // Fill foreground color

                    // Configure a simple shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    shape.Fill.ShdwForegnd.Value = shadowColor;     // Shadow color
                    shape.Fill.ShdwForegndTrans.Value = shadowTransparency; // Transparency (0‑1)
                    shape.Fill.ShapeShdwOffsetX.Value = shadowOffsetX; // X offset
                    shape.Fill.ShapeShdwOffsetY.Value = shadowOffsetY; // Y offset
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
