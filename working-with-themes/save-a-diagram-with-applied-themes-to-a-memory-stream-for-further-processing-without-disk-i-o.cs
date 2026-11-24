using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply a preset theme to every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Example theme – adjust as needed.
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Save the diagram with the applied theme to a memory stream.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save directly to the stream in VSDX format (no disk I/O).
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                    // Reset stream position for any subsequent processing.
                    memoryStream.Position = 0;

                    // Example: output the size of the generated stream.
                    Console.WriteLine($"Diagram saved to memory stream. Size: {memoryStream.Length} bytes.");

                    // Further processing of the memory stream can be performed here.
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, theming, or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}