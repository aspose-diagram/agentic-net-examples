using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create a new file name with suffix "_modified".
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages and enforce a minimum height of 5 inches.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the current page height (in inches).
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                // If the height is less than 5 inches, set it to 5.
                if (currentHeight < 5.0)
                {
                    page.PageSheet.PageProps.PageHeight.Value = 5.0;
                }
            }

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}