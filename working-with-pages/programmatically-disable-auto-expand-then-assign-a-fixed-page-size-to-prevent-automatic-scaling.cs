using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define the fixed page dimensions (in inches).
            double fixedWidth = 8.5;   // Example: Letter width
            double fixedHeight = 11.0; // Example: Letter height

            // Iterate through all pages and apply settings.
            foreach (Page page in diagram.Pages)
            {
                // Disable automatic page expansion.
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                // Assign a fixed page size.
                page.PageSheet.PageProps.PageWidth.Value = fixedWidth;
                page.PageSheet.PageProps.PageHeight.Value = fixedHeight;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}