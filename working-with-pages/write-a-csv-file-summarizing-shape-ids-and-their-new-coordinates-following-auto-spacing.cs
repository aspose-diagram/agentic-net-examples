using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Prepare auto‑spacing options (default distances can be used).
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    // Example: set horizontal and vertical spacing if needed.
                    // options.HorizontalSpacing = 0.5;
                    // options.VerticalSpacing = 0.5;

                    // Auto‑space all shapes on the current page.
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // After auto‑spacing, write shape IDs and their new coordinates to CSV.
                using (StreamWriter writer = new StreamWriter(outputCsvPath))
                {
                    // Write CSV header.
                    writer.WriteLine("ShapeID,PinX,PinY");

                    // Loop through all pages and shapes to collect data.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the shape's ID and its pin (center) coordinates.
                            long shapeId = shape.ID;
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;

                            // Write a CSV line for the current shape.
                            writer.WriteLine($"{shapeId},{pinX},{pinY}");
                        }
                    }
                }

                // Optionally, save the diagram unchanged (not required for CSV output).
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}