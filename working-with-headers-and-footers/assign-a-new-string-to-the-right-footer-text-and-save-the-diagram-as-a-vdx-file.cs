using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input file and output file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify that the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified input file.
            Diagram diagram = new Diagram(inputPath);

            // Assign a new string to the right side of the footer.
            diagram.HeaderFooter.FooterRight = "New footer text";

            // Save the modified diagram in VDX format.
            diagram.Save(outputPath, SaveFileFormat.Vdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}