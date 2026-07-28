using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (using the provided creation rule)
        Diagram diagram = new Diagram();

        // Access the first page (a diagram always has at least one page)
        Page page = diagram.Pages[0];

        // Define initial position for the text shape
        double pinX = 5.0;   // X coordinate of the text's pin
        double pinY = 5.0;   // Y coordinate of the text's pin

        // Add a text shape with default width/height (these will be overridden)
        Shape textShape = page.AddText(pinX, pinY, 1.0, 1.0, "Custom size text");

        // Set custom width and height for the text block
        double customWidth = 3.0;   // desired width of the text block
        double customHeight = 2.0;  // desired height of the text block

        // TxtWidth and TxtHeight are DoubleValue objects; assign the numeric values
        textShape.TextXForm.TxtWidth.Value = customWidth;
        textShape.TextXForm.TxtHeight.Value = customHeight;

        // Refresh shape data so that internal calculations (e.g., geometry) are updated
        textShape.RefreshData();

        // Retrieve the shape's pin position (center of rotation)
        double shapePinX = textShape.XForm.PinX.Value;
        double shapePinY = textShape.XForm.PinY.Value;

        // Calculate the bounding box of the text block (assuming no rotation)
        double left   = shapePinX - (textShape.TextXForm.TxtWidth.Value  / 2.0);
        double right  = shapePinX + (textShape.TextXForm.TxtWidth.Value  / 2.0);
        double bottom = shapePinY - (textShape.TextXForm.TxtHeight.Value / 2.0);
        double top    = shapePinY + (textShape.TextXForm.TxtHeight.Value / 2.0);

        // Output the bounding box coordinates
        Console.WriteLine("Text Block Bounding Box:");
        Console.WriteLine($"Left   : {left}");
        Console.WriteLine($"Right  : {right}");
        Console.WriteLine($"Bottom : {bottom}");
        Console.WriteLine($"Top    : {top}");

        // Save the diagram (using the provided saving rule)
        diagram.Save("Output.vsdx", SaveFileFormat.Vsdx);
    }
}
