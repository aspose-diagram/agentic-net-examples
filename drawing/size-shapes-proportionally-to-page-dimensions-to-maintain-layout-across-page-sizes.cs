using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Desired page size (in inches). Example: Letter landscape 11 x 8.5 inches
            double targetPageWidth = 11.0;
            double targetPageHeight = 8.5;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Original page dimensions
                double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute scaling factors for X and Y axes
                double scaleX = targetPageWidth / originalWidth;
                double scaleY = targetPageHeight / originalHeight;

                // Use uniform scaling to preserve aspect ratio (optional)
                double uniformScale = Math.Min(scaleX, scaleY);
                scaleX = uniformScale;
                scaleY = uniformScale;

                // Adjust each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Scale width and height
                    shape.XForm.Width.Value *= scaleX;
                    shape.XForm.Height.Value *= scaleY;

                    // Reposition the shape's PinX and PinY to keep relative layout
                    // Translate Pin to origin, scale, then translate back to new page center
                    double offsetX = shape.XForm.PinX.Value - (originalWidth / 2.0);
                    double offsetY = shape.XForm.PinY.Value - (originalHeight / 2.0);

                    shape.XForm.PinX.Value = (offsetX * scaleX) + (targetPageWidth / 2.0);
                    shape.XForm.PinY.Value = (offsetY * scaleY) + (targetPageHeight / 2.0);
                }

                // Update the page size to the target dimensions
                page.PageSheet.PageProps.PageWidth.Value = targetPageWidth;
                page.PageSheet.PageProps.PageHeight.Value = targetPageHeight;
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Dispose the diagram to release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
