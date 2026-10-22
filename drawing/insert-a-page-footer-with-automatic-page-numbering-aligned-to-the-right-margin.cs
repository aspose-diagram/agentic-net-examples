using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command-line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Insert a footer aligned to the right margin with automatic page numbering.
            // The '&p' field code inserts the current page number.
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Optionally set the distance of the footer from the bottom edge (in inches).
            // This demonstrates usage of the FooterMargin property.
            diagram.HeaderFooter.FooterMargin.Value = 0.5;

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}