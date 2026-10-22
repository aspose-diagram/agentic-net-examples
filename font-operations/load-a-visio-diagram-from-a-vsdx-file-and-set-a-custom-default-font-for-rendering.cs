using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input VSDX file path (first command‑line argument)
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Error.WriteLine("Error: No input file path provided. Pass the VSDX file path as the first argument.");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Set a custom default font for rendering before loading the diagram.
        // This font will be used whenever a required font is missing in the diagram.
        FontConfigs.DefaultFontName = "Calibri";

        // Optional: add an additional font folder if your custom fonts are stored elsewhere.
        // FontConfigs.SetFontFolder(@"C:\MyCustomFonts", true);

        try
        {
            // Load the Visio diagram from the specified VSDX file
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram to a new file to confirm that the custom font setting is applied
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_with_custom_font.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}