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
        string outputPath = "merged_output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // IDs of the two source shapes to merge (replace with actual IDs)
        long shapeId1 = 1; // first shape ID
        long shapeId2 = 2; // second shape ID

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Retrieve the two source shapes
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            if (shape1 == null || shape2 == null)
            {
                Console.Error.WriteLine("One or both source shapes could not be found.");
                return;
            }

            // Create a new rectangle shape that will hold the merged paragraphs
            // Parameters: pinX, pinY, master name, page index
            long newShapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            Shape targetShape = page.Shapes.GetShape(newShapeId);

            if (targetShape == null)
            {
                Console.Error.WriteLine("Failed to create target shape.");
                return;
            }

            // OPTIONAL: Clear any existing paragraphs/characters in the target shape
            // (If Clear() is not available, this block can be omitted)
            try { targetShape.Paras.Clear(); } catch { }
            try { targetShape.Chars.Clear(); } catch { }

            // Helper method to copy paragraphs and characters from a source shape
            void CopyContent(Shape source)
            {
                // Copy paragraphs preserving their formatting
                for (int i = 0; i < source.Paras.Count; i++)
                {
                    // Add the paragraph object directly; this retains its formatting
                    targetShape.Paras.Add(source.Paras[i]);
                }

                // Copy character runs preserving their formatting
                for (int i = 0; i < source.Chars.Count; i++)
                {
                    targetShape.Chars.Add(source.Chars[i]);
                }
            }

            // Merge paragraphs from the first shape
            CopyContent(shape1);
            // Merge paragraphs from the second shape
            CopyContent(shape2);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}