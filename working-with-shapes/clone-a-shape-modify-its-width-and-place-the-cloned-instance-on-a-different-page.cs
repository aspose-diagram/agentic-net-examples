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
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Source page (first page)
                Page sourcePage = diagram.Pages[0];

                // Ensure the source page has at least one shape
                if (sourcePage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The source page contains no shapes to clone.");
                    return;
                }

                // Retrieve the first shape on the source page
                Shape sourceShape = sourcePage.Shapes[0];

                // Create a new shape instance and copy the source shape's data
                Shape clonedShape = new Shape();
                clonedShape.Copy(sourceShape);

                // Modify the cloned shape's width (example: set to 2.0 inches)
                clonedShape.XForm.Width.Value = 2.0;

                // Optionally adjust the cloned shape's position to avoid overlap
                clonedShape.XForm.PinX.Value = sourceShape.XForm.PinX.Value + 2.0;
                clonedShape.XForm.PinY.Value = sourceShape.XForm.PinY.Value;

                // Determine target page (different from source page)
                Page targetPage;
                if (diagram.Pages.Count > 1)
                {
                    // Use the second page if it exists
                    targetPage = diagram.Pages[1];
                }
                else
                {
                    // Create a new page and add it to the diagram
                    targetPage = new Page();
                    diagram.Pages.Add(targetPage);
                }

                // Add the cloned shape to the target page
                targetPage.Shapes.Add(clonedShape);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}