using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the custom style values
            string fillColor = "#FFCC00";          // Example fill color (orange)
            string shadowColor = "#000000";        // Shadow color (black)
            double shadowTransparency = 0.3;       // 30% transparent
            double shadowOffsetX = 0.1;            // Horizontal offset
            double shadowOffsetY = 0.1;            // Vertical offset

            // Apply the style to every master and each shape within the master
            foreach (Master master in diagram.Masters)
            {
                foreach (Shape shape in master.Shapes)
                {
                    // Set solid fill pattern
                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                    shape.Fill.FillForegnd.Value = fillColor;       // Fill foreground color

                    // Configure simple shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    shape.Fill.ShdwForegnd.Value = shadowColor;     // Shadow color
                    shape.Fill.ShdwForegndTrans.Value = shadowTransparency; // Transparency
                    shape.Fill.ShapeShdwOffsetX.Value = shadowOffsetX; // X offset
                    shape.Fill.ShapeShdwOffsetY.Value = shadowOffsetY; // Y offset
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
