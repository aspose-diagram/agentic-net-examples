using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input file path and output file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Read the input Visio file into a byte array
            byte[] inputBytes = File.ReadAllBytes(inputPath);

            // Process the diagram: load, apply theme variant, and get modified bytes
            byte[] outputBytes = ApplyThemeVariant(inputBytes);

            // Write the modified diagram to the output file
            File.WriteAllBytes(outputPath, outputBytes);
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a Visio diagram from a byte array, applies a preset theme variant to the first page,
    /// and returns the modified diagram as a byte array.
    /// </summary>
    /// <param name="diagramBytes">The original diagram bytes.</param>
    /// <returns>Modified diagram bytes.</returns>
    private static byte[] ApplyThemeVariant(byte[] diagramBytes)
    {
        // Validate input
        if (diagramBytes == null || diagramBytes.Length == 0)
        {
            Console.Error.WriteLine("Input byte array is null or empty.");
            return Array.Empty<byte>();
        }

        try
        {
            // Load the diagram from the provided byte array using a memory stream
            using (MemoryStream inputStream = new MemoryStream(diagramBytes))
            using (Diagram diagram = new Diagram(inputStream))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram contains no pages.");
                    return Array.Empty<byte>();
                }

                // Apply a preset theme variant to the first page
                // Choose Variant1 as an example; other variants are available in PresetThemeVariantValue enum
                diagram.Pages[0].PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to a memory stream in VSDX format
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    // Return the resulting byte array
                    return outputStream.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Failed to apply theme variant: {ex.Message}");
            return Array.Empty<byte>();
        }
    }
}