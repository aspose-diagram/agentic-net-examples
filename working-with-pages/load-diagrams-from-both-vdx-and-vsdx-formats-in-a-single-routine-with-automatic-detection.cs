using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file (VDX or VSDX).");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Attempt to load the diagram with automatic format detection.
        Diagram diagram = null;
        try
        {
            // Detect format based on file extension (case‑insensitive).
            string ext = Path.GetExtension(inputPath).ToLowerInvariant();
            LoadFileFormat format;

            if (ext == ".vdx")
                format = LoadFileFormat.Vdx;
            else if (ext == ".vsdx")
                format = LoadFileFormat.Vsdx;
            else
            {
                Console.Error.WriteLine("Unsupported file extension. Only .vdx and .vsdx are supported.");
                return;
            }

            // Load the diagram using the detected format.
            diagram = new Diagram(inputPath, format);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Use a using block to ensure proper disposal of the Diagram object.
        using (diagram)
        {
            // Simple verification output.
            Console.WriteLine($"Diagram loaded successfully from '{inputPath}'.");
            Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
        }
    }
}