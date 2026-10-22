using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the global header font object.
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Output the current Italic flag (BOOL).
            Console.WriteLine($"Current Header Font Italic: {headerFont.Italic}");

            // Change the header font style to italic.
            headerFont.Italic = BOOL.True;

            // Verify that the change was applied.
            if (headerFont.Italic == BOOL.True)
            {
                Console.WriteLine("Header font italic flag set successfully.");
            }
            else
            {
                throw new Exception("Failed to set header font to italic.");
            }

            // Optional: save the modified diagram to a new file.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}