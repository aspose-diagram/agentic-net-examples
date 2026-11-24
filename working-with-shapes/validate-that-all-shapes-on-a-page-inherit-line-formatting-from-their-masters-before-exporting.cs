using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare output path with a suffix
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_validated.vsdx");

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allValid = true;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only shapes that have an associated master
                        if (shape.Master != null)
                        {
                            // Retrieve line formatting from the shape
                            var shapeLineColor = shape.Line?.LineColor?.Value;
                            var shapeLineWeight = shape.Line?.LineWeight?.Value;

                            // Retrieve inherited line formatting
                            var inheritLineColor = shape.InheritLine?.LineColor?.Value;
                            var inheritLineWeight = shape.InheritLine?.LineWeight?.Value;

                            // Validate inheritance: compare color and weight
                            bool colorMatches = shapeLineColor != null && inheritLineColor != null && shapeLineColor == inheritLineColor;
                            bool weightMatches = shapeLineWeight != null && inheritLineWeight != null && shapeLineWeight == inheritLineWeight;

                            if (!colorMatches || !weightMatches)
                            {
                                Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not inherit line formatting from master '{shape.Master.Name}'.");
                                allValid = false;
                            }
                        }
                    }
                }

                // Report validation result
                if (!allValid)
                {
                    Console.WriteLine("Validation failed: some shapes do not inherit line formatting from their masters.");
                }
                else
                {
                    Console.WriteLine("All shapes inherit line formatting from their masters.");
                }

                // Export the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}