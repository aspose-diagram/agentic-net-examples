using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Log total number of gluing relationships BEFORE modifications
                // ------------------------------------------------------------
                int glueCountBefore = GetTotalGluingRelationships(diagram);
                Console.WriteLine($"Gluing relationships before modifications: {glueCountBefore}");

                // ------------------------------------------------------------
                // Example modification: glue two shapes on the first page
                // ------------------------------------------------------------
                // Retrieve the first page (ensure at least one page exists)
                if (diagram.Pages.Count > 0)
                {
                    Page page = diagram.Pages[0];

                    // Example shape IDs – replace with actual IDs as needed
                    long shapeId1 = 1;
                    long shapeId2 = 2;

                    // Attempt to glue the shapes using the Center connection point
                    // Wrap in try/catch to handle possible errors (e.g., invalid IDs)
                    try
                    {
                        page.GlueShapes(shapeId1, ConnectionPointPlace.Center, shapeId2);
                        Console.WriteLine($"Glued shape {shapeId1} to shape {shapeId2} on page '{page.Name}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error gluing shapes: {ex.Message}");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Diagram contains no pages; cannot perform gluing operation.");
                }

                // ------------------------------------------------------------
                // Log total number of gluing relationships AFTER modifications
                // ------------------------------------------------------------
                int glueCountAfter = GetTotalGluingRelationships(diagram);
                Console.WriteLine($"Gluing relationships after modifications: {glueCountAfter}");

                // ------------------------------------------------------------
                // Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates the total number of gluing relationships in the diagram.
    /// TODO: Replace the placeholder implementation with actual API calls
    /// that enumerate gluing relationships (e.g., using page.GetGluedShapes
    /// or diagram.Windows[index].GlueSettings when available).
    /// </summary>
    /// <param name="diagram">The diagram instance to inspect.</param>
    /// <returns>The count of gluing relationships.</returns>
    private static int GetTotalGluingRelationships(Diagram diagram)
    {
        // Placeholder implementation – actual counting logic depends on
        // Aspose.Diagram API methods that expose gluing information.
        // For now, return 0 and add a TODO comment for future implementation.
        // TODO: Implement counting of gluing relationships using the appropriate Aspose.Diagram API.
        return 0;
    }
}