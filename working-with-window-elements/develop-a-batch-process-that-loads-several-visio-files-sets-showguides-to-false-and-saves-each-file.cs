using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the list of Visio files to process.
        string[] filesToProcess;
        if (args.Length > 0)
        {
            // Use file paths supplied via command‑line arguments.
            filesToProcess = args;
        }
        else
        {
            // No arguments – process all .vsdx files in the current directory.
            filesToProcess = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.vsdx");
        }

        // Iterate over each file path.
        foreach (string filePath in filesToProcess)
        {
            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block (required pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all windows in the diagram and disable guides.
                    foreach (Window window in diagram.Windows)
                    {
                        window.ShowGuides = BOOL.False;
                    }

                    // Save the modified diagram back to the original file (overwrite).
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed successfully: {filePath}");
            }
            catch (Exception ex)
            {
                // Write any errors that occur during loading, modification, or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}