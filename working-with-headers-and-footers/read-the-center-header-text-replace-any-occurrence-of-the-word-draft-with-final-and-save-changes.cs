using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            // Write usage information to error output and exit gracefully.
            Console.Error.WriteLine("Usage: DiagramHeaderUpdate <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Read the current center header text.
            string headerCenter = diagram.HeaderFooter.HeaderCenter;

            // Replace any occurrence of "Draft" with "Final".
            if (!string.IsNullOrEmpty(headerCenter) && headerCenter.Contains("Draft"))
            {
                diagram.HeaderFooter.HeaderCenter = headerCenter.Replace("Draft", "Final");
            }

            // Save the modified diagram using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error output.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}