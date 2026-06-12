using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle rule)
        // {CreateDiagram}
        Diagram diagram = new Diagram();

        // Define custom position and size for the text block
        double pinX = 5.0;      // X coordinate of the text shape pin
        double pinY = 5.0;      // Y coordinate of the text shape pin
        double customWidth = 3.0;   // Desired width of the text block
        double customHeight = 2.0;  // Desired height of the text block
        string text = "Sample Text";

        // Add a text shape with the custom width and height (method rule)
        // {AddText}
        Shape textShape = diagram.Pages[0].AddText(pinX, pinY, customWidth, customHeight, text);

        // Ensure the TextXForm reflects the custom dimensions
        textShape.TextXForm.TxtWidth.Value = customWidth;
        textShape.TextXForm.TxtHeight.Value = customHeight;

        // Refresh shape data so that internal calculations are up‑to‑date
        textShape.RefreshData();

        // Retrieve the shape's pin position (center of rotation)
        double shapePinX = textShape.XForm.PinX.Value;
        double shapePinY = textShape.XForm.PinY.Value;

        // Retrieve the actual text block dimensions
        double txtWidth = textShape.TextXForm.TxtWidth.Value;
        double txtHeight = textShape.TextXForm.TxtHeight.Value;

        // Calculate bounding box coordinates
        double left   = shapePinX - txtWidth / 2.0;
        double right  = shapePinX + txtWidth / 2.0;
        double top    = shapePinY + txtHeight / 2.0;
        double bottom = shapePinY - txtHeight / 2.0;

        // Output the bounding box values
        Console.WriteLine($"Bounding Box:");
        Console.WriteLine($"Left   = {left}");
        Console.WriteLine($"Right  = {right}");
        Console.WriteLine($"Top    = {top}");
        Console.WriteLine($"Bottom = {bottom}");

        // Save the diagram (lifecycle rule)
        // {SaveDiagram}
        diagram.Save("Result.vsdx", SaveFileFormat.Vsdx);
    }
}
