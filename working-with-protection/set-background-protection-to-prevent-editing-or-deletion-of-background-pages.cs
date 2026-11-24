using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Set global document protection to lock background pages.
                // This prevents users from editing or deleting any background page.
                // ------------------------------------------------------------
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;

                // ------------------------------------------------------------
                // (Optional) Ensure each page that is a background page is marked as such.
                // This does not affect protection directly but keeps the page state clear.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // If the page is already a background page, keep the flag; otherwise skip.
                    // The Background property expects a BOOL value.
                    if (page.Background == BOOL.True)
                    {
                        // No additional action required; the global protection already applies.
                    }
                }

                // Save the protected diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with background protection to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}