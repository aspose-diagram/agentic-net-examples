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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Combine hidden‑information flags to remove shapes, masters, and styles.
                int hiddenInfoFlags = (int)(RemoveHiddenInfoItem.Shapes |
                                            RemoveHiddenInfoItem.Masters |
                                            RemoveHiddenInfoItem.Styles);

                // Remove hidden metadata from the diagram.
                diagram.RemoveHiddenInformation(hiddenInfoFlags);

                // Save the cleaned diagram back to VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}