using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional: same folder with suffix).
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_RulersEnabled.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are windows to process.
            if (diagram.Windows == null || diagram.Windows.Count == 0)
            {
                Console.WriteLine("The diagram contains no windows to process.");
            }
            else
            {
                // Use LINQ to filter windows where ShowRulers is false.
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