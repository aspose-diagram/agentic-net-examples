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
        string outputPath = "output_modified.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at path '{inputPath}'.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's width or height is locked
                        bool widthLocked = shape.Protection?.LockWidth?.Value == BOOL.True;
                        bool heightLocked = shape.Protection?.LockHeight?.Value == BOOL.True;

                        if (widthLocked || heightLocked)
                        {
                            // Log attempt to modify a locked shape
                            Console.Error.WriteLine($"Attempt to modify locked shape (ID: {shape.ID}) on page '{page.Name}'. WidthLocked={widthLocked}, HeightLocked={heightLocked}");
                            // Skip modification for this shape
                            continue;
                        }

                        try
                        {
                            // Example modification: set new width and height
                            shape.XForm.Width.Value = 2.0;
                            shape.XForm.Height.Value = 1.0;
                        }
                        catch (Exception ex)
                        {
                            // Log any errors that occur during modification
                            Console.Error.WriteLine($"Error modifying shape (ID: {shape.ID}) on page '{page.Name}': {ex.Message}");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur while loading or saving the diagram
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}