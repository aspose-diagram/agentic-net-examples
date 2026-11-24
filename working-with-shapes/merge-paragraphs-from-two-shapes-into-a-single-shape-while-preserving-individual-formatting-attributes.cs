using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // IDs of the two shapes to merge (replace with actual IDs)
        long shapeId1 = 1; // first shape ID
        long shapeId2 = 2; // second shape ID

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume both shapes are on the first page
                Page page = diagram.Pages[0];

                // Retrieve the shapes by their IDs
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Validate that both shapes were found
                if (shape1 == null || shape2 == null)
                {
                    Console.Error.WriteLine("One or both shape IDs were not found on the first page.");
                    return;
                }

                // Merge paragraphs from shape2 into shape1 while preserving formatting
                for (int i = 0; i < shape2.Paras.Count; i++)
                {
                    // Retrieve the source paragraph
                    var sourcePara = shape2.Paras[i];

                    // Clone the paragraph to keep its formatting intact
                    var clonedPara = (Para)sourcePara.Clone();

                    // Add the cloned paragraph to the first shape's paragraph collection
                    shape1.Paras.Add(clonedPara);
                }

                // Optionally hide the second shape after merging
                shape2.Del = BOOL.True;

                // Save the modified diagram to the output file (must specify format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Paragraphs merged successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}