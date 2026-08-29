using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure two arguments: input file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> <outputVisioFile>");
            return;
        }

        // Input file path variable.
        string inputPath = args[0];
        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output file path variable.
        string outputPath = args[1];
        // Guard: ensure the directory for the output exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Read the entire diagram file into a byte array.
            byte[] inputBytes = File.ReadAllBytes(inputPath);

            // Apply the theme variant and obtain the modified diagram as a byte array.
            byte[] resultBytes = ApplyThemeVariant(inputBytes, PresetThemeVariantValue.Variant1);

            // Write the modified diagram to the specified output path.
            File.WriteAllBytes(outputPath, resultBytes);
            Console.WriteLine("Diagram processed and saved successfully.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a Visio diagram from a byte array, applies a preset theme variant,
    /// and returns the modified diagram as a new byte array.
    /// </summary>
    /// <param name="diagramBytes">Original diagram bytes.</param>
    /// <param name="variant">The theme variant to apply.</param>
    /// <returns>Modified diagram bytes.</returns>
    private static byte[] ApplyThemeVariant(byte[] diagramBytes, PresetThemeVariantValue variant)
    {
        // Validate input byte array.
        if (diagramBytes == null || diagramBytes.Length == 0)
        {
            throw new ArgumentException("Input diagram byte array is null or empty.");
        }

        // Load the diagram from the provided byte array using a memory stream.
        using (MemoryStream inputStream = new MemoryStream(diagramBytes))
        {
            // Diagram constructor reads from the stream.
            Diagram diagram = new Diagram(inputStream);

            // Access the first page (or any target page) to set the theme.
            Page page = diagram.Pages[0];

            // Set a base preset theme (required before setting a variant).
            page.PresetTheme = PresetThemeValue.Bubble;

            // Apply the requested theme variant.
            page.PresetThemeVariant = variant;

            // Save the modified diagram into a new memory stream in VSDX format.
            using (MemoryStream outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                // Return the resulting byte array.
                return outputStream.ToArray();
            }
        }
    }
}