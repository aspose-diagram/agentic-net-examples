using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first diagram path, second diagram path, and output path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <firstDiagramPath> <secondDiagramPath> <outputPath>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify that the input files exist.
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load the first diagram (the target) inside a using block.
            using (Diagram targetDiagram = new Diagram(firstPath))
            {
                // Load the second diagram (the source) inside a using block.
                using (Diagram sourceDiagram = new Diagram(secondPath))
                {
                    // Combine the source diagram into the target diagram.
                    // This appends the source pages after the target pages,
                    // preserving the original order of the first diagram.
                    targetDiagram.Combine(sourceDiagram);
                }

                // Save the combined diagram to the specified output path in VSDX format.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagrams combined successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
    }
}