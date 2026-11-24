using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the Visio file to process.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API guidelines.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Rename each page to a deterministic unique name (e.g., "Page_1", "Page_2", ...).
                int pageIndex = 1;
                foreach (Page page in diagram.Pages)
                {
                    // Assign a new name based on the current index.
                    page.Name = $"Page_{pageIndex}";
                    pageIndex++;
                }

                // Validate that all page names are unique after renaming.
                HashSet<string> nameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram.Pages)
                {
                    if (!nameSet.Add(page.Name))
                    {
                        // Duplicate detected – throw an exception to indicate validation failure.
                        throw new Exception($"Duplicate page name detected: \"{page.Name}\"");
                    }
                }

                // If we reach this point, all page names are unique.
                Console.WriteLine("Validation succeeded: All page names are unique.");

                // Optionally, save the modified diagram to a new file to persist the changes.
                // The output path is derived from the input path with a suffix.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_Renamed.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}