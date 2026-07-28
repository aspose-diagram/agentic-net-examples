using Aspose.Diagram;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure autospace options (distance in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical = 0.5    // vertical spacing
            };

            // Apply autospace to all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, options);
            }

            // Create CSV file to store shape IDs and their new PinX/PinY coordinates
            using (StreamWriter writer = new StreamWriter("ShapeCoordinates.csv"))
            {
                // CSV header
                writer.WriteLine("ShapeID,PinX,PinY");

                // Write each shape's ID and coordinates after autospace
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // shape.ID is the unique identifier
                        // shape.XForm.PinX and shape.XForm.PinY hold the coordinates
                        writer.WriteLine($"{shape.ID},{shape.XForm.PinX},{shape.XForm.PinY}");
                    }
                }
            }

            // Save the modified diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
