using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> <outputVisioFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Convert 0.3 centimeters to inches (1 cm = 0.393701 inches)
            double centimeters = 0.3;
            double inches = centimeters * 0.393701;

            // Set the global footer margin (in inches)
            diagram.HeaderFooter.FooterMargin.Value = inches;

            // Verify the change by reading back the value
            double verifiedMargin = diagram.HeaderFooter.FooterMargin.Value;
            Console.WriteLine($"Footer margin set to {verifiedMargin} inches (expected ~{inches} inches).");

            // TODO: Implement Print Preview verification using Aspose.Diagram.Printing APIs when available.
            // Example (placeholder):
            // using Aspose.Diagram.Printing;
            // PrintPreview preview = new PrintPreview(diagram);
            // preview.Show();

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}