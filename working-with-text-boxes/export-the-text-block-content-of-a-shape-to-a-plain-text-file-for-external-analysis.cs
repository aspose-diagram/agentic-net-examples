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

            // Path to the Visio file to be processed
            string visioPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape whose text block you want to export.
            // Here we use a known shape ID; replace with the appropriate ID or lookup logic.
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} was not found on the page.");
                return;
            }

            // Retrieve the plain‑text content of the shape.
            // shape.Text.Value.Text concatenates all text runs into a single string.
            string plainText = shape.Text.Value.Text;

            // Define the output file path for the exported text.
            string outputPath = "shape_text.txt";

            // Write the extracted text to the file.
            File.WriteAllText(outputPath, plainText);

            Console.WriteLine($"Shape text has been exported to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
