using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all windows and toggle the ShowGrid property
            foreach (Window window in diagram.Windows)
            {
                // Toggle: if currently True set to False, otherwise set to True
                window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
            }

            // Determine output path (same directory, fixed name)
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"ShowGrid toggled for all windows. Saved modified diagram to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}