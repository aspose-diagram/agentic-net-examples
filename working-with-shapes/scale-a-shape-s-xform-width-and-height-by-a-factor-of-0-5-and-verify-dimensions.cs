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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Capture original dimensions
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Define scaling factor
            double scaleFactor = 0.5;

            // Apply scaling to width and height
            shape.SetWidth(originalWidth * scaleFactor);
            shape.SetHeight(originalHeight * scaleFactor);

            // Retrieve new dimensions for verification
            double newWidth = shape.XForm.Width.Value;
            double newHeight = shape.XForm.Height.Value;

            // Output verification results
            Console.WriteLine($"Original Width: {originalWidth}, Scaled Width: {newWidth}");
            Console.WriteLine($"Original Height: {originalHeight}, Scaled Height: {newHeight}");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
