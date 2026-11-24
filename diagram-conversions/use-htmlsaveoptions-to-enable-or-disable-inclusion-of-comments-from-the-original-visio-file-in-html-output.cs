using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath> <includeComments:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string includeCommentsArg = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the include‑comments flag.
        bool includeComments;
        if (!bool.TryParse(includeCommentsArg, out includeComments))
        {
            Console.Error.WriteLine("Error: The includeComments argument must be 'true' or 'false'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Control whether comments (annotations) are exported.
                // HTMLSaveOptions.IsExportComments is a native bool, so assign the parsed value directly.
                htmlOptions.IsExportComments = includeComments;

                // Save the diagram to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine($"HTML export completed successfully. Output: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}