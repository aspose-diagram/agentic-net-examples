using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape has an XForm object (required for position data)
                    if (shape.XForm == null)
                        continue; // Skip shapes without transformation data

                    // Check that both LocPinX and LocPinY cells are present
                    // If either is missing, the shape cannot provide absolute coordinates
                    if (shape.XForm.LocPinX == null || shape.XForm.LocPinY == null)
                        continue; // Skip this shape safely

                    try
                    {
                        // Retrieve the shape's PinX and PinY (position of the shape's pin on the page)
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Retrieve the local pin offsets (relative to the shape's origin)
                        double locPinX = shape.XForm.LocPinX.Value;
                        double locPinY = shape.XForm.LocPinY.Value;

                        // Calculate absolute coordinates of the shape's origin
                        double absoluteX = pinX - locPinX;
                        double absoluteY = pinY - locPinY;

                        // Example operation: move the shape to a new absolute position (offset by +1 inch)
                        shape.MoveTo(absoluteX + 1.0, absoluteY + 1.0);

                        // Refresh shape data after moving
                        shape.RefreshData();
                    }
                    catch (Exception ex)
                    {
                        // If any unexpected error occurs (e.g., formula evaluation failure),
                        // skip the shape and continue processing the rest.
                        // Logging can be added here if needed.
                        continue;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
