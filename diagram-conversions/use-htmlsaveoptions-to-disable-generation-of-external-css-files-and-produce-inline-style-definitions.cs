using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output HTML file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Disable exporting hidden pages.
                htmlOptions.ExportHiddenPage = false;

                // Note: HTMLSaveOptions does not provide a property to disable external CSS generation.
                // Inline style definitions are used by default when external CSS is not generated.

                // Save the diagram as HTML with the configured options.
                diagram.Save(outputPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
