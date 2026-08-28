using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: require at least the input diagram path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        // Input diagram file path.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default).
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_resized.vsdx");

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Check if the page has auto‑expand (DrawingResizeType) enabled.
                    if (page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically)
                    {
                        // Capture original dimensions (in inches).
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Log page identification and original size.
                        Console.WriteLine($"Page ID {page.ID} ('{page.Name}') auto‑expand enabled. Original size: {originalWidth}in x {originalHeight}in");

                        // Example resizing: increase both width and height by 20%.
                        page.PageSheet.PageProps.PageWidth.Value = originalWidth * 1.2;
                        page.PageSheet.PageProps.PageHeight.Value = originalHeight * 1.2;

                        // Log the new dimensions after resizing.
                        Console.WriteLine($"Resized to: {page.PageSheet.PageProps.PageWidth.Value}in x {page.PageSheet.PageProps.PageHeight.Value}in");
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}