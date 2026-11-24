using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, page index (0‑based), scaling factor.
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <pageIndex> <scaleFactor>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }

        if (!double.TryParse(args[2], out double scaleFactor) || scaleFactor <= 0)
        {
            Console.Error.WriteLine("Invalid scaling factor. Must be a positive number.");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate requested page index.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page.
                Page page = diagram.Pages[pageIndex];

                // Iterate over all shapes on the page and apply scaling.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Scale position.
                    shape.XForm.PinX.Value *= scaleFactor;
                    shape.XForm.PinY.Value *= scaleFactor;

                    // Scale size.
                    shape.XForm.Width.Value *= scaleFactor;
                    shape.XForm.Height.Value *= scaleFactor;
                }

                // Prepare output path (original name with "_scaled" suffix).
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_scaled.vsdx");

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with scaled shapes to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}