using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; otherwise, nothing to process.
            if (diagram.Windows == null || diagram.Windows.Count == 0)
            {
                Console.WriteLine("The diagram contains no windows to process.");
            }
            else
            {
                // Use LINQ to find windows where ShowRulers is FALSE.
                var windowsToEnable = diagram.Windows
                    .Cast<Window>()
                    .Where(w => w.ShowRulers == BOOL.False)
                    .ToList();

                // Enable rulers for each filtered window.
                foreach (var window in windowsToEnable)
                {
                    window.ShowRulers = BOOL.True;
                }

                Console.WriteLine($"{windowsToEnable.Count} window(s) had rulers disabled and were updated.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}