using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file path, output file path, optional theme variant index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath> [variantIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int variantIndex = 1; // Default to Variant1

        if (args.Length >= 3 && !int.TryParse(args[2], out variantIndex))
        {
            Console.Error.WriteLine("Invalid variant index. It must be an integer.");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from a byte array
            byte[] inputBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (Diagram diagram = new Diagram(inputStream))
            {
                // Apply a preset theme and variant to the first page, if any pages exist
                if (diagram.Pages.Count > 0)
                {
                    // Set a known preset theme (Bubble) to ensure the variant takes effect
                    diagram.Pages[0].PresetTheme = PresetThemeValue.Bubble;

                    // Map the integer index to a supported PresetThemeVariantValue (only Variant1‑Variant4 exist)
                    PresetThemeVariantValue variantValue = PresetThemeVariantValue.Variant1;
                    switch (variantIndex)
                    {
                        case 1: variantValue = PresetThemeVariantValue.Variant1; break;
                        case 2: variantValue = PresetThemeVariantValue.Variant2; break;
                        case 3: variantValue = PresetThemeVariantValue.Variant3; break;
                        case 4: variantValue = PresetThemeVariantValue.Variant4; break;
                        default:
                            Console.Error.WriteLine($"Unsupported variant index {variantIndex}. Using Variant1.");
                            break;
                    }

                    diagram.Pages[0].PresetThemeVariant = variantValue;
                }
                else
                {
                    Console.Error.WriteLine("Diagram contains no pages. No theme applied.");
                }

                // Save the modified diagram to a memory stream
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    byte[] outputBytes = outputStream.ToArray();

                    // Write the modified byte array to the output file
                    File.WriteAllBytes(outputPath, outputBytes);

                    // Output the size of the resulting byte array
                    Console.WriteLine($"Modified diagram saved. Byte array length: {outputBytes.Length}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}