using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the input file.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Do not export hidden pages.
                    ExportHiddenPage = false,
                    // Do not export comments.
                    IsExportComments = false
                };

                // Create (or overwrite) the output HTML file.
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Save the diagram as HTML directly into the provided stream.
                    diagram.Save(outputStream, htmlOptions);
                }

                Console.WriteLine($"Diagram successfully saved as HTML to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}