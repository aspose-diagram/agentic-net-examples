using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the Visio diagram inside a try/catch block.
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        bool isValid = true;

        // Validate the diagram structure.
        try
        {
            // Ensure the diagram contains at least one page.
            if (diagram.Pages == null || diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Validation error: Diagram contains no pages.");
                isValid = false;
            }
            else
            {
                // Iterate over each page explicitly typed as Aspose.Diagram.Page.
                foreach (Page page in diagram.Pages)
                {
                    // Validate that the page has a Shapes collection.
                    if (page.Shapes == null || page.Shapes.Count == 0)
                    {
                        Console.Error.WriteLine($"Validation warning: Page '{page.NameU}' contains no shapes.");
                        // Not a fatal error; continue checking other pages.
                    }

                    // Iterate over each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape == null)
                            continue; // Defensive check.

                        // Check if the shape is marked as deleted (shape.Del is of type BOOL).
                        if (shape.Del == BOOL.True)
                        {
                            Console.Error.WriteLine($"Validation error: Shape ID {shape.ID} on page '{page.NameU}' is marked as deleted.");
                            isValid = false;
                            // Continue checking other shapes to report all issues.
                        }

                        // Validate that width and height are positive numbers.
                        if (shape.XForm != null && shape.XForm.Width != null && shape.XForm.Height != null)
                        {
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;
                            if (width <= 0 || height <= 0)
                            {
                                Console.Error.WriteLine($"Validation error: Shape ID {shape.ID} on page '{page.NameU}' has non‑positive dimensions (Width={width}, Height={height}).");
                                isValid = false;
                            }
                        }

                        // Validate custom properties (Props collection contains Prop objects).
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop == null)
                                    continue;

                                // Example check: property label (name) should not be empty.
                                if (string.IsNullOrWhiteSpace(prop.Label?.Value))
                                {
                                    Console.Error.WriteLine($"Validation warning: Shape ID {shape.ID} has a custom property with an empty label.");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed with exception: {ex.Message}");
            isValid = false;
        }

        if (!isValid)
        {
            Console.Error.WriteLine("Diagram validation failed. Conversion aborted.");
            return;
        }

        // Conversion: save the diagram to PDF (example format).
        try
        {
            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save using the appropriate SaveFileFormat.
            diagram.Save(outputPath, SaveFileFormat.Pdf);
            Console.WriteLine($"Diagram successfully converted and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}