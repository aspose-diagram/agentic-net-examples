using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument should be "hide" or "show", followed by at least one diagram file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <hide|show> <diagramPath1> [<diagramPath2> ...]");
            return;
        }

        // Determine desired comment visibility based on the first argument.
        bool exportComments;
        if (args[0].Equals("hide", StringComparison.OrdinalIgnoreCase))
        {
            exportComments = false;
        }
        else if (args[0].Equals("show", StringComparison.OrdinalIgnoreCase))
        {
            exportComments = true;
        }
        else
        {
            Console.Error.WriteLine("First argument must be either 'hide' or 'show' to set comment visibility.");
            return;
        }

        // Process each diagram file supplied after the visibility flag.
        for (int i = 1; i < args.Length; i++)
        {
            string diagramPath = args[i];

            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Configure export options to apply the desired comment visibility.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.IsExportComments = exportComments;

                    // Build output file name (same folder, suffix indicating visibility).
                    string directory = Path.GetDirectoryName(diagramPath) ?? string.Empty;
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
                    string suffix = exportComments ? "_withComments" : "_noComments";
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}{suffix}.png");

                    // Save the diagram as PNG with the specified options.
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Exported diagram with comment visibility '{(exportComments ? "shown" : "hidden")}': {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current diagram.
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        }
    }
}
