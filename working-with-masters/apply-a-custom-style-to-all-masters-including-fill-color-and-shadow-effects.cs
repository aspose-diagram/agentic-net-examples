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

            // Custom style settings
            string fillColor = "#FFCC00";          // Solid fill color
            string shadowColor = "#808080";        // Shadow color
            double shadowOffsetX = 0.1;            // Horizontal shadow offset (in inches)
            double shadowOffsetY = 0.1;            // Vertical shadow offset (in inches)
            double shadowTransparency = 0.3;      // 30% transparent shadow

            // Apply the style to every master and each shape within the master
            foreach (Master master in diagram.Masters)
            {
                foreach (Shape shape in master.Shapes)
                {
                    // Fill settings
                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                    shape.Fill.FillForegnd.Value = fillColor;       // Fill color

                    // Shadow settings
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple; // Simple shadow
                    shape.Fill.ShdwForegnd.Value = shadowColor;                // Shadow color
                    shape.Fill.ShdwForegndTrans.Value = shadowTransparency;   // Shadow transparency
                    shape.Fill.ShapeShdwOffsetX.Value = shadowOffsetX;         // Shadow offset X
                    shape.Fill.ShapeShdwOffsetY.Value = shadowOffsetY;         // Shadow offset Y
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
