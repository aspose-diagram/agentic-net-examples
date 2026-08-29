using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // ID of the shape whose X coordinate we want to change
            long targetShapeId = 5; // example ID; replace with actual ID
            // Desired new X coordinate (PinX) in inches
            double newPinX = 5.0;

            // Access the first page (or specify a different page as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Preserve the current Y coordinate (PinY)
            double currentPinY = shape.XForm.PinY.Value;

            // Set the new X coordinate while keeping Y unchanged
            shape.XForm.PinX.Value = newPinX;
            shape.XForm.PinY.Value = currentPinY; // optional, reinforces unchanged Y

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape X coordinate updated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
