using System.IO;
using System;
using Aspose.Diagram;

class AdjustTextBlock
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page and a specific shape (ID = 1)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1);

            // Example: increase the shape's width by 20%
            double originalWidth = shape.XForm.Width.Value;
            double newWidth = originalWidth * 1.20;

            // Apply the new width to the shape
            shape.SetWidth(newWidth);

            // Adjust TxtWidth proportionally to the new shape width
            // Keep TxtHeight unchanged (no modification needed)
            shape.TextXForm.TxtWidth = new DoubleValue { Value = newWidth };

            // Refresh shape data so that Visio recalculates dependent values
            shape.RefreshData();

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
