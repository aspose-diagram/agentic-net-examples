using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path-to-diagram>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine file extension (lowercase) to apply conditional logic.
        string extension = Path.GetExtension(inputPath).ToLowerInvariant();

        // Prepare output path by appending "_modified" before the extension.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified" + extension);

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the HeaderFooter object.
            HeaderFooter headerFooter = diagram.HeaderFooter;

            // Configure footer font based on file extension.
            if (extension == ".vsdx")
            {
                // For VSDX files: use Arial, size 12, bold.
                headerFooter.HeaderFooterFont.FaceName = "Arial";
                headerFooter.HeaderFooterFont.Height = 12;          // Font size in points.
                headerFooter.HeaderFooterFont.Weight = 700;        // 700 = bold.
                headerFooter.HeaderFooterFont.Underline = BOOL.False;
            }
            else if (extension == ".vdx")
            {
                // For VDX files: use Times New Roman, size 10, regular.
                headerFooter.HeaderFooterFont.FaceName = "Times New Roman";
                headerFooter.HeaderFooterFont.Height = 10;
                headerFooter.HeaderFooterFont.Weight = 400;        // 400 = regular.
                headerFooter.HeaderFooterFont.Underline = BOOL.False;
            }
            else
            {
                Console.Error.WriteLine($"Error: Unsupported file extension '{extension}'. Only .vsdx and .vdx are supported.");
                return;
            }

            // Save the modified diagram using the appropriate format.
            if (extension == ".vsdx")
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            else // .vdx
            {
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}