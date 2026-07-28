using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and apply the required print settings.
            foreach (Page page in diagram.Pages)
            {
                // Set page orientation to Portrait.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                // Set horizontal scaling factor to 1.2 (120%).
                page.PageSheet.PrintProps.ScaleX.Value = 1.2;
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}
