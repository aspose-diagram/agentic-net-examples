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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Store original dimensions
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Scale dimensions by a factor of 0.5
            shape.SetWidth(originalWidth * 0.5);
            shape.SetHeight(originalHeight * 0.5);

            // Verify the new dimensions
            double newWidth = shape.XForm.Width.Value;
            double newHeight = shape.XForm.Height.Value;

            Console.WriteLine($"Width scaled: {originalWidth} -> {newWidth}");
            Console.WriteLine($"Height scaled: {originalHeight} -> {newHeight}");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
