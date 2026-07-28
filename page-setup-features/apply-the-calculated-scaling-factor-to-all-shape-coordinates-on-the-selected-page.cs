using System.IO;
using System;
using Aspose.Diagram;

class ScaleShapesOnPage
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the page index (0‑based) you want to work on
            int pageIndex = 0; // change as needed

            // Define the scaling factor (e.g., 1.5 for 150%)
            double scalingFactor = 1.5;

            // Get the target page
            Page page = diagram.Pages[pageIndex];

            // Apply scaling factor to each shape's coordinates and size on the page
            foreach (Shape shape in page.Shapes)
            {
                // Scale position (PinX, PinY)
                shape.XForm.PinX.Value *= scalingFactor;
                shape.XForm.PinY.Value *= scalingFactor;

                // Scale size (Width, Height)
                shape.XForm.Width.Value *= scalingFactor;
                shape.XForm.Height.Value *= scalingFactor;

                // Scale local pin offsets (LocPinX, LocPinY) if they exist
                if (shape.XForm.LocPinX != null)
                    shape.XForm.LocPinX.Value *= scalingFactor;
                if (shape.XForm.LocPinY != null)
                    shape.XForm.LocPinY.Value *= scalingFactor;
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
