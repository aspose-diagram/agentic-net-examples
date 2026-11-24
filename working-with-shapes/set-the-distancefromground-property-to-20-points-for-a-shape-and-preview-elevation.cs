using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page firstPage = diagram.Pages[0];
                Shape targetShape = null;

                // Find the first non‑deleted shape on the page.
                foreach (Shape shape in firstPage.Shapes)
                {
                    if (shape.Del != BOOL.True)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Error: No visible shape found on the first page.");
                    return;
                }

                // Set the 3‑D distance from ground to 20 points.
                // DistanceFromGround is a double value representing points.
                targetShape.ThreeDFormat.DistanceFromGround.Value = 20.0;

                // NOTE: The Aspose.Diagram API does not expose a PreviewElevation property
                // on ThreeDFormat. Therefore it cannot be set. This comment documents the
                // limitation and the intended behavior.

                // Save the modified diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}