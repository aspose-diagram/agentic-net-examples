using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define a tolerance for dimension comparison (in inches)
        const double tolerance = 0.001;

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page.
            // Parameters: pinX, pinY, width, height, master name.
            // Using arbitrary initial size; it will be changed by SetHeight/SetWidth.
            double initialPinX = 2.0;
            double initialPinY = 2.0;
            double initialWidth = 1.0;
            double initialHeight = 1.0;
            long shapeId = page.AddShape(initialPinX, initialPinY, initialWidth, initialHeight, "Rectangle");

            // Retrieve the shape instance using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the shape after creation.");
                return;
            }

            // -------------------- Test SetHeight --------------------
            double newHeight = 3.5; // inches
            shape.SetHeight(newHeight);

            // Verify the height was set within tolerance
            double actualHeight = shape.XForm.Height.Value;
            if (Math.Abs(actualHeight - newHeight) > tolerance)
            {
                throw new Exception($"SetHeight test failed. Expected: {newHeight}, Actual: {actualHeight}");
            }
            Console.WriteLine("SetHeight test passed.");

            // -------------------- Test SetWidth --------------------
            double newWidth = 4.25; // inches
            shape.SetWidth(newWidth);

            // Verify the width was set within tolerance
            double actualWidth = shape.XForm.Width.Value;
            if (Math.Abs(actualWidth - newWidth) > tolerance)
            {
                throw new Exception($"SetWidth test failed. Expected: {newWidth}, Actual: {actualWidth}");
            }
            Console.WriteLine("SetWidth test passed.");

            // Optional: Save the diagram to verify visually (not required for the test)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}