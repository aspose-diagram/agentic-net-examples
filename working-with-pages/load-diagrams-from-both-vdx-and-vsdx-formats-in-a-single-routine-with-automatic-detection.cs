using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Expects a single argument: the path to a Visio file (VDX or VSDX).
    /// </summary>
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        // Attempt to load the diagram with automatic format detection.
        Diagram diagram = LoadDiagram(inputPath);
        if (diagram == null)
        {
            // LoadDiagram already reported the error.
            return;
        }

        // Example usage after loading: display basic information.
        Console.WriteLine($"Successfully loaded diagram: {Path.GetFileName(inputPath)}");
        Console.WriteLine($"Page count: {diagram.Pages.Count}");
    }

    /// <summary>
    /// Loads a Visio diagram from the specified path.
    /// Detects VDX or VSDX format based on file extension and uses the appropriate LoadFileFormat.
    /// Returns the loaded Diagram instance, or null if loading fails.
    /// </summary>
    private static Diagram LoadDiagram(string path)
    {
        try
        {
            // Determine the file extension (case‑insensitive) to select the correct LoadFileFormat.
            string extension = Path.GetExtension(path).ToLowerInvariant();

            // Default to automatic detection by using the constructor without explicit format.
            // For explicit control, map known extensions to LoadFileFormat values.
            Diagram diagram;
            if (extension == ".vdx")
            {
                // VDX (Visio 2003/2007) format.
                diagram = new Diagram(path, LoadFileFormat.Vdx);
            }
            else if (extension == ".vsdx")
            {
                // VSDX (Visio 2013+) format.
                diagram = new Diagram(path, LoadFileFormat.Vsdx);
            }
            else
            {
                // For other supported formats, rely on Aspose.Diagram's internal detection.
                diagram = new Diagram(path);
            }

            return diagram;
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading.
            Console.Error.WriteLine($"Error loading diagram '{path}': {ex.Message}");
            return null;
        }
    }
}