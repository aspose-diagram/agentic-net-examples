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

            // Load an existing Visio diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram.
            Page page = diagram.Pages[0];

            // Find the first shape on the page to modify.
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                targetShape = s;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // ----- Geometry modification -----
            // Move the shape to a new position (PinX, PinY) and resize it (Width, Height).
            targetShape.XForm.PinX.Value = 5.0;    // New X coordinate (in inches)
            targetShape.XForm.PinY.Value = 7.0;    // New Y coordinate (in inches)
            targetShape.XForm.Width.Value = 2.5;   // New width (in inches)
            targetShape.XForm.Height.Value = 1.5;  // New height (in inches)

            // ----- Add comment describing the change -----
            string rationale = "Moved shape to (5,7) and resized to 2.5x1.5 inches for layout alignment.";
            page.AddComment(targetShape, rationale);

            // Save the modified diagram.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
