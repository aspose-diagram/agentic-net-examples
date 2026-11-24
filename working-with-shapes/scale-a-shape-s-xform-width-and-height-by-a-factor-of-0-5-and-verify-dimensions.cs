using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the target shape was found
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the first non-deleted shape named "Rectangle"
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check shape name (NameU) – adjust as needed for your diagram
                    if (shape.NameU == "Rectangle")
                    {
                        shapeFound = true;

                        // Store original dimensions
                        double originalWidth = shape.XForm.Width.Value;
                        double originalHeight = shape.XForm.Height.Value;

                        // Scale dimensions by 0.5
                        double factor = 0.5;
                        shape.XForm.Width.Value = originalWidth * factor;
                        shape.XForm.Height.Value = originalHeight * factor;

                        // Verify the scaling
                        double expectedWidth = originalWidth * factor;
                        double expectedHeight = originalHeight * factor;
                        double tolerance = 0.0001; // tolerance for floating‑point comparison

                        if (Math.Abs(shape.XForm.Width.Value - expectedWidth) > tolerance ||
                            Math.Abs(shape.XForm.Height.Value - expectedHeight) > tolerance)
                        {
                            throw new Exception("Scaling verification failed: dimensions do not match expected values.");
                        }

                        Console.WriteLine($"Shape '{shape.NameU}' scaled successfully. New Width={shape.XForm.Width.Value}, New Height={shape.XForm.Height.Value}");
                        // Exit after processing the first matching shape
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine("Target shape named 'Rectangle' was not found in the diagram.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}