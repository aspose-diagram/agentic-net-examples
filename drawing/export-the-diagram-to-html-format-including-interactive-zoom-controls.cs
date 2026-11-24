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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
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
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Do not export hidden pages. Property expects a bool, not BOOL.
                htmlOptions.ExportHiddenPage = false; // changed from BOOL.False

                // Do not export comments. Property expects a bool, not BOOL.
                htmlOptions.IsExportComments = false; // changed from BOOL.False

                // Save the diagram as an HTML file using the configured options.
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
