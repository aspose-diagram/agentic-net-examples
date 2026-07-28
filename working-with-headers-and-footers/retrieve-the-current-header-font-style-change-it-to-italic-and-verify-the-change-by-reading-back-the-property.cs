using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (use first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the global header/footer font settings
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Display the current Italic setting (BOOL enum)
            Console.WriteLine($"Current Italic setting: {headerFont.Italic}");

            // Change the font style to Italic using the BOOL enum
            headerFont.Italic = BOOL.True;

            // Verify that the change was applied
            if (headerFont.Italic != BOOL.True)
            {
                throw new Exception("Failed to set the header font to Italic.");
            }
            else
            {
                Console.WriteLine("Header font successfully set to Italic.");
            }

            // Save the diagram to persist the change
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}