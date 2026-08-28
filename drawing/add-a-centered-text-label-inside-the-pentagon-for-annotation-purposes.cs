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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page of the diagram
            Page page = diagram.Pages[0];

            // Define position and size for the pentagon (in inches)
            double pinX = 5.0;   // X coordinate of the shape's pin (center)
            double pinY = 5.0;   // Y coordinate of the shape's pin (center)
            double width = 2.0;  // Width of the pentagon
            double height = 2.0; // Height of the pentagon

            // Add a pentagon shape using the built‑in master named "Pentagon"
            long pentagonId = page.AddShape(pinX, pinY, width, height, "Pentagon");
            Shape pentagon = page.Shapes.GetShape(pentagonId);

            // Clear any existing text and add the annotation label
            pentagon.Text.Value.Clear();
            pentagon.Text.Value.Add(new Txt("Annotation"));

            // Center the text horizontally within the shape
            if (pentagon.Paras.Count > 0)
            {
                pentagon.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // Center the text vertically within the shape
            pentagon.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Save the diagram to a VSDX file
            diagram.Save("PentagonWithLabel.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
