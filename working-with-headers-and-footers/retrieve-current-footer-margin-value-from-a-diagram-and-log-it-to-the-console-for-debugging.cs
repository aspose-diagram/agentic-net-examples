using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the current footer margin (in inches) from the diagram's HeaderFooter
            double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

            // Log the retrieved footer margin value for debugging
            Console.WriteLine($"Footer margin: {footerMargin} inches");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}