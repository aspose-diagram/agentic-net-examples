using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "CircularLayout.vsdx";

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Insert a new blank page into the diagram
                Page page = new Page();
                page.Name = "Page-1";
                diagram.Pages.Add(page);

                // Parameters for circular layout
                double centerX = 5.0;   // Center X coordinate
                double centerY = 5.0;   // Center Y coordinate
                double radius = 3.0;    // Radius of the circle
                double shapeWidth = 1.0;
                double shapeHeight = 0.5;

                // Create five shapes positioned around the circle
                for (int i = 0; i < 5; i++)
                {
                    double angle = 2 * Math.PI * i / 5; // Evenly spaced angles
                    double posX = centerX + radius * Math.Cos(angle);
                    double posY = centerY + radius * Math.Sin(angle);

                    // Draw a rectangle shape at the calculated position
                    long shapeId = page.DrawRectangle(posX, posY, shapeWidth, shapeHeight);

                    // Retrieve the shape object using the returned ID
                    Shape shape = page.Shapes.GetShape((int)shapeId);

                    // Clear any existing text and add new label
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Shape {i + 1}"));
                }

                // Save the diagram to VSDX format with the required second argument
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}