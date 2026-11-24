using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder, suffix "_Header.vsdx").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_Header.vsdx");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the document title; fall back to a default if empty.
                string title = diagram.DocumentProps.Title;
                if (string.IsNullOrWhiteSpace(title))
                {
                    title = "Untitled Document";
                }

                // Set the header text (centered) to the document title.
                diagram.HeaderFooter.HeaderCenter = title;

                // Configure the header/footer font: Arial, 12 pt.
                // Height uses a negative scaling factor: -16 approximates 12 pt.
                HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;
                headerFont.FaceName = "Arial";
                headerFont.Height = -16;               // Approx. 12 pt.
                headerFont.Weight = 400;               // Normal weight.
                headerFont.Italic = BOOL.False;
                headerFont.Underline = BOOL.False;

                // Save the modified diagram to a new file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Header added successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}