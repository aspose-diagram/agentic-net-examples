using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <firstDiagramPath> <secondDiagramPath> <outputDiagramPath>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the first input file exists.
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPath}");
            return;
        }

        // Guard: ensure the second input file exists.
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPath}");
            return;
        }

        try
        {
            // Load the first diagram (VSD) inside a using block as required.
            using (Diagram firstDiagram = new Diagram(firstPath))
            {
                // Load the second diagram (VSDX) inside a using block.
                using (Diagram secondDiagram = new Diagram(secondPath))
                {
                    // Combine the second diagram into the first, preserving the page order of the first diagram.
                    firstDiagram.Combine(secondDiagram);

                    // Save the combined diagram to the specified output path in VSDX format.
                    firstDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine($"Successfully combined diagrams. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}