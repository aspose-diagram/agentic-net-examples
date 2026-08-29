using System;
using System.IO;

// Load the main Visio diagram (replace with your actual file path)
using Aspose.Diagram;
using System;
using System.Linq;

class ApplyCustomStyle
{
    static void Main()
    {
        try
        {

            // Load the target diagram that contains the shape with ID 12
            Diagram diagram = new Diagram("input.vsdx"); // loads VSDX by default

            // Load the custom stencil (.vss) that defines the stylesheet
            Diagram stencil = new Diagram("custom.vss", LoadFileFormat.Vss);

            // Find the stylesheet named "CustomStyle" in the stencil
            StyleSheet customStyle = stencil.StyleSheets.FirstOrDefault(s => s.Name == "CustomStyle" || s.NameU == "CustomStyle");
            if (customStyle == null)
            {
                Console.WriteLine("CustomStyle stylesheet not found in the stencil.");
                return;
            }

            // Locate the shape with ID 12 in the diagram's active page
            Shape targetShape = diagram.ActivePage.Shapes.FirstOrDefault(sh => sh.ID == 12);
            if (targetShape == null)
            {
                Console.WriteLine("Shape with ID 12 not found in the diagram.");
                return;
            }

            // Apply the stylesheet to the shape's fill, line, and text formatting
            targetShape.FillStyle = customStyle;
            targetShape.LineStyle = customStyle;
            targetShape.TextStyle = customStyle;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
