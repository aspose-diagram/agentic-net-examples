using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page to work with.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Add a new shape to the first page using the built‑in "Rectangle" master.
                // The AddShape method returns the shape ID (long).
                long shapeId = diagram.AddShape(new Shape(), "Rectangle", 0);

                // Retrieve the newly added shape object.
                Shape newShape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Add some text to the shape to demonstrate modification.
                newShape.Text.Value.Add(new Txt("Added via Aspose.Diagram"));

                // Save the modified diagram to the new file using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // After saving, compare file sizes to verify that content was added.
            long originalSize = new FileInfo(inputPath).Length;
            long newSize = new FileInfo(outputPath).Length;

            Console.WriteLine($"Original file size: {originalSize} bytes");
            Console.WriteLine($"New file size: {newSize} bytes");

            if (newSize > originalSize)
            {
                Console.WriteLine("The new file size reflects the added content.");
            }
            else
            {
                Console.WriteLine("Warning: The new file size does not appear larger than the original.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}