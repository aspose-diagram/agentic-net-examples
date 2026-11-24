using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate the diagram structure before any conversion.
                if (!ValidateDiagram(diagram))
                {
                    Console.Error.WriteLine("Diagram validation failed. Conversion aborted.");
                    return;
                }

                // Define the output PDF file path (same name with .pdf extension).
                string outputPath = Path.ChangeExtension(inputPath, ".pdf");

                // Save the diagram as PDF using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Pdf);
                Console.WriteLine($"Diagram successfully converted and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Performs basic structural validation on the loaded diagram.
    /// Checks for presence of pages, shapes, and ensures shapes are not marked as deleted.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <returns>True if validation passes; otherwise false.</returns>
    private static bool ValidateDiagram(Diagram diagram)
    {
        // Ensure the diagram has at least one page.
        if (diagram.Pages == null || diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("Validation error: Diagram contains no pages.");
            return false;
        }

        // Iterate through each page explicitly typing the loop variable.
        foreach (Page page in diagram.Pages)
        {
            // Ensure the page has at least one shape.
            if (page.Shapes == null || page.Shapes.Count == 0)
            {
                Console.Error.WriteLine($"Validation warning: Page '{page.NameU}' contains no shapes.");
                // Not a fatal error; continue checking other pages.
                continue;
            }

            // Iterate through each shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Guard against null shape reference (should not happen).
                if (shape == null)
                {
                    continue;
                }

                // Check if the shape is marked as deleted (Del cell equals BOOL.True).
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Validation error: Shape ID {shape.ID} on page '{page.NameU}' is marked as deleted.");
                    return false;
                }

                // Verify that the shape has an associated master (for non-group shapes).
                if (shape.Master == null && shape.Type != TypeValue.Group)
                {
                    Console.Error.WriteLine($"Validation warning: Shape ID {shape.ID} on page '{page.NameU}' has no master.");
                }
            }
        }

        // If all checks passed, return true.
        return true;
    }
}