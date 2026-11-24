using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least the input file path is provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsd> [output.html]");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output HTML file path
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Replace the input file's extension with .html
            outputPath = Path.ChangeExtension(inputPath, ".html");
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the entire diagram to HTML format
                diagram.Save(outputPath, SaveFileFormat.Html);
            }

            Console.WriteLine($"Diagram successfully converted to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}