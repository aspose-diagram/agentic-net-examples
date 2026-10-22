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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputXpsFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure XPS save options to exclude hidden pages.
                XPSSaveOptions options = new XPSSaveOptions();
                options.ExportHiddenPage = false; // Do not export hidden pages.

                // Save the diagram as XPS using the configured options.
                diagram.Save(outputPath, options);
            }

            Console.WriteLine($"XPS file successfully saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
