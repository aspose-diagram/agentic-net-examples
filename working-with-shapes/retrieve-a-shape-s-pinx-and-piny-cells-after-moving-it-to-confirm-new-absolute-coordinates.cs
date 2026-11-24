using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Verify the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // ----- Retrieve original absolute coordinates -----
                double originalPinX = shape.XForm.PinX.Value;
                double originalPinY = shape.XForm.PinY.Value;
                Console.WriteLine($"Original PinX: {originalPinX}, PinY: {originalPinY}");

                // ----- Move the shape to a new location -----
                // Example offsets; adjust as needed
                double offsetX = 2.0;
                double offsetY = 1.5;
                shape.XForm.PinX.Value = originalPinX + offsetX;
                shape.XForm.PinY.Value = originalPinY + offsetY;

                // ----- Retrieve new absolute coordinates -----
                double newPinX = shape.XForm.PinX.Value;
                double newPinY = shape.XForm.PinY.Value;
                Console.WriteLine($"Moved PinX: {newPinX}, PinY: {newPinY}");

                // ----- Save the modified diagram -----
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}