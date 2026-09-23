using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram using the VSDX format.
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Combine hidden‑information flags (shapes and masters only; pages flag is not available).
            int hiddenInfoFlags = (int)(RemoveHiddenInfoItem.Shapes |
                                        RemoveHiddenInfoItem.Masters);

            // Remove the specified hidden information from the diagram.
            diagram.RemoveHiddenInformation(hiddenInfoFlags);

            // Save the cleaned diagram back to VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Cleaned diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}