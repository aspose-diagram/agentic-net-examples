using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class ScaleShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Assume we work with the first shape on the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Store original dimensions
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Scale factor
            double scaleFactor = 0.5;

            // Calculate new dimensions
            double newWidth = originalWidth * scaleFactor;
            double newHeight = originalHeight * scaleFactor;

            // Apply new dimensions using Shape.SetWidth and Shape.SetHeight
            shape.SetWidth(newWidth);
            shape.SetHeight(newHeight);

            // Verify that the dimensions were updated correctly
            double updatedWidth = shape.XForm.Width.Value;
            double updatedHeight = shape.XForm.Height.Value;

            Debug.Assert(Math.Abs(updatedWidth - newWidth) < 0.0001, "Width scaling failed.");
            Debug.Assert(Math.Abs(updatedHeight - newHeight) < 0.0001, "Height scaling failed.");

            // Save the modified diagram
            diagram.Save(@"C:\Output\sample_scaled.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
