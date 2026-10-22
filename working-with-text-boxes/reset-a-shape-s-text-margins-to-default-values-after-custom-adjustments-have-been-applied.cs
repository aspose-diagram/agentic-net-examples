using System;
using System.IO;
using Aspose.Diagram;

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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not provided, create one with "_reset" suffix.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_reset" + Path.GetExtension(inputPath));

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page and one shape.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            var page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: First page contains no shapes.");
                return;
            }

            // Select the first shape on the first page.
            Shape shape = page.Shapes[0];

            // Reset text block margins to default (undefined) values.
            // Using MeasureConst.Undefined signals Visio to use its built‑in defaults.
            DoubleValue defaultMargin = new DoubleValue(0, MeasureConst.Undefined);
            shape.TextBlock.LeftMargin = defaultMargin;
            shape.TextBlock.RightMargin = defaultMargin;
            shape.TextBlock.TopMargin = defaultMargin;
            shape.TextBlock.BottomMargin = defaultMargin;

            // Optionally, reset the default tab stop to its default (undefined) as well.
            shape.TextBlock.DefaultTabStop = new DoubleValue(0, MeasureConst.Undefined);

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}