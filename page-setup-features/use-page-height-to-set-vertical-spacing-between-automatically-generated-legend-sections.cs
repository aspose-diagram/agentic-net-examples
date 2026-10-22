using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional argument or default).
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram.
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Create legend sections as simple rectangle shapes with text.
            // -----------------------------------------------------------------
            int legendCount = 3;               // Number of legend sections to create.
            double startX = 1.0;                // Starting X position (in inches).
            double startY = 1.0;                // Starting Y position (in inches).
            double rectWidth = 2.0;             // Width of each legend rectangle (in inches).
            double rectHeight = 0.5;            // Height of each legend rectangle (in inches).
            double interLegendGap = 0.2;        // Additional gap between legends (in inches).

            for (int i = 0; i < legendCount; i++)
            {
                // Draw a rectangle shape on the page.
                long shapeId = page.DrawRectangle(
                    startX,
                    startY + i * (rectHeight + interLegendGap),
                    rectWidth,
                    rectHeight);

                // Retrieve the shape object using the returned ID.
                Shape legendShape = page.Shapes.GetShape((int)shapeId);

                // Clear any existing text and add new legend text.
                legendShape.Text.Value.Clear();
                legendShape.Text.Value.Add(new Txt($"Legend {i + 1}"));
            }

            // -----------------------------------------------------------------
            // Calculate vertical spacing based on the page height.
            // -----------------------------------------------------------------
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value; // Height in inches.
            // Example: set vertical spacing to 5% of the page height.
            double verticalSpacing = pageHeight * 0.05;

            // Configure AutoSpaceOptions with the calculated vertical distance.
            AutoSpaceOptions spaceOptions = new AutoSpaceOptions();
            spaceOptions.DistanceInHorizontal = 0.0;          // No horizontal spacing needed.
            spaceOptions.DistanceInVertical = verticalSpacing; // Use page‑height‑based spacing.

            // Apply auto‑spacing to all shapes on the page (including the legends).
            page.AutoSpaceShapes(page.Shapes, spaceOptions);

            // -----------------------------------------------------------------
            // Save the modified diagram to the output file.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}