using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, page index (0‑based), scaling factor (double)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <pageIndex> <scaleFactor>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string scaleFactorArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        // Parse scaling factor
        if (!double.TryParse(scaleFactorArg, out double scaleFactor) || scaleFactor <= 0)
        {
            Console.Error.WriteLine($"Invalid scaling factor: {scaleFactorArg}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate page index range
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Get the selected page
            Page page = diagram.Pages[pageIndex];

            // Iterate over all shapes on the page and apply scaling
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Scale position
                shape.XForm.PinX.Value *= scaleFactor;
                shape.XForm.PinY.Value *= scaleFactor;

                // Scale size
                shape.XForm.Width.Value *= scaleFactor;
                shape.XForm.Height.Value *= scaleFactor;

                // Optionally scale other transform properties if needed (LocPinX, LocPinY, Angle, etc.)
                // Here we keep rotation unchanged.
            }

            // Prepare output path (append "_scaled" before extension)
            string dir = Path.GetDirectoryName(inputPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string ext = Path.GetExtension(inputPath);
            string outputPath = Path.Combine(dir, $"{nameWithoutExt}_scaled{ext}");

            // Save the modified diagram in the same format as the original (using Vsdx as a safe default)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with scaled shapes to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}