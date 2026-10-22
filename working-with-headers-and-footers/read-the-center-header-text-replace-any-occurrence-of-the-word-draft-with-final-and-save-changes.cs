using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path (overwrite if not provided)
        string outputPath = args.Length >= 2 ? args[1] : inputPath;

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Read the current center header text
                string currentHeader = diagram.HeaderFooter.HeaderCenter;

                // Replace occurrences of "Draft" with "Final"
                string updatedHeader = currentHeader.Replace("Draft", "Final");

                // Assign the modified header back to the diagram
                diagram.HeaderFooter.HeaderCenter = updatedHeader;

                // Save the updated diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Header updated and diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}