using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, optional list of shape IDs
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [shapeId1 shapeId2 ...]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs if provided
        HashSet<long> targetShapeIds = new HashSet<long>();
        for (int i = 2; i < args.Length; i++)
        {
            if (long.TryParse(args[i], out long id))
            {
                targetShapeIds.Add(id);
            }
            else
            {
                Console.Error.WriteLine($"Invalid shape ID ignored: {args[i]}");
            }
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // If no specific IDs were supplied, enable inheritance for all shapes
                bool applyToAll = targetShapeIds.Count == 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;

                        if (applyToAll || targetShapeIds.Contains(shapeId))
                        {
                            // Enable fill inheritance by setting the user function to INHERITED()
                            // This follows the documented approach for enabling inheritance.
                            shape.Fill.FillForegnd.Ufe.F = "INHERITED()";

                            // Optionally, also inherit background fill if needed:
                            // shape.Fill.FillBkgnd.Ufe.F = "INHERITED()";

                            Console.WriteLine($"Enabled fill inheritance for shape ID {shapeId} on page \"{page.NameU}\".");
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}