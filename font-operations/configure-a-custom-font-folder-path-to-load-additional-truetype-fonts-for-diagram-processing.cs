using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, custom font folder path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <customFontFolderPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fontFolderPath = args[2];

        // Guard: ensure the input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the custom font folder exists
        if (!Directory.Exists(fontFolderPath))
        {
            Console.Error.WriteLine($"Font folder not found: {fontFolderPath}");
            return;
        }

        try
        {
            // Configure Aspose.Diagram to search the custom font folder (recursive)
            // The second parameter expects a plain bool, not Aspose BOOL
            FontConfigs.SetFontFolder(fontFolderPath, true);

            // Optional: set a fallback font name
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // List fonts referenced in the diagram
            Console.WriteLine("Fonts referenced in the diagram:");
            foreach (Font font in diagram.Fonts)
            {
                Console.WriteLine($"- {font.Name}");
            }

            // Save the diagram to the specified output path (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}