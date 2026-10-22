using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Attempt to assign an undefined orientation value.
            try
            {
                // Cast an invalid integer to the enum to provoke ArgumentException.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
                Console.WriteLine("Undefined orientation assigned successfully (unexpected).");
            }
            catch (ArgumentException ex)
            {
                // Log the caught exception as required.
                Console.Error.WriteLine($"Caught ArgumentException while setting PrintPageOrientation: {ex.Message}");
            }

            // Save the modified diagram to the specified output path.
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}