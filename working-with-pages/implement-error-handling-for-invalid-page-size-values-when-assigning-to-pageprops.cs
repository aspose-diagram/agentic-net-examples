using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure required arguments are provided: input file, width, height, output file.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <pageWidthInInches> <pageHeightInInches> <outputVisioPath>");
            return;
        }

        // Assign and validate the input diagram path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign and validate the output diagram path (directory must exist).
        string outputPath = args[3];
        string outputDir = Path.GetDirectoryName(outputPath);
        if (string.IsNullOrEmpty(outputDir) || !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Parse and validate page width.
        if (!double.TryParse(args[1], out double pageWidth) || pageWidth <= 0)
        {
            Console.Error.WriteLine($"Invalid page width: '{args[1]}'. Width must be a positive number.");
            return;
        }

        // Parse and validate page height.
        if (!double.TryParse(args[2], out double pageHeight) || pageHeight <= 0)
        {
            Console.Error.WriteLine($"Invalid page height: '{args[2]}'. Height must be a positive number.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            using Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) to modify its size.
            Page page = diagram.Pages[0];

            // Assign validated width and height to the page properties (values are in inches).
            page.PageSheet.PageProps.PageWidth.Value = pageWidth;   // Set page width.
            page.PageSheet.PageProps.PageHeight.Value = pageHeight; // Set page height.

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Page size updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}