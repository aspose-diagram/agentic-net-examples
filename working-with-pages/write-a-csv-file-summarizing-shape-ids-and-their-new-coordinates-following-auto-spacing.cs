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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string csvPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(csvPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a using block.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and apply auto‑spacing.
                foreach (Page page in diagram.Pages)
                {
                    // Prepare auto‑spacing options (example distances in inches).
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    options.DistanceInHorizontal = 0.5; // 0.5 inch horizontal gap
                    options.DistanceInVertical = 0.5;   // 0.5 inch vertical gap

                    try
                    {
                        // Auto‑space the shapes on the current page.
                        page.AutoSpaceShapes(page.Shapes, options);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Auto‑spacing failed on page '{page.Name}': {ex.Message}");
                        // Continue with next page instead of aborting the whole process.
                        continue;
                    }
                }

                // Write the CSV file with shape IDs and their new coordinates.
                try
                {
                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        // CSV header.
                        writer.WriteLine("ShapeID,PinX,PinY");

                        // Process each page and each shape.
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes.
                                if (shape.Del == BOOL.True)
                                    continue;

                                long shapeId = shape.ID;
                                double pinX = shape.XForm.PinX.Value;
                                double pinY = shape.XForm.PinY.Value;

                                // Write a CSV line.
                                writer.WriteLine($"{shapeId},{pinX},{pinY}");
                            }
                        }
                    }

                    Console.WriteLine($"CSV summary written to: {csvPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write CSV file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}