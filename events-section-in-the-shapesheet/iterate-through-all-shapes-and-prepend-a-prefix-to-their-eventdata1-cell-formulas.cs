using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Prefix to prepend to each Event cell formula
            string prefix = "MyPrefix_";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current formula from a valid event cell (TheData)
                    string currentFormula = shape.Event.TheData.Ufe.F ?? string.Empty;

                    // Prepend the prefix and assign back to the cell
                    shape.Event.TheData.Ufe.F = prefix + currentFormula;
                }
            }

            // Path for the modified diagram
            string outputPath = "output.vsdx";

            // Save the modified diagram using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}