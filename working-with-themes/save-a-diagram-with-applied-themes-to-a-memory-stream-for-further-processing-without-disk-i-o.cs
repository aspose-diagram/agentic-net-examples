using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply a preset theme to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Example theme: Bubble with Variant1
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Save the themed diagram to a memory stream (no disk I/O)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save as VSDX format directly into the stream
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                    // Reset stream position if further processing is needed
                    memoryStream.Position = 0;

                    // Example: output the size of the generated stream
                    Console.WriteLine($"Diagram saved to memory stream. Size: {memoryStream.Length} bytes.");
                    
                    // Further processing of the memoryStream can be performed here
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}