using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input file path and page count threshold
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <pageCountThreshold>");
            return;
        }

        string inputPath = args[0];
        string thresholdArg = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse threshold
        if (!int.TryParse(thresholdArg, out int pageThreshold) || pageThreshold < 0)
        {
            Console.Error.WriteLine($"Invalid page count threshold: {thresholdArg}");
            return;
        }

        // Determine output path (append "_modified" before extension)
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_modified.vsdx");

        try
        {
            // Load diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get total page count
                int pageCount = diagram.Pages.Count;

                // If page count exceeds the threshold, set ShowGuides to false for all windows
                if (pageCount > pageThreshold)
                {
                    // Iterate over all windows in the diagram
                    for (int i = 0; i < diagram.Windows.Count; i++)
                    {
                        Window window = diagram.Windows[i];
                        window.ShowGuides = BOOL.False;
                    }

                    Console.WriteLine($"Page count ({pageCount}) exceeds threshold ({pageThreshold}). ShowGuides set to false.");
                }
                else
                {
                    Console.WriteLine($"Page count ({pageCount}) does not exceed threshold ({pageThreshold}). No changes made.");
                }

                // Save the (potentially modified) diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}