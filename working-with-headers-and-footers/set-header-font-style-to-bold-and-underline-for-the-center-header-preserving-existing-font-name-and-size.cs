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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Modified_" + Path.GetFileName(inputPath));

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the global header/footer font settings
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Preserve existing font name and size
            string existingFaceName = headerFont.FaceName;
            int existingHeight = headerFont.Height;

            // Apply bold style using Weight (700 = bold, 400 = regular)
            headerFont.Weight = 700;

            // Apply underline style using BOOL enum
            headerFont.Underline = BOOL.True;

            // Re-assign preserved values (optional, they are unchanged)
            headerFont.FaceName = existingFaceName;
            headerFont.Height = existingHeight;

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}