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

            // Define the custom style parameters
            string fillColor = "#FFCC00";          // Solid fill color
            string shadowColor = "#808080";        // Shadow color
            double shadowOffset = 0.1;             // Shadow offset in inches
            double shadowTransparency = 0.3;       // 30% transparent shadow

            // Apply the style to every master in the document
            foreach (Master master in diagram.Masters)
            {
                // Each master can contain multiple shapes; style each one
                foreach (Shape shape in master.Shapes)
                {
                    // Set solid fill
                    shape.Fill.FillPattern.Value = 1;               // 1 = solid fill
                    shape.Fill.FillForegnd.Value = fillColor;       // Fill foreground color

                    // Configure a simple drop shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    shape.Fill.ShdwForegnd.Value = shadowColor;      // Shadow color
                    shape.Fill.ShdwForegndTrans.Value = shadowTransparency; // Transparency (0 = opaque, 1 = fully transparent)
                    shape.Fill.ShapeShdwOffsetX.Value = shadowOffset; // Horizontal offset
                    shape.Fill.ShapeShdwOffsetY.Value = shadowOffset; // Vertical offset
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
