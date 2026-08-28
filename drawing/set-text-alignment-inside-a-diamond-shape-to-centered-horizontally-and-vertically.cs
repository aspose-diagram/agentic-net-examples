using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the default first page
            Page page = diagram.Pages[0];

            // Add a diamond shape at (5,5) inches
            long diamondId = page.AddShape(5.0, 5.0, "Diamond");

            // Retrieve the shape instance
            Shape diamond = page.Shapes.GetShape(diamondId);

            // Set the shape's text
            diamond.Text.Value.Clear();
            diamond.Text.Value.Add(new Txt("Centered Text"));

            // Center text horizontally
            diamond.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // Center text vertically
            diamond.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Save the diagram
            diagram.Save("DiamondAligned.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
