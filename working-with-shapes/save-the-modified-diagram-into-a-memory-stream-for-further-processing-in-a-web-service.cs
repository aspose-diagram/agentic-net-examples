using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram from a file.
            // Replace "input.vsdx" with the actual path to your diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Example modification: add a rectangle shape to the first page.
            Page page = diagram.Pages[0];
            double pinX = 5.0;    // X coordinate (in inches)
            double pinY = 5.0;    // Y coordinate (in inches)
            double width = 2.0;   // Width (in inches)
            double height = 1.0;  // Height (in inches)

            // AddShape returns the shape ID as a long.
            long shapeId = diagram.AddShape(pinX, pinY, width, height, "Rectangle", 0);
            // Retrieve the shape object if further manipulation is needed.
            Shape rectangle = page.Shapes.GetShape(shapeId);

            // Save the modified diagram into a memory stream in VSDX format.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, SaveFileFormat.Vsdx);
                // Reset the stream position to the beginning for downstream processing.
                memoryStream.Position = 0;

                // Example: output the size of the generated stream.
                Console.WriteLine($"Diagram saved to memory stream. Size = {memoryStream.Length} bytes");
                // The memoryStream can now be returned from a web service or further processed.
            }

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
