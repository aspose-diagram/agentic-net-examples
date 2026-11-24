using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by Aspose)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Retrieve the page height (in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute vertical spacing based on page height (example: 5% of page height)
                double verticalSpacing = pageHeight * 0.05;

                // -----------------------------------------------------------------
                // Create sample legend sections (text shapes) on the page
                // -----------------------------------------------------------------
                double startX = 1.0; // inches from left
                double startY = 1.0; // inches from top
                int legendCount = 3;

                // Store created legend shapes for possible further processing
                Shape[] legendShapes = new Shape[legendCount];

                for (int i = 0; i < legendCount; i++)
                {
                    // Add a text shape for each legend entry
                    // Parameters: pinX, pinY, width, height, text
                    // Width and height are arbitrary; using 2 inches width and 0.5 inches height
                    Shape legendShape = page.AddText(
                        startX,
                        startY + i * (verticalSpacing + 0.5), // position each entry with spacing
                        2.0,
                        0.5,
                        $"Legend Item {i + 1}");

                    // Store the shape reference
                    legendShapes[i] = legendShape;
                }

                // -----------------------------------------------------------------
                // Prepare AutoSpaceOptions using the calculated vertical spacing
                // -----------------------------------------------------------------
                AutoSpaceOptions spaceOptions = new AutoSpaceOptions
                {
                    // No horizontal spacing needed for vertical legend layout
                    DistanceInHorizontal = 0.0,
                    // Apply the computed vertical spacing
                    DistanceInVertical = verticalSpacing
                };

                // -----------------------------------------------------------------
                // Auto-space the shapes on the page (including the legend shapes)
                // -----------------------------------------------------------------
                // Using the page's existing ShapeCollection; this will auto-space all shapes.
                page.AutoSpaceShapes(page.Shapes, spaceOptions);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}