using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: RemoveHiddenInfoUtility <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – \"{inputPath}\"");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Combine the hidden‑information flags that are supported.
                // DataConnections flag is not available in this version, so we use Shapes, Masters, and Styles.
                int flags = (int)(RemoveHiddenInfoItem.Shapes |
                                   RemoveHiddenInfoItem.Masters |
                                   RemoveHiddenInfoItem.Styles);

                // Remove hidden information.
                diagram.RemoveHiddenInformation(flags);
                Console.WriteLine("Hidden information removed.");

                // Save the cleaned diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Cleaned diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
