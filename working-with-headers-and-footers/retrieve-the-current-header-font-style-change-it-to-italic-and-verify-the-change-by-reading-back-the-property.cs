using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Define input diagram path
        string inputPath = "sample.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the global header/footer font settings
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Retrieve and display the current Italic setting (BOOL type)
            BOOL originalItalic = headerFont.Italic;
            Console.WriteLine($"Original Header Font Italic: {originalItalic == BOOL.True}");

            // Change the header font style to italic (set BOOL.True)
            headerFont.Italic = BOOL.True;

            // Verify that the change was applied
            BOOL updatedItalic = diagram.HeaderFooter.HeaderFooterFont.Italic;
            if (updatedItalic != BOOL.True)
            {
                throw new Exception("Failed to set the header font to italic.");
            }
            Console.WriteLine("Header font italic property successfully set to true.");

            // Save the diagram to persist the change (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}