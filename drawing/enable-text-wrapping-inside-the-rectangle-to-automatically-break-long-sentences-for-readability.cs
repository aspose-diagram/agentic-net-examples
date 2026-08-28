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

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape (master name "Rectangle") at position (5,5) inches with width and height of 4 inches
            long rectId = page.AddShape(5.0, 5.0, 4.0, 4.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Clear any existing text (optional)
            rectShape.Text.Value.Clear();

            // Add a long sentence that should wrap inside the rectangle
            string longText = "This is a very long sentence that will automatically wrap inside the rectangle shape to improve readability and demonstrate text wrapping functionality.";
            rectShape.Text.Value.Add(new Txt(longText));

            // Note: Text wrapping is automatically handled based on shape size; explicit Wrap cell is not exposed in the API.

            // Optionally adjust margins so the text does not touch the shape borders
            rectShape.TextBlock.LeftMargin.Value = 0.1;   // 0.1 inch left margin
            rectShape.TextBlock.RightMargin.Value = 0.1;  // 0.1 inch right margin
            rectShape.TextBlock.TopMargin.Value = 0.1;    // 0.1 inch top margin
            rectShape.TextBlock.BottomMargin.Value = 0.1; // 0.1 inch bottom margin

            // Save the diagram as a PNG image to verify the result
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("WrappedRectangle.png", saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}