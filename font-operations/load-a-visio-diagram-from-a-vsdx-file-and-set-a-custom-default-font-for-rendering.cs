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
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> <defaultFontName>");
            return;
        }

        string inputPath = args[0];
        string defaultFontName = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        // Apply the custom default font before loading the diagram.
        // This ensures the font setting is considered during rendering.
        FontConfigs.DefaultFontName = defaultFontName;

        try
        {
            // Load the Visio diagram from the specified VSDX file.
            Diagram diagram = new Diagram(inputPath);

            // Optional: output basic information to confirm successful load.
            Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");

            // The diagram is now ready for rendering operations (e.g., image or PDF export)
            // and will use the specified default font when required.
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}