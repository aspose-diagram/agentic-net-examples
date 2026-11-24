using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input diagram path and output diagram path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Guard: verify output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (string.IsNullOrEmpty(outputDir) || !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Instantiate Diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Add a new blank page to the diagram.
                diagram.Pages.Add(new Page());

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully added a new page and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}