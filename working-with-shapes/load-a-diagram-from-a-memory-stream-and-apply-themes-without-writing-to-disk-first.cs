using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Read the entire file into a memory buffer
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Load diagram directly from the memory stream
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // Apply a preset theme to each page in the diagram
                    // Here we use the "Bubble" theme as an example
                    foreach (Page page in diagram.Pages)
                    {
                        page.PresetTheme = PresetThemeValue.Bubble;
                        // Optionally set a variant; using the first variant as an example
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Save the modified diagram to the specified output path
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}