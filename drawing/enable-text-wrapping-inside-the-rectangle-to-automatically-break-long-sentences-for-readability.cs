using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape to the page (PinX, PinY, Width, Height, master name)
            long rectShapeId = page.AddShape(5.0, 5.0, 3.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Clear any existing text and add a long sentence
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("This is a very long sentence that should automatically wrap inside the rectangle shape to improve readability."));

            // Adjust margins to give the text some padding
            double marginInches = 0.1; // 0.1 inch margin
            rectShape.TextBlock.LeftMargin.Value = marginInches;
            rectShape.TextBlock.RightMargin.Value = marginInches;
            rectShape.TextBlock.TopMargin.Value = marginInches;
            rectShape.TextBlock.BottomMargin.Value = marginInches;

            // Save the diagram to a VSDX file
            diagram.Save("WrappedTextDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}