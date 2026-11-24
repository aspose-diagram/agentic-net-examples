using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (append "_bg" before extension)
        string dir = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(dir, $"{fileNameWithoutExt}_bg.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Create a new stylesheet for background fill
            StyleSheet bgStyle = new StyleSheet();

            // Assign a unique ID (next available ID)
            bgStyle.ID = diagram.StyleSheets.Count + 1;

            // Set fill pattern to solid (1) and specify the fill color (e.g., light blue)
            bgStyle.Fill.FillPattern.Value = 1;               // Solid fill
            bgStyle.Fill.FillForegnd.Value = "#ADD8E6";       // Hex color for light blue

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(bgStyle);

            // Apply the stylesheet to the page (line, fill, and character styles use the same ID)
            page.ApplyStyle(bgStyle.ID, bgStyle.ID, bgStyle.ID);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Background fill applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}