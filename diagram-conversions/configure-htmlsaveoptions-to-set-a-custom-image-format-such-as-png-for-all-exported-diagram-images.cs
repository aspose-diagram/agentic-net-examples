using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
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

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // PNG is the default image format for HTML export.
            // The API does not provide a property to change the image format,
            // so we rely on the default PNG output for all diagram images.
            // This comment documents the limitation and the chosen approach.

            // Example of setting a boolean property using plain bool
            // because ExportHiddenPage is a native bool, not Aspose BOOL.
            htmlOptions.ExportHiddenPage = false;

            // Save the diagram to HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}