using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // OLE objects are represented by shapes that contain ForeignData with ObjectData
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Retrieve position (PinX, PinY) and size (Width, Height) from the shape's XForm
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate bounding box coordinates
                        double left   = pinX - width / 2;
                        double right  = pinX + width / 2;
                        double top    = pinY + height / 2;
                        double bottom = pinY - height / 2;

                        // Log the bounding box for layout analysis
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, OLE Bounding Box => Left: {left}, Right: {right}, Top: {top}, Bottom: {bottom}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
