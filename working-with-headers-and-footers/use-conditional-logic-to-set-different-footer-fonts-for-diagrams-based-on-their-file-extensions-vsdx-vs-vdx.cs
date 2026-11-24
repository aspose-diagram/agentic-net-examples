using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-diagram-path]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Insert "_modified" before the extension
            string dir = Path.GetDirectoryName(inputPath);
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string ext = Path.GetExtension(inputPath);
            outputPath = Path.Combine(dir, $"{name}_modified{ext}");
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine file extension (case-insensitive)
            string extension = Path.GetExtension(inputPath).ToLowerInvariant();

            // Access the HeaderFooterFont object
            HeaderFooterFont footerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Apply different font settings based on extension
            if (extension == ".vsdx")
            {
                // Settings for VSDX files
                footerFont.FaceName = "Arial";
                footerFont.Height = 12;          // Font size in points
                footerFont.Weight = 700;         // Bold weight
                footerFont.Underline = BOOL.True;
            }
            else if (extension == ".vdx")
            {
                // Settings for VDX files
                footerFont.FaceName = "Times New Roman";
                footerFont.Height = 10;          // Font size in points
                footerFont.Weight = 400;         // Regular weight
                footerFont.Underline = BOOL.False;
            }
            else
            {
                Console.Error.WriteLine($"Warning: Unhandled file extension '{extension}'. No footer font changes applied.");
            }

            // Save the diagram using the same format as the input
            if (extension == ".vsdx")
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            else if (extension == ".vdx")
            {
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }
            else
            {
                // Default to Vsdx if unknown extension
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}