using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path specified.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at path '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the footer margin value (in inches) from the diagram's HeaderFooter.
                double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                // Log the retrieved margin to the console for debugging.
                Console.WriteLine($"Footer margin: {footerMargin}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}