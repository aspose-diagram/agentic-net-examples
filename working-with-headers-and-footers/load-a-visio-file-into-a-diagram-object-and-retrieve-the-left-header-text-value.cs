using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the left header text from the diagram's HeaderFooter property.
            string leftHeaderText = diagram.HeaderFooter.HeaderLeft;

            // Output the retrieved header text.
            Console.WriteLine($"Left Header Text: {leftHeaderText}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram related errors to the error console.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}