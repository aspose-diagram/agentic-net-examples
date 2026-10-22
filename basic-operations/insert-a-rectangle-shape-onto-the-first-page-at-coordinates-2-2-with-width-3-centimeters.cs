using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output Visio file path.
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram. This automatically contains a default page.
            Diagram diagram = new Diagram();

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page (index 0).
            Page firstPage = diagram.Pages[0];

            // Convert centimeters to inches (Aspose.Diagram uses inches for geometry).
            double cmToInch = 0.393701;
            double pinX = 2.0 * cmToInch;   // X coordinate (2 cm)
            double pinY = 2.0 * cmToInch;   // Y coordinate (2 cm)
            double widthInches = 3.0 * cmToInch; // Width (3 cm)

            // Add a rectangle shape to the first page.
            // The AddShape method returns the shape ID (long).
            long shapeId = firstPage.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the newly added shape using its ID.
            Shape rectangle = firstPage.Shapes.GetShape(shapeId);
            if (rectangle == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added rectangle shape.");
                return;
            }

            // Set the width of the rectangle. Height is left as default.
            rectangle.XForm.Width.Value = widthInches;

            // Save the diagram to a VSDX file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}