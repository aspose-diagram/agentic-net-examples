using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the source diagram has at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page (source page) and its first shape as the shape to copy
            Page sourcePage = diagram.Pages[0];
            if (sourcePage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The source page contains no shapes to copy.");
                return;
            }

            // Retrieve the shape to copy using GetShape (shape IDs are long)
            long sourceShapeId = sourcePage.Shapes[0].ID;
            Shape sourceShape = sourcePage.Shapes.GetShape((int)sourceShapeId);

            // ------------------------------------------------------------
            // Create or retrieve the target page named "Page-3"
            // ------------------------------------------------------------
            Page targetPage = diagram.Pages.GetPage("Page-3");
            if (targetPage == null)
            {
                // Page does not exist; create a new one
                targetPage = new Page();
                targetPage.Name = "Page-3";
                diagram.Pages.Add(targetPage);
            }

            // ------------------------------------------------------------
            // Copy the shape together with its paragraphs and formatting
            // ------------------------------------------------------------
            // Create a new empty shape instance
            Shape copiedShape = new Shape();

            // Copy all properties, including paragraphs, from the source shape
            copiedShape.Copy(sourceShape);

            // Optionally, adjust the position of the copied shape on the target page
            // Here we place it at the same coordinates as the source shape
            copiedShape.XForm.PinX.Value = sourceShape.XForm.PinX.Value;
            copiedShape.XForm.PinY.Value = sourceShape.XForm.PinY.Value;

            // Add the copied shape to the target page's shape collection
            targetPage.Shapes.Add(copiedShape);

            // ------------------------------------------------------------
            // Save the modified diagram to the specified output path
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape copied to page \"Page-3\" and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}