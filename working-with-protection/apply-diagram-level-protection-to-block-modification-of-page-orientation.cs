using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_protected.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // TODO: Apply diagram‑level protection to block page orientation changes.
                // The Aspose.Diagram API does not expose a direct property for locking page orientation.
                // When such a property becomes available (e.g., a LockPageSetup cell on the page sheet),
                // set its value to BOOL.True here.

                // Example placeholder (uncomment and adjust when the correct API is known):
                // foreach (Page page in diagram.Pages)
                // {
                //     page.PageSheet.LockPageSetup.Value = BOOL.True;
                // }

                // Save the diagram with (potential) protection applied
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}