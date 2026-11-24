using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new blank diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (active) page
            Page page = diagram.ActivePage;

            // Convert centimeters to inches (Aspose.Diagram uses inches for geometry)
            double cmToInch = 1.0 / 2.54;
            double pinX = 2.0 * cmToInch; // X coordinate (2 cm)
            double pinY = 2.0 * cmToInch; // Y coordinate (2 cm)
            double widthInches = 3.0 * cmToInch; // Width (3 cm)

            // Add a rectangle shape using the built‑in "Rectangle" master.
            // The fourth argument 'isCalculate' must be a bool; false means no immediate recalculation.
            long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the newly added shape to modify its dimensions.
            Shape rectShape = page.Shapes.GetShape(shapeId);
            if (rectShape != null)
            {
                // Set the width to 3 cm (converted to inches). Height remains default.
                rectShape.XForm.Width.Value = widthInches;
            }
            else
            {
                Console.Error.WriteLine("Failed to retrieve the newly added rectangle shape.");
                return;
            }

            // Save the diagram to a VSDX file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}