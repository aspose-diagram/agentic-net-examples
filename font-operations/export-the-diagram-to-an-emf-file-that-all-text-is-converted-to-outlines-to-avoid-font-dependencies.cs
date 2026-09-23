using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Configure a fallback font to avoid missing font issues during rendering
        FontConfigs.DefaultFontName = "Arial";

        // Path to the source Visio diagram
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the exported EMF file
        string outputPath = "output.emf";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Set up EMF export options using PrintSaveOptions
            PrintSaveOptions emfOptions = new PrintSaveOptions
            {
                SaveFormat = SaveFileFormat.Emf // Export as EMF
                // Note: ExportHiddenPage is not supported by PrintSaveOptions; hidden pages are excluded by default
            };

            // Save the diagram as an EMF file with the configured options
            diagram.Save(outputPath, emfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}