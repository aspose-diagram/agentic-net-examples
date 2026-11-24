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

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options to preserve hidden pages.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    ExportHiddenPage = true // Preserve hidden pages in the HTML output.
                };

                // Save the diagram to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Diagram saved to HTML successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}