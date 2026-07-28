using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Loads a Visio diagram, automatically detecting VDX or VSDX format.
    static Diagram LoadDiagram(string filePath)
    {
        // Determine file extension (case‑insensitive).
        string ext = Path.GetExtension(filePath).ToLowerInvariant();

        // Choose the appropriate LoadFileFormat enum value.
        LoadFileFormat format = ext switch
        {
            ".vdx"  => LoadFileFormat.Vdx,
            ".vsdx" => LoadFileFormat.Vsdx,
            _ => throw new Exception($"Unsupported file extension '{ext}'. Only .vdx and .vsdx are supported.")
        };

        // Use the constructor that accepts a format for explicit loading.
        return new Diagram(filePath, format);
    }

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide one or more Visio file paths as arguments.");
            return;
        }

        foreach (string filePath in args)
        {
            try
            {
                // Load the diagram inside a using block to ensure proper disposal.
                using Diagram diagram = LoadDiagram(filePath);
                Console.WriteLine($"Successfully loaded '{filePath}'. Pages count: {diagram.Pages.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load '{filePath}': {ex.Message}");
            }
        }
    }
}
