using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <masterName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetMasterName = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that use the specified master.
                    List<Shape> matchingShapes = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            matchingShapes.Add(shape);
                        }
                    }

                    // If there are matching shapes, apply auto‑spacing.
                    if (matchingShapes.Count > 0)
                    {
                        // Configure auto‑spacing options.
                        AutoSpaceOptions options = new AutoSpaceOptions();
                        // Set horizontal and vertical distances (example values).
                        options.DistanceInHorizontal = 0.5; // inches
                        options.DistanceInVertical = 0.5;   // inches

                        // Apply auto‑spacing to the page's shape collection.
                        // The API requires a ShapeCollection; we use the page's Shapes.
                        page.AutoSpaceShapes(page.Shapes, options);
                    }
                }

                // Save the modified diagram to the output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}