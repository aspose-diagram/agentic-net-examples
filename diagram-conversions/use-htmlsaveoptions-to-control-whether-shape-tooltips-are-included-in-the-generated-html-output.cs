using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath;
        string outputPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputPath = args[1];
        }
        else
        {
            // Default paths (adjust as needed).
            inputPath = "sample.vsdx";
            outputPath = "output.html";
        }

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // NOTE: Aspose.Diagram does not provide a direct option to include or exclude
            // shape tooltips in the generated HTML. As a closest alternative, we control
            // whether hidden pages are exported.
            htmlOptions.ExportHiddenPage = false; // Use native bool, not BOOL enum.

            // Save the diagram as HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
