using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory disposal pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Batch add a rectangle shape to every page.
                foreach (Page page in diagram.Pages)
                {
                    // Add a rectangle at position (1,1). The fourth argument isCalculate = false.
                    long shapeId = page.AddShape(1.0, 1.0, "Rectangle", false);

                    // Retrieve the newly added shape.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Replace any existing text with new text.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Added Shape"));
                }

                // Validate that each page now contains at least one shape.
                bool allPagesValid = true;
                foreach (Page page in diagram.Pages)
                {
                    if (page.Shapes.Count == 0)
                    {
                        Console.Error.WriteLine($"Validation failed: Page \"{page.Name}\" contains no shapes.");
                        allPagesValid = false;
                    }
                }

                if (!allPagesValid)
                {
                    Console.Error.WriteLine("One or more pages are empty after adding shapes. Aborting save.");
                    return;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}