using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            // Adjust the ID as needed for your specific diagram
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 was not found on the page.");
                return;
            }

            // Set the text direction of the shape.
            // The API provides Horizontal and Vertical options; using Vertical as a placeholder for right‑to‑left.
            shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

            Console.WriteLine("Text direction has been set for the shape.");

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
