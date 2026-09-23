using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the Visio file.");
            return;
        }

        string inputPath = args[0];
        string outputPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(inputPath) ?? string.Empty,
            System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_reset.vsdx");

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and reset orientation and ScaleX.
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Portrait.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                // Set horizontal scaling factor to 1.0 (default).
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with reset pages to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
