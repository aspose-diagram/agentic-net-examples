using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Define output path for the saved diagram.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_out.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one window to work with.
                if (diagram.Windows.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no windows.");
                    return;
                }

                // Set ShowPageBreaks to TRUE on the first window.
                Window window = diagram.Windows[0];
                window.ShowPageBreaks = BOOL.True;

                // Save the diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify the property persisted.
            using (Diagram reloadedDiagram = new Diagram(outputPath))
            {
                if (reloadedDiagram.Windows.Count == 0)
                {
                    Console.Error.WriteLine("Error: Reloaded diagram contains no windows.");
                    return;
                }

                Window reloadedWindow = reloadedDiagram.Windows[0];

                // Verify that ShowPageBreaks remains TRUE.
                if (reloadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks property was not preserved after saving and reloading.");
                }

                // If verification passes, output success message.
                Console.WriteLine("Test passed: ShowPageBreaks remains TRUE after save and reload.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}